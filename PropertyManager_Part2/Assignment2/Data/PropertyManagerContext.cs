using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Data {
    public class PropertyManagerContext : DbContext {
        public PropertyManagerContext(DbContextOptions<PropertyManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Buyer>()
                .HasMany(b => b.OwnedProperties)
                .WithOne(p => p.currentBuyer);
        }

        public DbSet<Property> Property { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
    }
}
