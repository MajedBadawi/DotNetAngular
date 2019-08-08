using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Assignment3.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private UserManager<User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public AuthController(PropertyManagerContext context, UserManager<User> userManager, IOptions<ApplicationSettings> appSettings) {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        // POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<object> RegisterBuyer(User buyer) {
            try {
                var result = await _userManager.CreateAsync(buyer, buyer.Password);
                return Ok(result);
            } catch (Exception e) {
                throw e;
            }
        }

        //POST : /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model) {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            } else
                return BadRequest(new { message = "Username and/or password are incorrect." });
        }
    }
}