using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoneyMe.Common.Helpers;
using MoneyMe.Model.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyMe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<string> Get(string password)
        {
            if (string.IsNullOrEmpty(password.Trim()))
                return BadRequest("Password is required.");

            string hashedPassword = PasswordHashHelper.HashPasword(password);

            return Ok(hashedPassword);
        }

        [HttpPost("Generate")]
        public ActionResult<string> Generate(TokenInfoDto info)
        {
            if (string.IsNullOrEmpty(info.Username))
                return BadRequest("Username is required.");

            if (string.IsNullOrEmpty(info.Password))
                return BadRequest("Password is required.");

            if (!PasswordHashHelper.VerifyPassword(info.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(info.Username);
            return Ok(new
            {
                access_token = token,
                message = "Successfully Created JWT Token!"
            });
        }


        private string CreateToken(string username)
        {
            IList<Claim> claims = new List<Claim>
            {
                new("UserName", username),
                new(ClaimTypes.Role, "Admin")
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthDetails:Hash-SHA512-Key")));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token = new(claims: claims,
                                         expires: DateTime.Now.AddDays(1),
                                         signingCredentials: creds);

            string jwtTokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtTokenString;
        }
    }
}
