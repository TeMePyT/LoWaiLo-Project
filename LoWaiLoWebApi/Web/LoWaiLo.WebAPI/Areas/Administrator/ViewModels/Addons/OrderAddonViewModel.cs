namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Addons
{
    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class OrderAddonViewModel : IMapFrom<OrderAddon>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderAddon, OrderAddonViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Addon.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddonId));
        }
    }
}
