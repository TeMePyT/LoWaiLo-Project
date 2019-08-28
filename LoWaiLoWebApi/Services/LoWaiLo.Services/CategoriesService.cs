namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IQueryable<Category> All()
        {
            return this.categoriesRepository
                .All();
        }

        public bool Any()
        {
            return this.categoriesRepository.All().Any();
        }

        public async Task CreateRangeAsync(IEnumerable<Category> categories)
        {
            await this.categoriesRepository.AddRangeAsync(categories);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(string categoryName, string imgUrl)
        {
            var category = new Category
            {
                Name = categoryName,
                Image = imgUrl,
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

        public async Task<Category> Update(int categoryId, string categoryName, string imgUrl)
        {
            var category = this.categoriesRepository
                .All()
                .First(x => x.Id == categoryId);
            if (category.Name != categoryName)
            {
                category.Name = categoryName;
                this.categoriesRepository.Update(category);
                await this.categoriesRepository.SaveChangesAsync();
            }

            if (category.Image != imgUrl)
            {
                category.Image = imgUrl;
                this.categoriesRepository.Update(category);
                await this.categoriesRepository.SaveChangesAsync();
            }

            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = this.categoriesRepository
                 .All()
                 .Include(x => x.Products)
                 .FirstOrDefault(x => x.Id == id);

            if (category == null || category.Products.Count() != 0)
            {
                return false;
            }

            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
            return true;
        }
    }
}
