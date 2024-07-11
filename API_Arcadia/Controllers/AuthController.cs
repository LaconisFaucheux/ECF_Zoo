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

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        //[Authorize]
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
                return  BadRequest("Au moins un champ est invalide");
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
