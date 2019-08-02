namespace LoWaiLo.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Xunit;

    public class ProductsServiceTests
    {
        private readonly IRepository<Product> productsRepository;
        private readonly ProductsService productsService;
        private readonly LoWaiLoDbContext context;
        public ProductsServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            this.context = new LoWaiLoDbContext(options);

            this.productsRepository = new DbRepository<Product>(context);
            this.productsService = new ProductsService(productsRepository);
        }

        [Fact]
        public async Task AddAsyncShouldAddProductSuccesfully()
        {
            var product = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };

            await productsService.AddAsync(product);

            var result = productsRepository.All().ToList().Count();
            Assert.Equal(1, result);

            var productResult = productsRepository.All().First();

            Assert.Equal(123, productResult.Id);
            Assert.Equal("Панирани зеленчуци", productResult.Name);
            Assert.Equal(3, productResult.MenuNumber);
            Assert.Equal(3.80m, productResult.Price);
            Assert.Equal(300, productResult.Weight);
            Assert.Equal("https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0", productResult.Image);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteProductSuccesfully()
        {
            var product = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };

            await productsService.AddAsync(product);
            var result1 = productsRepository.All().ToList().Count();
            Assert.Equal(1, result1);

            await productsService.DeleteAsync(product.Id);
            var result2 = productsRepository.All().ToList().Count();
            Assert.Equal(0, result2);
        }

        [Fact]
        public async Task EditAsyncShouldEditProductSuccesfully()
        {
            var product = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };

            await productsService.AddAsync(product);
            context.Entry(product).State = EntityState.Detached;

            var editedProduct = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 2,
                Price = 3.40m,
                Weight = 200,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };

            await productsService.EditAsync(editedProduct);

            var result = productsRepository.All().AsNoTracking().First();

            Assert.Equal("Панирани зеленчуци", result.Name);
            Assert.Equal(2, result.MenuNumber);
            Assert.Equal(3.40m, result.Price);
            Assert.Equal(200, result.Weight);
        }

        [Fact]
        public void AllShouldReturnEmptyCollection()
        {
            var products = productsService.All().ToList();

            Assert.Empty(products);
        }

        [Fact]
        public async Task AllShouldReturnCorrectValues()
        {
            var products = new List<Product>();

            var product1 = new Product
            {
                Name = "Салата от китайски гъби",
                MenuNumber = 9,
                Price = 4.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/6ysks28rw3ntxjg/09.jpg?dl=0",
                Category = new Category
                {
                    Id = 1,
                    Name = "Салати"
                },
                Likes = new List<Like>(),
                Reviews = new List<ProductReview>()
            };
            products.Add(product1);

            var product2 = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
                Category = new Category
                {
                    Id = 2,
                    Name = "Предястия"
                },
                Likes = new List<Like>(),
                Reviews = new List<ProductReview>()
            };
            products.Add(product2);

            await productsService.AddRangeAsync(products);

            var allProducts = productsService.All().ToList();

            Assert.Equal(2, allProducts.Count());

            var result1 = allProducts.First();

            Assert.Equal("Салата от китайски гъби", result1.Name);
            Assert.Equal(9,result1.MenuNumber);
            Assert.Equal(4.80m,result1.Price);
            Assert.Equal("https://www.dropbox.com/s/6ysks28rw3ntxjg/09.jpg?dl=0",result1.Image);

            var result2 = allProducts.Last();

            Assert.Equal("Панирани зеленчуци", result2.Name);
            Assert.Equal(3, result2.MenuNumber);
            Assert.Equal(3.80m, result2.Price);
            Assert.Equal("https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0", result2.Image);
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {
            var result = productsService.Any();
            Assert.False(result);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            var product = new Product
            {
                Id = 123,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };

            await productsService.AddAsync(product);

            var result = productsService.Any();

            Assert.True(result);
        }
    }
}
