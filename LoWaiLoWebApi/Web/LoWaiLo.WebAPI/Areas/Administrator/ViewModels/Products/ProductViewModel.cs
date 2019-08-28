namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products
{
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
