using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using JinjiProject.DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Seeds
{
    public static class AdminSeed
    {
        private const string AdminEmail = "admin@jinjiapp.com";
        private const string AdminPassword = "newPassword+0";
        public static async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();

            dbContextBuilder.UseSqlServer(configuration.GetConnectionString(AppDbContext.ConnectionName));

            using AppDbContext context = new(dbContextBuilder.Options);
            if (!context.Roles.Any())
            {
                await AddRoles(context);
            }

            if (!context.Users.Any(user => user.Email == AdminEmail))
            {
                await AddAdmin(context);
            }

            await Task.CompletedTask;
        }

        private static async Task AddAdmin(AppDbContext context)
        {
            AppUser user = new()
            {
                UserName = AdminEmail,
                NormalizedUserName = AdminEmail.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = "ADMIN@JINJIAPP.COM",
                EmailConfirmed = true,
                CreatedDate = DateTime.Now,
                Status = Status.Active 
            };
            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, AdminPassword);
            await context.Users.AddAsync(user);

            var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString())!.Id;

            await context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = user.Id, RoleId = adminRoleId });

            context.Admins.Add(new Admin
            {
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FirstName = "Admin",
                LastName = "Admin",
                Email = AdminEmail,
                ImagePath = "~/images/defaultUserPhoto.png",
                Gender = Gender.Man,
                BirthDate = new DateTime(2000, 1, 1),
                AppUserId = user.Id,
            });
            await context.SaveChangesAsync();
        }

        private static async Task AddRoles(AppDbContext context)
        {
            string[] roles = Enum.GetNames(typeof(Roles));
            for (int i = 0; i < roles.Length; i++)
            {
                if (await context.Roles.AnyAsync(role => role.Name == roles[i]))
                {
                    continue;
                }

                await context.Roles.AddAsync(new IdentityRole() { Name = roles[i] ,NormalizedName = roles[i].ToUpper()});
            }
            context.SaveChanges();
        }
    }
}
