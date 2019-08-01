namespace LoWaiLo.WebAPI.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedProductsMiddleware
    {
        private readonly RequestDelegate next;

        public SeedProductsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            var productsService = serviceProvider.GetService<IProductsService>();

            if (!productsService.Any())
            {
                var categoriesService = serviceProvider.GetService<ICategoriesService>();
                var products = new List<Product>();

                var proletniRolca = new Product
                {
                    Name = "Пролетни ролца",
                    MenuNumber = 1,
                    Price = 2.70m,
                    Weight = 200,
                    Image = "https://www.dropbox.com/s/axa8nnf6bac10zf/01.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Предястия").Id,
                };

                products.Add(proletniRolca);

                var paniraniZelenchuci = new Product
                {
                    Name = "Панирани зеленчуци",
                    MenuNumber = 3,
                    Price = 3.80m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Предястия").Id,
                };
                products.Add(paniraniZelenchuci);
                var salata = new Product
                {
                    Name = "Салата от китайски гъби",
                    MenuNumber = 9,
                    Price = 4.80m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/6ysks28rw3ntxjg/09.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Предястия").Id,
                };
                products.Add(salata);
                var supaSkaridi = new Product
                {
                    Name = "Супа от скариди",
                    MenuNumber = 26,
                    Price = 3.00m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/fi3tk319lj9fm5g/26.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Супи").Id,
                };
                products.Add(supaSkaridi);
                var supaVodorasli = new Product
                {
                    Name = "Супа от морски водорасли",
                    MenuNumber = 29,
                    Price = 2.30m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/drla3ojeual1qvx/29.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Супи").Id,
                };
                products.Add(supaVodorasli);
                var orizGuanDong = new Product
                {
                    Name = "Ориз гуан донг с яйце, грах и шунка",
                    MenuNumber = 35,
                    Price = 4.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/7kmoch7lf6m4n3b/39.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с ориз").Id,
                };
                products.Add(orizGuanDong);
                var orizSkaridi = new Product
                {
                    Name = "Ориз със скариди, яйца и шунка",
                    MenuNumber = 35,
                    Price = 6.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/u05yopm9pexbxi0/40.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с ориз").Id,
                };
                products.Add(orizSkaridi);
                var spagetiFenPile = new Product
                {
                    Name = "Ми Фен с пилешко и зеленчуци",
                    MenuNumber = 44,
                    Price = 6.80m,
                    Weight = 700,
                    Image = "https://www.dropbox.com/s/bnkeby96hu5yvqy/44.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Спагети Ми Фен").Id,
                };
                products.Add(spagetiFenPile);
                var spagetiFenSvinsko = new Product
                {
                    Name = "Ми Фен със свинско и зеленчуци",
                    MenuNumber = 43,
                    Price = 6.80m,
                    Weight = 700,
                    Image = "https://www.dropbox.com/s/6of3ubsnrk43azz/43.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Спагети Ми Фен").Id,
                };
                products.Add(spagetiFenSvinsko);
                var pileUKsiang = new Product
                {
                    Name = "Пиле Ю Ксианг",
                    MenuNumber = 69,
                    Price = 9.20m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/ithw2vi6e4a9xmz/69.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с пилешко").Id,
                };
                products.Add(pileUKsiang);
                var osemte = new Product
                {
                    Name = "Осемте драгоценности",
                    MenuNumber = 52,
                    Price = 13.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/yl7ve3adsur5pqb/52.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с пилешко").Id,
                };
                products.Add(osemte);
                var spagetiPileZel = new Product
                {
                    Name = "Спагети с пилешко месо и зеленчуци",
                    MenuNumber = 48,
                    Price = 5.90m,
                    Weight = 750,
                    Image = "https://www.dropbox.com/s/s901j9y9vv893ec/48.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със спагети").Id,
                };
                products.Add(spagetiPileZel);
                var spagetiTriVida = new Product
                {
                    Name = "Спагети с три вида месо и зеленчуци",
                    MenuNumber = 50,
                    Price = 6.80m,
                    Weight = 750,
                    Image = "https://www.dropbox.com/s/z1h9bfzt4ztjxaw/50.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със спагети").Id,
                };
                products.Add(spagetiTriVida);
                var teleBambuk = new Product
                {
                    Name = "Телешко с бамбук и китайски гъби",
                    MenuNumber = 70,
                    Price = 9.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/dstetvsbhfvetfu/70.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с телешко").Id,
                };
                products.Add(teleBambuk);
                var teleKitai = new Product
                {
                    Name = "Телешко с бамбук и китайски гъби",
                    MenuNumber = 70,
                    Price = 11.80m,
                    Weight = 700,
                    Image = "https://www.dropbox.com/s/jhs7gi8cwqaq71k/76.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия с телешко").Id,
                };
                products.Add(teleKitai);
                var praseU = new Product
                {
                    Name = "Свинско Ю Ксианг",
                    MenuNumber = 80,
                    Price = 9.20m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/1kgd5vowqchawzn/80.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със свинско").Id,
                };
                products.Add(praseU);
                var rebra = new Product
                {
                    Name = "Свински ребра в сладко-кисел сос",
                    MenuNumber = 88,
                    Price = 10.80m,
                    Weight = 450,
                    Image = "https://www.dropbox.com/s/b77byqn0i985jsk/88.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със свинско").Id,
                };
                products.Add(rebra);
                var paticaTigan = new Product
                {
                    Name = "Патица на тиган",
                    MenuNumber = 97,
                    Price = 12.30m,
                    Weight = 700,
                    Image = "https://www.dropbox.com/s/m87xaz4lh804tui/97.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със патешко").Id,
                };
                products.Add(paticaTigan);
                var paticaPikant = new Product
                {
                    Name = "Пържена пикантна патица",
                    MenuNumber = 94,
                    Price = 11.30m,
                    Weight = 700,
                    Image = "https://www.dropbox.com/s/lgr4o86z1tll3jg/94.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Ястия със патешко").Id,
                };
                products.Add(paticaPikant);
                var kalmari = new Product
                {
                    Name = "Препържени калмари",
                    MenuNumber = 105,
                    Price = 17.00m,
                    Weight = 400,
                    Image = "https://www.dropbox.com/s/h9k0nviciwrmxjq/105.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Морски деликатеси").Id,
                };
                products.Add(kalmari);
                var skaridi = new Product
                {
                    Name = "Препържени скариди",
                    MenuNumber = 108,
                    Price = 20.50m,
                    Weight = 400,
                    Image = "https://www.dropbox.com/s/o0a5xqf4s64zh88/108.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Морски деликатеси").Id,
                };
                products.Add(skaridi);
                var sladoled = new Product
                {
                    Name = "Пържен сладолед",
                    MenuNumber = 120,
                    Price = 3.00m,
                    Weight = 250,
                    Image = "https://www.dropbox.com/s/5fyq8amu157ck09/120.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Десерти").Id,
                };
                products.Add(sladoled);
                var banan = new Product
                {
                    Name = "Пържен банан",
                    MenuNumber = 119,
                    Price = 2.70m,
                    Weight = 200,
                    Image = "https://www.dropbox.com/s/5yyccz17utg48mc/119.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Десерти").Id,
                };
                products.Add(banan);
                var plodove = new Product
                {
                    Name = "Пържени плодове",
                    MenuNumber = 116,
                    Price = 2.80m,
                    Weight = 200,
                    Image = "https://www.dropbox.com/s/049r2sonlejudlz/116.jpg?dl=0",
                    CategoryId = categoriesService.FindByName("Десерти").Id,
                };
                products.Add(plodove);
                await productsService.AddRangeAsync(products);
            }

            await this.next(context);
        }
    }
}
