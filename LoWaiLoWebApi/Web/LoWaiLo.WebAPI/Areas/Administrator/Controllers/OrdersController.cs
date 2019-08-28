namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using LoWaiLo.Data.Models.Enums;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders;
    using LoWaiLo.WebAPI.Helpers;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministratorController
    {
        private const string ErrorMessageInvalidOrderId = "Невалиден номер на поръчка, моля опитайте отново!";
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(string id, string returnUrl = null)
        {
            var order = this.ordersService.GetOrderById(id);
            if (order == null)
            {
                this.TempData["ErrorMessage"] = ErrorMessageInvalidOrderId;
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            await this.ordersService.ProcessOrderAsync(id, OrderStatus.Canceled);

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string id, string returnUrl = null)
        {
            var order = this.ordersService.GetOrderById(id);
            if (order == null)
            {
                this.TempData["ErrorMessage"] = ErrorMessageInvalidOrderId;
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            if (order.Status != OrderStatus.Pending)
            {
                this.TempData["ErrorMessage"] = $"Не можете да приемете поръчка със статус: {order.Status.GetDisplayName()}";
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            await this.ordersService.ProcessOrderAsync(id, OrderStatus.Approved);

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Ship(string id, string returnUrl = null)
        {
            var order = this.ordersService.GetOrderById(id);
            if (order == null)
            {
                this.TempData["ErrorMessage"] = ErrorMessageInvalidOrderId;
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            if (order.Status != OrderStatus.Approved)
            {
                this.TempData["ErrorMessage"] = $"Не можете да изпратите поръчка със статус: {order.Status.GetDisplayName()}";
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            await this.ordersService.ProcessOrderAsync(id, OrderStatus.Shipping);

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Deliver(string id, string returnUrl = null)
        {
            var order = this.ordersService.GetOrderById(id);
            if (order == null)
            {
                this.TempData["ErrorMessage"] = ErrorMessageInvalidOrderId;
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            if (order.Status != OrderStatus.Shipping)
            {
                this.TempData["ErrorMessage"] = $"Не можете да доставите поръчка със статус: {order.Status.GetDisplayName()}";
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
                else
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }

            await this.ordersService.ProcessOrderAsync(id, OrderStatus.Delivered);

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var order = this.ordersService.GetOrderById(id);
            if (order == null)
            {
                this.TempData["ErrorMessage"] = ErrorMessageInvalidOrderId;
                return this.RedirectToAction("Index", "Home");
            }

            var model = Mapper.Map<OrderViewModel>(order);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Shipped()
        {
            var model = new ShippedOrdersViewModel();
            var shippedOrders = this.ordersService.GetOrdersByStatus(OrderStatus.Shipping).To<OrderViewModel>();
            model.ShippedOrders = shippedOrders;

            return this.View(model);
        }

        public IActionResult History()
        {
            var model = new HistoryViewModel();

            var deliveredOrders = this.ordersService.GetOrdersByStatus(OrderStatus.Delivered).To<OrderViewModel>();
            model.DeliveredOrders = deliveredOrders.OrderByDescending(x => x.CreatedOn);

            var canceledOrders = this.ordersService.GetOrdersByStatus(OrderStatus.Canceled).To<OrderViewModel>();
            model.CanceledOrders = canceledOrders.OrderByDescending(x => x.CreatedOn);

            return this.View(model);
        }
    }
}