namespace LoWaiLo.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ProductDto : IMapTo<Product>, IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public IEnumerable<ApplicationUser> Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductDto, Product>()
                  .ForMember(dest => dest.Category, opt => opt.Ignore())
                 .ForMember(dest => dest.Likes, opt => opt.Ignore());

            configuration.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Select(l => l.User)));
        }
    }
}
