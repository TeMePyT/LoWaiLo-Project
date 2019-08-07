namespace LoWaiLo.WebAPI.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
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
                var categories = new List<Category>();
                var appetizers = new Category
                {
                    Name = "Предястия",
                    Image = "https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1",
                };
                categories.Add(appetizers);
                var soups = new Category
                {
                    Name = "Супи",
                    Image = "https://www.dropbox.com/s/8a7suzb06c0b88a/sm-02.jpg?raw=1",
                };
                categories.Add(soups);
                var rise = new Category
                {
                    Name = "Ястия с ориз",
                    Image = "https://www.dropbox.com/s/9ywejk80lb1c8fi/sm-03.jpg?raw=1",
                };
                categories.Add(rise);
                var mifen = new Category
                {
                    Name = "Спагети Ми Фен",
                    Image = "https://www.dropbox.com/s/hmb9kgy3u5j8qqm/sm-04.jpg?raw=1",
                };
                categories.Add(mifen);
                var spagheti = new Category
                {
                    Name = "Ястия със спагети",
                    Image = "https://www.dropbox.com/s/gbu9mlkd4qmoe6n/sm-05.jpg?raw=1",
                };
                categories.Add(spagheti);
                var chicken = new Category
                {
                    Name = "Ястия с пилешко",
                    Image = "https://www.dropbox.com/s/iyzfmyqc088doat/sm-06.jpg?raw=1",
                };
                categories.Add(chicken);
                var beef = new Category
                {
                    Name = "Ястия с телешко",
                    Image = "https://www.dropbox.com/s/7hoy4w3knu8qwrw/sm-07.jpg?raw=1",
                };
                categories.Add(beef);
                var pork = new Category
                {
                    Name = "Ястия със свинско",
                    Image = "https://www.dropbox.com/s/ngvoqa2ipxft7v7/sm-08.jpg?raw=1",
                };
                categories.Add(pork);
                var duck = new Category
                {
                    Name = "Ястия със патешко",
                    Image = "https://www.dropbox.com/s/kiw1dqkns1uq8tm/sm-09.jpg?raw=1",
                };
                categories.Add(duck);
                var seafood = new Category
                {
                    Name = "Морски деликатеси",
                    Image = "https://www.dropbox.com/s/8866z3vjlkazvzz/sm-10.jpg?raw=1",
                };
                categories.Add(seafood);
                var deserts = new Category
                {
                    Name = "Десерти",
                    Image = "https://www.dropbox.com/s/sieptexpbwekvws/sm-11.jpg?raw=1",
                };
                categories.Add(deserts);
                await categoriesService.CreateRangeAsync(categories);
            }

            await this.next(context);
        }
    }
}
