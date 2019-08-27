namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Helpers;

    public class OrderDetailsViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderDetailsViewModel()
        {
            this.Products = new List<OrderDetailsProductViewModel>();
            this.Addons = new List<OrderDetailsAddonViewModel>();
        }

        public string Id { get; set; }

        public int OrderNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? ShippedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<OrderDetailsProductViewModel> Products { get; set; }

        public ICollection<OrderDetailsAddonViewModel> Addons { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderDetailsViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts))
                .ForMember(dest => dest.Addons, opt => opt.MapFrom(src => src.OrderAddons))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
        }
    }
}
