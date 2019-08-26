namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels
{
    using System;
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Models.Enums;
    using LoWaiLo.Services.Mapping;
    using XeonComputers;

#pragma warning disable SA1649 // File name should match first type name
    public class OrderListViewModel : IMapFrom<Order>, IHaveCustomMappings
#pragma warning restore SA1649 // File name should match first type name
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderListViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));

        }
    }
}
