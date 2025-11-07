using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xedekop.Server.Data.Entities;

namespace Xedekop.Server.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        /// <summary>
        /// Constructor for AppDbContext used in our webapp.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Set of pokemon db.
        /// </summary>
        public DbSet<Pokemon> Pokemons { get; set; }

        /// <summary>
        /// Set of orders db.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Handles creation of tables in db.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Calls parent.
            base.OnModelCreating(builder);

            // Sets as money type.
            builder.Entity<IdentityRole<int>>
            (iR => 
            {
                iR.Property(r => r.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Pokemon>().Property(p => p.Price).HasColumnType("money");
            builder.Entity<OrderItem>().Property(oI => oI.UnitPrice).HasColumnType("money");
        }
    }
}
