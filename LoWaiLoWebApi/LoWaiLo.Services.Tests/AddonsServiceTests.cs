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

    using Xunit;

    public class AddonsServiceTests
    {
        private readonly IRepository<Addon> addonsRepository;
        private readonly AddonsService addonsService;
        private readonly LoWaiLoDbContext context;

        public AddonsServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            this.context = new LoWaiLoDbContext(options);

            this.addonsRepository = new DbRepository<Addon>(context);
            this.addonsService = new AddonsService(addonsRepository);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAddon_Succesfully()
        {
            var addon = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 0.50m
            };

            await addonsService.AddAsync(addon);
            var countResult = this.addonsRepository.All().ToList().Count();
            Assert.Equal(1, countResult);

            var addonResult = this.addonsRepository.All().First();

            Assert.Equal(567, addonResult.Id);
            Assert.Equal("Клечки", addonResult.Name);
            Assert.Equal("1 бр.", addonResult.Description);
            Assert.Equal(0.50m, addonResult.Price);
        }

        [Fact]
        public async Task EditAsync_ShouldEditAddon_Succesfully()
        {
            var addon = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 0.50m
            };
            await addonsService.AddAsync(addon);
            context.Entry(addon).State = EntityState.Detached;

            var edditedAddon = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 1.50m
            };
            await this.addonsService.EditAsync(edditedAddon);

            var edditedResult = this.addonsRepository.All().AsNoTracking().First();

            Assert.Equal(1.50m, edditedResult.Price);
        }

        [Fact]
        public void All_ShouldReturn_EmptyCollection()
        {
            var result = this.addonsService.All().ToList();

            Assert.Empty(result);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddRange_Succesfully()
        {
            var addons = new List<Addon>();

            var addon1 = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 0.50m
            };
            addons.Add(addon1);

            var addon2 = new Addon
            {
                Id = 765,
                Name = "Соев сос",
                Description = "80 гр.",
                Price = 0.80m
            };
            addons.Add(addon2);

            await this.addonsService.AddRangeAsync(addons);

            var allAddons = this.addonsService.All().ToList();

            Assert.Equal(2, allAddons.Count);
        }

        [Fact]
        public async Task All_ShouldReturn_CorrectValues()
        {
            var addons = new List<Addon>();

            var addon1 = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 0.50m
            };
            addons.Add(addon1);

            var addon2 = new Addon
            {
                Id = 765,
                Name = "Соев сос",
                Description = "80 гр.",
                Price = 0.80m
            };
            addons.Add(addon2);

            await this.addonsService.AddRangeAsync(addons);

            var allAddons = this.addonsService.All().ToList();

            Assert.Equal(2, allAddons.Count);

            var result1 = allAddons.First();

            Assert.Equal(567, result1.Id);
            Assert.Equal("Клечки", result1.Name);
            Assert.Equal(0.50m, result1.Price);

            var result2 = allAddons.Last();

            Assert.Equal(765, result2.Id);
            Assert.Equal("Соев сос", result2.Name);
            Assert.Equal(0.80m, result2.Price);
        }

        [Fact]
        public void Any_ShouldReturn_False()
        {
            var result = this.addonsService.Any();

            Assert.False(result);
        }

        [Fact]
        public async Task Any_ShouldReturn_True()
        {
            var addon = new Addon
            {
                Id = 567,
                Name = "Клечки",
                Description = "1 бр.",
                Price = 0.50m
            };
            await addonsService.AddAsync(addon);

            var result = this.addonsService.Any();

            Assert.True(result);
        }
    }
}
