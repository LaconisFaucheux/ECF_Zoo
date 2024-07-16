using API_Arcadia.Interfaces;
using API_Arcadia.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Arcadia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO r)
        {
			IdentityResult IdentityResult;
			// Create IdentityUser object
			var user = new IdentityUser
            {
                UserName = r.Email?.Trim(),
                Email = r.Email?.Trim(),
            };            

            if (!String.IsNullOrEmpty(r.Password) 
                && !String.IsNullOrEmpty(r.Email) 
                && !String.IsNullOrEmpty(r.Role))
            {
                IdentityResult = await _userManager.CreateAsync(user, r.Password);
            }
            else
            {
                return  BadRequest("Au moins un champ Email, Password ou Role, est invalide");
            }
			
            if (IdentityResult.Succeeded)
            {
                IdentityResult = await _userManager.AddToRoleAsync(user, r.Role);

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

            return  ValidationProblem(ModelState);

        }

        private void IdentityResultChecking (IdentityResult ir)
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
