namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Models;

    public interface ICategoriesService
    {
        IEnumerable<CategoryDto> All();

        bool Any();

        Task CreateAsync(string categoryName);

        CategoryDto FindById(int categoryId);
    }
}
