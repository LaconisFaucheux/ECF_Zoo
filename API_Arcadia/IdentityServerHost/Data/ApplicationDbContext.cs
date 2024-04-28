using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerHost.Data
{
    public class ApplicationDbContext : IdentityDbContext<ArcadiaUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ArcadiaUser>(entity =>
            {
                entity.Property(e => e.Fonction)
                    .HasMaxLength(50)
                    .HasDefaultValue(""); //TODO: définir une valeur par défaut 
            });
        }
    }
}
