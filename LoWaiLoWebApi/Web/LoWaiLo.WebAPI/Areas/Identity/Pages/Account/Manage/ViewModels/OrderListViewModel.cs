namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels
{
    using System;

    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    using LoWaiLo.WebAPI.Helpers;

    public partial class OrderListViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int OrderNumber { get; set; }

        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderListViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
        }
    }
}
