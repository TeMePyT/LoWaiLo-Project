﻿namespace LoWaiLo.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;

    using Microsoft.EntityFrameworkCore;
   
    using Xunit;

    public class CategoriesServiceTest
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly CategoriesService categoriesService;
        public CategoriesServiceTest()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new LoWaiLoDbContext(options);

            this.categoriesRepository = new DbRepository<Category>(dbContext);
            this.categoriesService = new CategoriesService(categoriesRepository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateCategorySuccesully()
        {
            await this.categoriesService.CreateAsync("Супи", "https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1");

            var resultCount = categoriesService.All().Count();
            var resultName = categoriesService.All().First().Name;
            Assert.Equal(1, resultCount);
            Assert.Equal("Супи", resultName);
        }

        [Fact]
        public async Task AllShouldReturnCorrectValues()
        {
            var categories = new List<Category>();
            var appetizers = new Category
            {
                Name = "Десерти",
                Image = "https://www.dropbox.com/s/r1qx7zp4zja7azt/sm_01.jpg?dl=0",
            };
            categories.Add(appetizers);
            var soups = new Category
            {
                Name = "Супи",
                Image = "https://www.dropbox.com/s/x8nge5s4s919lzb/sm_02.jpg?dl=0",
            };
            categories.Add(soups);
            await categoriesService.CreateRangeAsync(categories);

            var resultCount = categoriesService.All().Count();
            var resultNameFirst = categoriesService.All().First().Name;
            var resultNameLast = categoriesService.All().Last().Name;

            Assert.Equal(2, resultCount);
            Assert.Equal("Десерти", resultNameFirst);
            Assert.Equal("Супи", resultNameLast);
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {
            var result = categoriesService.Any();
             Assert.False(result);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            await categoriesService.CreateAsync("Супи", "https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1");

            var result = categoriesService.Any();

            Assert.True(result);
        }

        [Fact]
        public async Task FindByIdShouldReturnCorrectValues()
        {
            var categories = new List<Category>();
            var appetizers = new Category
            {
                Id=1,
                Name = "Десерти",
                Image = "https://www.dropbox.com/s/r1qx7zp4zja7azt/sm_01.jpg?dl=0",
            };
            categories.Add(appetizers);
            var soups = new Category
            {
                Id=2,
                Name = "Супи",
                Image = "https://www.dropbox.com/s/x8nge5s4s919lzb/sm_02.jpg?dl=0",
            };
            categories.Add(soups);
            await categoriesService.CreateRangeAsync(categories);

            var result1 = categoriesService.FindById(1).Name;
            var result2 = categoriesService.FindById(2).Name;

            Assert.Equal("Десерти", result1);
            Assert.Equal("Супи", result2);
        }

        [Fact]
        public async Task FindByNameShouldReturnCorrectValues()
        {
            var categories = new List<Category>();
            var appetizers = new Category
            {
                Name = "Десерти",
                Image = "https://www.dropbox.com/s/r1qx7zp4zja7azt/sm_01.jpg?dl=0",
            };
            categories.Add(appetizers);
            var soups = new Category
            {
                Name = "Супи",
                Image = "https://www.dropbox.com/s/x8nge5s4s919lzb/sm_02.jpg?dl=0",
            };
            categories.Add(soups);
            await categoriesService.CreateRangeAsync(categories);

            var result1 = categoriesService.FindByName("Десерти").Name;
            var result2 = categoriesService.FindByName("Супи").Name;

            Assert.Equal("Десерти", result1);
            Assert.Equal("Супи", result2);
        }

        [Fact]
        public void FindByNameShoudReturnNull()
        {
            var category = categoriesService.FindByName("Супи");

            Assert.Null(category);
        }

        [Fact]
        public void FindByIdShouldReturnNull()
        {
            var category = categoriesService.FindById(1);

            Assert.Null(category);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCategory_Succesfully()
        {
            string name = "Супи";
            string imgUrl = "https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1";
            await this.categoriesService.CreateAsync(name, imgUrl);

            var category = this.categoriesRepository.All().First();

            Assert.Equal("Супи", category.Name);
            Assert.Equal("https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1", category.Image);

            string newName = "Десерти";
            string newUrl = "https://www.dropbox.com/s/x8nge5s4s919lzb/sm_02.jpg?dl=0";

            var result = await this.categoriesService.Update(category.Id, newName, newUrl);

            Assert.Equal("Десерти", result.Name);
            Assert.Equal("https://www.dropbox.com/s/x8nge5s4s919lzb/sm_02.jpg?dl=0", result.Image);

        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCategory_Succesfully()
        {
            string name = "Супи";
            string imgUrl = "https://www.dropbox.com/s/qmokaivtr9zel24/sm-01.jpg?raw=1";
            await this.categoriesService.CreateAsync(name, imgUrl);

            var category = this.categoriesRepository.All().First();
            Assert.NotNull(category);

            var result1 = await this.categoriesService.DeleteAsync(category.Id);
            Assert.True(result1);

            var result2 = this.categoriesRepository.All().FirstOrDefault();
            Assert.Null(result2);

        }
    }
}
