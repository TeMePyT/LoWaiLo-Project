namespace LoWaiLo.Data.Seeding
{
    using System;
    using System.Linq;
    using LoWaiLo.Common;
    using LoWaiLo.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    public static class ApplicationDbContextSeeder
    {
        public static void Seed(LoWaiLoDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            Seed(dbContext, roleManager);
        }

        public static void Seed(LoWaiLoDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            SeedRoles(roleManager);
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
            SeedRole(GlobalConstants.MerchantRoleName, roleManager);
            SeedRole(GlobalConstants.CarrierRoleName, roleManager);
            SeedRole(GlobalConstants.ApplicationUserRoleName, roleManager);
        }

        private static void SeedRole(string roleName, RoleManager<ApplicationRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new ApplicationRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
