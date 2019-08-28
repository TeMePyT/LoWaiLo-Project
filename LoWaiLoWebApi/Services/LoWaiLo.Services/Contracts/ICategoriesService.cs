namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> All();

        bool Any();

        Task CreateRangeAsync(IEnumerable<Category> categories);

        Task CreateAsync(string categoryName, string imgUrl);

        Category FindById(int categoryId);

        Category FindByName(string categoryName);

        Task<Category> Update(int categoryId, string categoryName, string imgUrl);

        Task<bool> DeleteAsync(int id);
    }
}
