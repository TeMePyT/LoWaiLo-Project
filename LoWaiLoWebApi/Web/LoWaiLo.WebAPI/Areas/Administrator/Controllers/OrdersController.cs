namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using LoWaiLo.Data.Models.Enums;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : AdministratorController
    {
        private const string ErrorMessageInvalidOrderId = "Невалиден номер на поръчка, моля опитайте отново!";
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

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
    }
}