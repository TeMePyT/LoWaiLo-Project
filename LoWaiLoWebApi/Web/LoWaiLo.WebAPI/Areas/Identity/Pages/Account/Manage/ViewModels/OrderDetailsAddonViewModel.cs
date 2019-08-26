namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels
{
    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class OrderDetailsAddonViewModel : IMapFrom<OrderAddon>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderAddon, OrderDetailsAddonViewModel>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddonId))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Addon.Name))
                  .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Price));
        }
    }
}
