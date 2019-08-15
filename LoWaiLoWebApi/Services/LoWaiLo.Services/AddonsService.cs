namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    public class AddonsService : IAddonsService
    {
        private readonly IRepository<Addon> addonsRepository;

        public AddonsService(IRepository<Addon> addonsRepository)
        {
            this.addonsRepository = addonsRepository;
        }

        public async Task AddAsync(Addon addon)
        {
            await this.addonsRepository.AddAsync(addon);

            await this.addonsRepository.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Addon> addons)
        {
            await this.addonsRepository.AddRangeAsync(addons);

            await this.addonsRepository.SaveChangesAsync();
        }

        public IQueryable<Addon> All()
        {
            return this.addonsRepository.All();
        }

        public bool Any()
        {
            return this.addonsRepository.All().Any();
        }

        public async Task DeleteAsync(int addonId)
        {
            var addon = this.addonsRepository
                 .All()
                 .First(a => a.Id == addonId);

            this.addonsRepository.Delete(addon);

            await this.addonsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(Addon addon)
        {
            this.addonsRepository.Update(addon);

            await this.addonsRepository.SaveChangesAsync();
        }

        public bool Exists(int addonId)
        {
            return this.addonsRepository
                 .All()
                 .Any(a => a.Id == addonId);
        }

        public Addon GetAddonById(int addonId)
        {
            return this.addonsRepository
                 .All()
                 .First(a => a.Id == addonId);
        }
    }
}
