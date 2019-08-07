namespace LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ReviewViewModel : IMapFrom<ProductReview>, IMapFrom<SiteReview>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        [Display(Name="Последно коригиран на: ")]
        public DateTime LastModified { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductReview, ReviewViewModel>()
                 .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.UserName));

            configuration.CreateMap<SiteReview, ReviewViewModel>()
                 .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.UserName));
        }
    }
}
