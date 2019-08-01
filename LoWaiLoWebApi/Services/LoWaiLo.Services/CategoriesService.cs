namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;;
        }

        public IEnumerable<Category> All()
        {
            return this.categoriesRepository
                .All()
                .OrderBy(c => c.Name);
        }

        public bool Any()
        {
            return this.categoriesRepository.All().Any();
        }

        public async Task CreateRangeAsync(string[] categoriesNames)
        {
            var categories = categoriesNames
                .Select(categoryName => new Category
                {
                    Name = categoryName,
                });

            await this.categoriesRepository.AddRangeAsync(categories);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public Category FindById(int categoryId)
        {
            return this.categoriesRepository
                 .All()
                 .FirstOrDefault(c => c.Id == categoryId);
        }

        public Category FindByName(string categoryName)
        {
            return this.categoriesRepository
                .All()
                .FirstOrDefault(c => c.Name == categoryName);
        }
    }
}
