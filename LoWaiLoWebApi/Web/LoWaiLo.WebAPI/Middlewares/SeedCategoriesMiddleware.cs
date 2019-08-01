namespace LoWaiLo.WebAPI.Middlewares
{
    using System;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Contracts;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedCategoriesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedCategoriesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var categoriesService = serviceProvider.GetService<ICategoriesService>();

            if (!categoriesService.Any())
            {
                await categoriesService.CreateRangeAsync(new string[]
                {
                    "Предястия",
                    "Супи",
                    "Ястия с ориз",
                    "Спагети Ми Фен",
                    "Ястия със спагети",
                    "Ястия с пилешко",
                    "Ястия с телешко",
                    "Ястия със свинско",
                    "Ястия със патешко",
                    "Морски деликатеси",
                    "Десерти",
                });
            }

            await this.next(context);
        }
    }
}
