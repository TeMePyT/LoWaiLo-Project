namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Models;

    public interface IProductsService
    {
        IEnumerable<ProductDto> All();

        Task AddAsync(ProductDto product);

        Task EditAsync(ProductDto product);

        Task DeleteAsync(int productId);

        bool Exists(int productId);
    }
}
