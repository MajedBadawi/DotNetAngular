using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;
using ReflectionIT.Mvc.Paging;

namespace Assignment3.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase {
        private readonly PropertyManagerContext _context;

        public PropertiesController(PropertyManagerContext context) {
            _context = context;
        }

        // GET: api/Properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperty() {
            return await _context.Property.ToListAsync();
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id) {
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
                return NotFound();
            return @property;
        }

        // GET: api/Properties/filter
        [HttpGet("filter")]
        public async Task<IEnumerable<Property>> FilterProperties([FromQuery] int? FromPrice, int? ToPrice, string Address, int? NumberOfRooms) {
            var properties = _context.Property as IQueryable<Property>;
            if (FromPrice.HasValue && ToPrice.HasValue)
                properties = properties.Where(p => FromPrice <= p.Price && p.Price <= ToPrice);
            if (NumberOfRooms.HasValue)
                properties = properties.Where(p => p.NumberOfRooms == NumberOfRooms);
            if (!string.IsNullOrEmpty(Address))
                properties = properties.Where(p => p.Address.ToUpper().Contains(Address.ToUpper()));
            return await properties.OrderByDescending(p => p.Price).ToListAsync();
        }

        // PUT: api/Properties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(int id, Property p) {
            var property = await _context.Property.FirstOrDefaultAsync(x => x.Id == id);
            if (property == null)
                return BadRequest();
            property.Title = p.Title;
            property.Address = p.Address;
            property.NumberOfRooms = p.NumberOfRooms;
            property.Price = p.NumberOfRooms * 15000;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProperty", new { id = @property.Id }, @property);
        }

        // POST: api/Properties
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property @property) {
            property.Price = property.NumberOfRooms * 15000;
            _context.Property.Add(@property);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProperty", new { id = @property.Id }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(int id) {
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
                return NotFound();
            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();
            return @property;
        }

        public bool PropertyExists(int id) {
            return _context.Property.Any(e => e.Id == id);
        }
    }
}
