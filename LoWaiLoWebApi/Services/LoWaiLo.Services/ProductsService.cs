namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.EntityFrameworkCore;

    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> productsRepository;

        public ProductsService(IRepository<Product> productRepository)
        {
            this.productsRepository = productRepository;
        }

        public bool Any()
        {
            return this.productsRepository.All().Any();
        }

        public async Task AddAsync(Product product)
        {
            await this.productsRepository.AddAsync(product);

            await this.productsRepository.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Product> products)
        {
            await this.productsRepository.AddRangeAsync(products);
            await this.productsRepository.SaveChangesAsync();
        }

        public IQueryable<Product> All()
        {
            return this.productsRepository
                 .All()
                 .AsNoTracking()
                 .Include(c => c.Category)
                 .Include(l => l.Likes)
                 .ThenInclude(u => u.User)
                 .Include(r => r.Reviews);
        }

        public async Task DeleteAsync(int productId)
        {
            var product = this.productsRepository
                .All()
                .First(p => p.Id == productId);

            this.productsRepository.Delete(product);

            await this.productsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public Product GetProductById(int productId)
        {
            return this.productsRepository
                .All()
                .First(p => p.Id == productId);
        }

        public bool Exists(int productId)
        {
            return this.productsRepository
               .All()
               .Any(p => p.Id == productId);
        }
    }
}
