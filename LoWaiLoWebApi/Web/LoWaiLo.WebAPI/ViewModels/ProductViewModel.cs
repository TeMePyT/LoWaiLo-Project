namespace LoWaiLo.WebAPI.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>, IMapTo<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name="Номер в менюто")]
        public int MenuNumber { get; set; }

        [Display(Name = "Тегло")]
        public int Weight { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        public IEnumerable<string> Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
           configuration.CreateMap<Product, ProductViewModel>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                 .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Select(u => u.User.UserName)));
        }
    }
}
