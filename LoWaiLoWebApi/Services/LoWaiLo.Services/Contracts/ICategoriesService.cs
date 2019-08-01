namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LoWaiLo.Data.Models;

    public interface ICategoriesService
    {
        IEnumerable<Category> All();

        bool Any();

        Task CreateRangeAsync(string[] categoriesName);

        Task CreateAsync(string categoryName);

        Category FindById(int categoryId);

        Category FindByName(string categoryName);
    }
}
