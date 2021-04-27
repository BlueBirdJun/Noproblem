
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nop.RootApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        //public IActionResult Login([FromBody] UserModel login)
        //{
        //    IActionResult response = Unauthorized();
        //    var user = AuthenticateUser(login);

        //    if (user != null)
        //    {
        //        var tokenString = GenerateJSONWebToken2(login);//GenerateJSONWebToken(user);
        //        response = Ok(new { token = tokenString });
        //    }

        //    return response;
        //}

        private string MakeJwt()
        {
            var jwtSecurityKey = _config["Jwt:Key"];//_configuration.GetValue<string>("JWT:SecritKey");
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, "name") };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                issuer: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(10),
                audience: _config["Jwt:Issuer"]
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
