using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Models.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;
        public AuthDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var cfg = _configuration.GetSection("AuthDbContextConfig").Get<AuthDbContextConfig>();
            var mailCfg = _configuration.GetSection("EmailConfig").Get<EmailServiceConfig>();
            if (cfg == null) return;
            if(mailCfg == null) return;
            var AdminRoleId = cfg.AdminRoleId;
            var EmployeeRoleId = cfg.EmployeeRoleId;
            var VetRoleId = cfg.VetRoleId;

            base.OnModelCreating(builder);

            //Create Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = AdminRoleId
                },
                new IdentityRole()
                {
                    Id = EmployeeRoleId,
                    Name = "Employee",
                    NormalizedName = "Employee".ToUpper(),
                    ConcurrencyStamp = EmployeeRoleId
                },
                new IdentityRole()
                {
                    Id = VetRoleId,
                    Name = "Vet",
                    NormalizedName = "Vet".ToUpper(),
                    ConcurrencyStamp = VetRoleId
                }
            };

            //Seed Roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create admin user
            if (mailCfg.AsAdmin == null) return;
            var AdminUserId = cfg.AdminUserId;
            var Admin = new IdentityUser()
            {
                Id = AdminUserId,
                UserName = mailCfg.AsAdmin.id,
                Email = mailCfg.AsAdmin.id,
                NormalizedEmail = mailCfg.AsAdmin.id.ToUpper(),
                NormalizedUserName = mailCfg.AsAdmin.id.ToUpper()
            };
            Admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(Admin, cfg.AdminInitialPassword);

            builder.Entity<IdentityUser>().HasData(Admin);

            //Give Admin Role

            var AdminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = AdminUserId,
                    RoleId = AdminRoleId,
                },
                new()
                {
                    UserId = AdminUserId,
                    RoleId = EmployeeRoleId,
                },
                new()
                {
                    UserId = AdminUserId,
                    RoleId = VetRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(AdminRoles);
        }
    }
}
