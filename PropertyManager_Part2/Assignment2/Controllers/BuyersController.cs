using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using System.Security.Cryptography;
using System;

namespace Assignment2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase {
        private readonly PropertyManagerContext _context;

        public BuyersController(PropertyManagerContext context) {
            _context = context;
        }

        // GET: api/Buyers
        [HttpGet]
        public List<Buyer> GetBuyers() {
            var query = from b in _context.Buyer
                        select new { b.Name, b.Credit, b.NumberOfOwnedProperties };
            var products = query.ToList().Select(r => new Buyer {
                Name = r.Name, Credit = r.Credit, NumberOfOwnedProperties = r.NumberOfOwnedProperties }).ToList();
            return products;
        }

        // GET: api/Buyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id) {
            var buyer = await _context.Buyer.Include(b => b.OwnedProperties).FirstOrDefaultAsync(b => b.Id == id);
            if (buyer == null)
                return NotFound();
            return buyer;
        }

        // GET: api/Buyers/5/properties
        [HttpGet("{id}/properties")]
        public async Task<ActionResult<Buyer>> GetBuyerProperties(int id) {
            var buyer = await _context.Buyer.Include(b => b.OwnedProperties).FirstOrDefaultAsync(b => b.Id == id);
            if (buyer == null)
                return NotFound();
            return Ok(buyer.OwnedProperties);
        }

        // PUT: api/Buyers/5
        [HttpPut("{BuyerId}/{PropertyId}")]
        public async Task<IActionResult> PurchaseProperty(int BuyerId, int PropertyId) {
            if (!BuyerExists(BuyerId) || _context.Property.Any(e => e.Id == PropertyId) == false)
                return BadRequest();
            var buyer = await _context.Buyer.FirstOrDefaultAsync(x => x.Id == BuyerId);
            var property = await _context.Property.FirstOrDefaultAsync(x => x.Id == PropertyId);
            if(buyer.Credit >= property.Price) {
                property.currentBuyer = buyer;
                buyer.Credit -= property.Price;
                buyer.NumberOfOwnedProperties++;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBuyer", new { id = BuyerId }, buyer);
            }
            else
                return CreatedAtAction("PrintError", "Purchase error: not enough credit");
        }
        public string PrintError(string err) {
            return err;
        }

        // POST: api/Buyers
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer) {
            if (!_context.Buyer.Any(x => x.Name.Equals(buyer.Name))){
                //salt and hash
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(buyer.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string pass = Convert.ToBase64String(hashBytes);
                buyer.Password = pass;
                buyer.NumberOfOwnedProperties = 0;
                //insert
                _context.Buyer.Add(buyer);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
            } else
                return BadRequest();
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buyer>> DeleteBuyer(int id) {
            var buyer = await _context.Buyer.FindAsync(id);
            if (buyer == null)
                return NotFound();
            _context.Buyer.Remove(buyer);
            await _context.SaveChangesAsync();
            return buyer;
        }

        private bool BuyerExists(int id) {
            return _context.Buyer.Any(e => e.Id == id);
        }
    }
}
