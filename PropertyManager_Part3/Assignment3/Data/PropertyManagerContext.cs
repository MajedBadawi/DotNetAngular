using Assignment3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Data {
    public class PropertyManagerContext : IdentityDbContext {
        public PropertyManagerContext(DbContextOptions<PropertyManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(u => u.OwnedProperties)
                .WithOne(p => p.currentBuyer);
        }

        public DbSet<Property> Property { get; set; }
        public DbSet<User> User { get; set; }
    }
}
