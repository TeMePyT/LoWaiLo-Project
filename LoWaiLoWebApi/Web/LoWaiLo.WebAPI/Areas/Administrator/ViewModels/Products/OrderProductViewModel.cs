namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products
{
    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class OrderProductViewModel : IMapFrom<OrderProduct>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderProduct, OrderProductViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
