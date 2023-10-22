using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMSBlazorAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TMSBlazorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace TMSBlazorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TMSDbContext _dbContext;
        private readonly JwtSetting jwtSetting;

        public AuthenticationController(TMSDbContext tMSDbContext, IOptions<JwtSetting> options) 
        {
            this._dbContext = tMSDbContext;
            this.jwtSetting = options.Value;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(item => item.Email == userCred.UserName && item.Password == userCred.Password);
            if (user == null)
                return Unauthorized();

            //generate token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(this.jwtSetting.securitykey);
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, user.Email) }
                        ),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
                var token = tokenhandler.CreateToken(tokendesc);
                string finaltoken = tokenhandler.WriteToken(token);

                return Ok(finaltoken);


        }


    }
}
