namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LoWaiLo.Data.Models;

    public interface IProductsService
    {
        IEnumerable<Product> All();

        bool Any();

        Task AddAsync(Product product);

        Task AddRangeAsync(IEnumerable<Product> products);

        Task EditAsync(Product product);

        Task DeleteAsync(int productId);

        Product GetProductById(int productId);

        bool Exists(int productId);
    }
}
