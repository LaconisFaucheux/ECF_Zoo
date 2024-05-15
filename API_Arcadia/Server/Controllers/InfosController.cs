using Duende.AccessTokenManagement.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//TODO: SUPPRIMER AVANT MISE EN PROD

namespace BlazorWasm.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InfosController : ControllerBase
    {
        // GET: api/<InfosController>
        [HttpGet("EncodedToken")]
        public async Task<ActionResult<string?>> GetEncodedJWT()
        {
            UserTokenRequestParameters? p = null;
            UserToken tk = await HttpContext.GetUserAccessTokenAsync(p);

            return tk.AccessToken;
        }

        [HttpGet("DecodedToken")]
        public async Task<ActionResult<string>> GetDecodedJWT()
        {
            UserTokenRequestParameters? p = null;
            UserToken tk = await HttpContext.GetUserAccessTokenAsync(p);

            string jetonDecode = string.Empty;
            if (!string.IsNullOrEmpty(tk.AccessToken))
            {
                var jwt = new JwtSecurityToken(tk.AccessToken);
                var doc = JsonDocument.Parse(jwt.Payload.SerializeToJson());
                jetonDecode = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true});
            }

            return jetonDecode;
        }
    }
}
