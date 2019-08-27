namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using LoWaiLo.Data.Models.Enums;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Home;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdministratorController
    {
        private readonly IOrdersService ordersService;

        public HomeController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();

            var perndingOrders = this.ordersService.GetOrdersByStatus(OrderStatus.Pending).To<OrderViewModel>();
            model.PendingOrders = perndingOrders;

            var approvedOrders = this.ordersService.GetOrdersByStatus(OrderStatus.Approved).To<OrderViewModel>();
            model.ApprovedOrders = approvedOrders;

            return this.View(model);
        }
    }
}