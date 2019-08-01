namespace LoWaiLo.WebAPI.ViewModels
{
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class CategoryViewModel : IMapTo<Category>, IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
