namespace LoWaiLo.WebAPI.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedAddonsMiddleware
    {
        private readonly RequestDelegate next;

        public SeedAddonsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var addonsService = serviceProvider.GetService<IAddonsService>();

            if (!addonsService.Any())
            {
                var addons = new List<Addon>();

                var soySauce = new Addon
                {
                    Name = "Соев сос",
                    Description = "80 гр.",
                    Price = 0.80m,
                };
                addons.Add(soySauce);

                var garlicSauce = new Addon
                {
                    Name = "Чеснов сос",
                    Description = "80 гр.",
                    Price = 0.80m,
                };
                addons.Add(garlicSauce);

                var meat = new Addon
                {
                    Name = "Месо",
                    Description = "100 гр.",
                    Price = 1.60m,
                };
                addons.Add(meat);

                var vegetables= new Addon
                {
                    Name = "Зеленчуци",
                    Description = "100 гр.",
                    Price = 0.50m,
                };
                addons.Add(vegetables);

                var sticks = new Addon
                {
                    Name = "Клечки",
                    Description = "1 бр.",
                    Price = 0.50m,
                };
                addons.Add(sticks);

                var mushrooms = new Addon
                {
                    Name = "Китайски гъби",
                    Description = "100 гр.",
                    Price = 1.60m,
                };
                addons.Add(mushrooms);

                var bamboo = new Addon
                {
                    Name = "Бамбук",
                    Description = "100 гр.",
                    Price = 1.60m,
                };
                addons.Add(bamboo);
                await addonsService.AddRangeAsync(addons);
            }

            await this.next(context);
        }
    }
}
