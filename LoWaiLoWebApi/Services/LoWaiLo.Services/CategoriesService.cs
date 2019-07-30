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

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly IMapper mapper;

        public CategoriesService(IRepository<Category> categoriesRepository, IMapper mapper)
        {
            this.categoriesRepository = categoriesRepository;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryDto> All()
        {
            return this.categoriesRepository
                .All()
                .Select(c => this.mapper.Map<CategoryDto>(c))
                .OrderBy(c => c.Name);
        }

        public bool Any()
        {
            return this.categoriesRepository.All().Any();
        }

        public async Task CreateAsync(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public CategoryDto FindById(int categoryId)
        {
            return this.categoriesRepository
                 .All()
                 .Select(c => this.mapper.Map<CategoryDto>(c))
                 .FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
