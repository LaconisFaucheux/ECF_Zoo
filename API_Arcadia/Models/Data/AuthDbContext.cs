using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Arcadia.Models.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var AdminRoleId = "92f3d9c7-ca7a-4032-b2e5-45f511eca19e";
            var EmployeeRoleId = "4dd3b707-837e-4ba5-b8b7-cc2a1f0aec49";
            var VetRoleId = "2bc0d38d-b990-454c-91a5-95705251063c";

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
            var AdminUserId = "4d1f3651-74f6-4542-96a1-418e9b9ccb79";
            var Admin = new IdentityUser()
            {
                Id = AdminUserId,
                UserName = "admin@arcadia.fr",
                NormalizedEmail = "admin@arcadia.fr".ToUpper(),
                NormalizedUserName = "admin@arcadia.fr".ToUpper()
            };
            Admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(Admin, "Admin`@123");

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
