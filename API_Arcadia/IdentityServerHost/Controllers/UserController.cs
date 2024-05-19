using IdentityServerHost.Data;
using IdentityServerHost.Models;
using IdentityServerHost.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

//pbm de requetes CORS ??

namespace IdentityServerHost.Controllers
{
    [Route("identity_provider/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ArcadiaUser> _userManager;

        public UserController(UserManager<ArcadiaUser> um)
        {
            _userManager = um;
        }

        [HttpGet]
        [Authorize(Policy = "ReadUser")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var userClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            Console.WriteLine($"User Claims: {string.Join(", ", userClaims)}");
            //return await _UserManager.Users.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var usersWithClaims = new List<UserDTO>();           

            foreach (var user in users)
            {

                var claims = await _userManager.GetClaimsAsync(user);
                var claimsDTOList = new List<ClaimDTO>();
                foreach (Claim c in claims)
                {
                    claimsDTOList.Add(new ClaimDTO
                    {
                        Type = c.Type,
                        Value = c.Value
                    });
                }
                var userWithClaims = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Claims = claimsDTOList
                };
                usersWithClaims.Add(userWithClaims);
            }

            return Ok(usersWithClaims);

        }

        //[HttpGet("{id}")]
        ////[Authorize(Policy = "ReadUser")]
        //[AllowAnonymous]
        //public async Task<ActionResult<UserDTO>> GetUser(int id)
        //{
        //    //var req = from u in _userManager.Users
        //    //          where u.Id == id
        //    //          select u;
        //    //ArcadiaUser? user = await req.FirstOrDefaultAsync();

        //    //if (user == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //else
        //    //{
        //    //    return Ok(user);
        //    //}
        //    //
        //    var req = from u in _userManager.Users
        //              where u.Id == id
        //              select u;
        //    var user = await req.FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var claims = await _userManager.GetClaimsAsync(user);
        //    var userWithClaims = new UserDTO
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Claims = claims.ToList()
        //    };

        //    return Ok(userWithClaims);
        //}
    }
     
}
