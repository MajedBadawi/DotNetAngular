using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private PropertyManagerContext _context;
        private UserManager<User> _userManager;

        public UsersController(PropertyManagerContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        //GET : /api/Users
        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserInfo() {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new {
                user.Name,
                user.Credit,
                user.NumberOfOwnedProperties
            };
        }

        // PUT: api/Users/5
        [HttpPut("{PropertyId}")]
        [Authorize]
        public async Task<bool> PurchaseProperty(int PropertyId) {
            var user = await _userManager.FindByIdAsync(User.Claims.First(c => c.Type == "UserID").Value);
            var property = await _context.Property.FirstOrDefaultAsync(x => x.Id == PropertyId);
            if (user.Credit >= property.Price) {
                property.currentBuyer = user;
                property.Purchased = '1';
                user.Credit -= property.Price;
                user.NumberOfOwnedProperties++;
                var res = await _context.SaveChangesAsync();
                return true;
            } else
                return false;
        }
        public string PrintError(string err) {
            return err;
        }

    }
}