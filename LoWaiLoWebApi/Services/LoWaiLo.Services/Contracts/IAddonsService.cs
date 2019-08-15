namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoWaiLo.Data.Models;

    public interface IAddonsService
    {
        IQueryable<Addon> All();

        bool Any();

        Task AddAsync(Addon addon);

        Task AddRangeAsync(IEnumerable<Addon> addons);

        Task EditAsync(Addon addon);

        Task DeleteAsync(int addonId);

        Addon GetAddonById(int addonId);

        bool Exists(int addonId);
    }
}
