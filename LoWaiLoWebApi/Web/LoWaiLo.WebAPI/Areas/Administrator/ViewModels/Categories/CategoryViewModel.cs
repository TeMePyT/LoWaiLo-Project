namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Categories
{
    using System.Linq;

    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int ProductsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                 .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Count()));
        }
    }
}
