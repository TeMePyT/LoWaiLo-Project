namespace LoWaiLo.WebAPI.Middlewares
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedAdminMiddleware
    {
        private readonly RequestDelegate next;

        public SeedAdminMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "admin@SoftUni.com",
                    UserName = "admin@SoftUni.com",
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    ShoppingCart = new ShoppingCart(),
                };

                var userResult = await userManager.CreateAsync(user, "123456");

                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException();
                }

                var roleResult = await userManager.AddToRoleAsync(user, "Administrator");
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException();
                }
            }

            await this.next(context);
        }
    }
}
