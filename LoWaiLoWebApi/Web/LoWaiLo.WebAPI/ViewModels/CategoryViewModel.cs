namespace LoWaiLo.WebAPI.ViewModels
{
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.Services.Models;

    public class CategoryViewModel : IMapTo<CategoryDto>, IMapFrom<CategoryDto>
    {
        public string Name { get; set; }
    }
}
