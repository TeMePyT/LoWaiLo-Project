namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Addons;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products;

    using LoWaiLo.WebAPI.Helpers;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderViewModel()
        {
            this.OrderProducts = new List<OrderProductViewModel>();
            this.OrderAddons = new List<OrderAddonViewModel>();
        }

        public string Id { get; set; }

        public int OrderNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CustomerFullName => $"{this.FirstName} {this.LastName}";

        public string Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? ShippedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public IEnumerable<OrderProductViewModel> OrderProducts { get; set; }

        public IEnumerable<OrderAddonViewModel> OrderAddons { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                 .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => (src.Customer.FirstName + " " + src.Customer.LastName)))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
        }
    }
}
