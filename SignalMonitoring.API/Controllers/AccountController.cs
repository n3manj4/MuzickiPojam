using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SignalMonitoring.API.Controllers
{
    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly UserManager<IdentityUser> m_userManager;
        readonly SignInManager<IdentityUser> m_signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            m_userManager = userManager;
            m_signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.UserName, Email = credentials.UserName };

            var result = await m_userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await m_signInManager.SignInAsync(user, isPersistent: false);

            return Ok(CreateToken(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await m_signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, false, false);

            if (!result.Succeeded)
                return BadRequest();

            var user = await m_userManager.FindByEmailAsync(credentials.UserName);

            return Ok(CreateToken(user));
        }

        string CreateToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GiVe me BesT JWT t0kEn"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials/*, claims: claims*/);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}