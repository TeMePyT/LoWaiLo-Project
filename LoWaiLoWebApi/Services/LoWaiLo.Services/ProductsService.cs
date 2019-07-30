namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Models;

    using Microsoft.EntityFrameworkCore;

    public class ProductsService : IProductService
    {
        private readonly IRepository<Product> productsRepository;

        private readonly IMapper mapper;

        public ProductsService(IRepository<Product> productRepository, IMapper mapper)
        {
            this.productsRepository = productRepository;

            this.mapper = mapper;
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var product = this.mapper.Map<Product>(productDto);

            await this.productsRepository.AddAsync(product);

            await this.productsRepository.SaveChangesAsync();
        }

        public IEnumerable<ProductDto> All()
        {
            return this.productsRepository
                 .All()
                 .AsNoTracking()
                 .Include(c => c.Category)
                 .Include(l => l.Likes)
                 .ThenInclude(u => u.User)
                 .Select(p => this.mapper.Map<ProductDto>(p));
        }

        public async Task DeleteAsync(int productId)
        {
            var product = this.productsRepository
                .All()
                .First(p => p.Id == productId);

            this.productsRepository.Delete(product);

            await this.productsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(ProductDto productDto)
        {
            var product = this.mapper
                  .Map<Product>(productDto);

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public bool Exists(int productId)
        {
            return this.productsRepository
               .All()
               .Any(p => p.Id == productId);
        }
    }
}
