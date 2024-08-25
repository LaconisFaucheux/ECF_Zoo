using API_Arcadia.Interfaces;
using API_Arcadia.Models;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//TODO: Créer un service?
namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthDbContext _authDbContext;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenService tokenService,
            AuthDbContext authDbContext,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authDbContext = authDbContext;
            _logger = logger;
        }

        [HttpGet("roles")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles()
        {
            var roles = await _authDbContext.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserWithRolesDTO>>> GetEmployees()
        {
            var Employees = await _authDbContext.Users.ToListAsync();
            var EmployeesWithRoles = new List<UserWithRolesDTO>();

            foreach (var employee in Employees)
            {
                var roles = await _userManager.GetRolesAsync(employee);
                EmployeesWithRoles.Add(new UserWithRolesDTO
                {
                    id = employee.Id,
                    email = employee.Email!,
                    roles = roles.ToList()
                });

            }

            return Ok(EmployeesWithRoles);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserWithRolesDTO>> GetEmployee(string id)
        {
            var Employee = await _userManager.FindByIdAsync(id);

            if (Employee == null)
            {
                return NotFound();
            }

            var Roles = await _userManager.GetRolesAsync(Employee);
            var EmployeeWithRole = new UserWithRolesDTO
            {
                id = Employee.Id,
                email = Employee.Email!,
                roles = Roles.ToList()

            };

            return Ok(EmployeeWithRole);
        }

        [HttpGet("role/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserWithRolesDTO>>> GetEmployeesInRole(string role)
        {
            var Employees = await _userManager.GetUsersInRoleAsync(role);
            var EmployeesWithRoles = new List<UserWithRolesDTO>();

            foreach (var employee in Employees)
            {
                var roles = await _userManager.GetRolesAsync(employee);
                EmployeesWithRoles.Add(new UserWithRolesDTO
                {
                    id = employee.Id,
                    email = employee.Email!,
                    roles = roles.ToList()
                });

            }

            return Ok(EmployeesWithRoles);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutEmployee(string id, [FromBody] UserUpdateDTO employee)
        {
            if (id != employee.id)
            {
                return BadRequest("ID invalide");
            }

            if (!Utils.CheckEmailValidity(employee.email))
            {
                return BadRequest("Format d'email invalide");
            }

            try
            {
                var dbEmployee = await _userManager.FindByIdAsync(id);

                if (dbEmployee == null)
                {
                    return NotFound($"Aucun enregistrement pour l'ID {id} dans la table 'Animals'");
                }

                //MAJ des données de l'utilisateur
                dbEmployee.Email = employee.email;
                dbEmployee.UserName = employee.email;
                dbEmployee.NormalizedEmail = employee.email.ToUpper();

                var updateResult = await _userManager.UpdateAsync(dbEmployee);
                if (!updateResult.Succeeded)
                {
                    return BadRequest("Erreur lors de la mise à jour de l'utilisateur");
                }

                //MAJ des rôles
                var currentRoles = await _userManager.GetRolesAsync(dbEmployee);
                var rolesToRemove = currentRoles.Except(employee.roles).ToList();
                var rolesToAdd = employee.roles.Except(currentRoles).ToList();

                var removeRolesResult = await _userManager.RemoveFromRolesAsync(dbEmployee, rolesToRemove);
                if (!removeRolesResult.Succeeded)
                {
                    return BadRequest("Erreur lors de la suppression des roles");
                }

                var addRolesResult = await _userManager.AddToRolesAsync(dbEmployee, rolesToAdd);
                if (!addRolesResult.Succeeded)
                {
                    return BadRequest("Erreur lors de l'ajout des roles");
                }
            }
            catch (Exception e)
            {
                return this.CustomErrorResponse<IdentityUser>(e, null, _logger);
            }

            return Ok();
        }

        [HttpPut("password/{id}")]
        public async Task<IActionResult> UpdatePassword(string id, [FromBody] UpdatePasswordDTO u)
        {
            if (id != u.Id)
            {
                return BadRequest("L'ID fourni ne correspond pas à l'ID de l'utilisateur.");
            }

            if (string.IsNullOrEmpty(u.OldPassword))
            {
                return BadRequest("L'ancien mot de passe est requis. Si votre mot de passe a été oublié rapprochez vous de votre administrateur");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Aucun utilisateur trouvé avec l'ID {id}");
            }

            IdentityResult result;

            if (!string.IsNullOrEmpty(u.OldPassword))
            {
                //Old pwd is known => Regular Pwd Change
                result = await _userManager.ChangePasswordAsync(user, u.OldPassword, u.NewPassword);
            }

            return Ok("Mot de passe mis à jour avec succès.");
        }

        [HttpPut("forgotten-password/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ForgottenPassword(string id, [FromBody] UpdatePasswordDTO u)
        {
            if (id != u.Id)
            {
                return BadRequest("L'ID fourni ne correspond pas à l'ID de l'utilisateur.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Aucun utilisateur trouvé avec l'ID {id}");
            }

            IdentityResult result;
            if (string.IsNullOrEmpty(u.OldPassword))
            {
                // Old Pwd !known => forgotten pwd
                result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddPasswordAsync(user, u.NewPassword);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return ValidationProblem(ModelState);
                }
            }

            return Ok("Mot de passe mis à jour avec succès.");
        }

        [HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO r)
        {
            IdentityResult IdentityResult;
            // Create IdentityUser object

            if (!Utils.CheckEmailValidity(r.Email))
            {
                return BadRequest("Format d'email invalide");
            }

            var user = new IdentityUser
            {
                UserName = r.Email?.Trim(),
                Email = r.Email?.Trim(),
            };

            if (!String.IsNullOrEmpty(r.Password)
                && !String.IsNullOrEmpty(r.Email)
                && r.Roles.Count() > 0)
            {
                IdentityResult = await _userManager.CreateAsync(user, r.Password);
            }
            else
            {
                return BadRequest("Au moins un champ Email, Password ou Role, est invalide");
            }

            if (IdentityResult.Succeeded)
            {
			    IdentityResult = await _userManager.AddToRolesAsync(user, r.Roles);
                

                if (IdentityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    IdentityResultChecking(IdentityResult);
                }
            }
            else
            {
                IdentityResultChecking(IdentityResult);
            }

            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO l)
        {
            if (String.IsNullOrEmpty(l.Email) || String.IsNullOrEmpty(l.Password))
            {
                return Forbid("Mot de passe ou identifiant incorrect");
            }

            var IdentityUser = await _userManager.FindByEmailAsync(l.Email);

            if (IdentityUser != null)
            {
                var CheckPasswordResult = await _userManager.CheckPasswordAsync(IdentityUser, l.Password);

                if (CheckPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(IdentityUser);

                    //Create a Token and Response
                    var token = _tokenService.CreateJwtToken(IdentityUser, roles.ToList());

                    var response = new LoginResponseDTO()
                    {
                        Email = l.Email,
                        Roles = roles.ToList(),
                        Token = token
                    };

                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Mot de passe ou identifiant incorrect");

            return ValidationProblem(ModelState);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var dbUser = await _userManager.FindByIdAsync(id);

            if (dbUser == null)
            {
                return NotFound("Aucun user trouvé en base avec cet ID.");
            }

            var result = await _userManager.DeleteAsync(dbUser);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return ValidationProblem(ModelState);
            }

             return Ok("Utilisateur supprimé avec succès");
           //return NoContent();
        }

        private void IdentityResultChecking(IdentityResult ir)
        {
            if (ir.Errors.Any())
            {
                foreach (var error in ir.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
    }
}
