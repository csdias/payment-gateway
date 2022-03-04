using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EnterpriseBusinessRules.Entities;
using System;
using ApplicationBusinessRules.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace InterfaceAdapters.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("v1/auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult Login([FromBody] LoginEntity input)
        {
            var login = ValidatorHelper.ValidateEntity<LoginEntity>(input);
            if (login.HasErrors())
            {
                return Ok(login.GetMessages());
            } 
            
            if ((input.Email == "payment@gateway.com") &&
                (input.Password == "123@Test"))
            {
                return Ok(new
                {
                    Email = input.Email,
                    Token = CreateJwt(input.Email)
                });
            }
            
            var response = new Response<string>().AddMessage("User and/or password invalid!!");
            return BadRequest(response.GetMessages());
        }

        private string CreateJwt(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_TOKEN"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT_ISSUER"],
                Audience = _configuration["JWT_AUDIENCE"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("ClientId", "A953DC88-EB1B-350C-E053-2C118C0A2285")
                }),
                Expires = DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["JWT_EXPIRESINMINUTE"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
