namespace LoWaiLo.WebAPI.Helpers
{
    using System.Linq;
    using AutoMapper;
    using LoWaiLo.Data.Models;

    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            // this.CreateMap<Category, CategoryDto>();

            // this.CreateMap<Product, ProductDto>()
            //   .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Select(l => l.User)));

            // this.CreateMap<Addon, AddonDto>();

            // this.CreateMap<OrderProductDto, OrderProduct>()
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Id, opt => opt.Ignore());

            // this.CreateMap<OrderProduct, OrderProductDto>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name));

            // this.CreateMap<Order, OrderDto>()
            //   .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Customer.Email))
            //   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            //   .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));
        }
    }
}
