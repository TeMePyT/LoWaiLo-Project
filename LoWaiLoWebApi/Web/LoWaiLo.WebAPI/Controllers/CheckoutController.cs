namespace LoWaiLo.WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Checkout;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.ShoppingCart;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CheckoutController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrdersService ordersService;
        private readonly UserManager<ApplicationUser> userManager;

        public CheckoutController(
            IShoppingCartService shoppingCartService,
            IOrdersService ordersService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(CheckoutInputModel model)
        {
            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            if (user == null)
            {
                this.TempData["ErrorMessage"] = $"Потребител с име {this.User.Identity.Name} не беше намерен.";
                return this.RedirectToAction("Index", "Home");
            }

            model.Address = user.Address;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.PhoneNumber = user.PhoneNumber;

            var cartProducts = await this.shoppingCartService.GetAllShoppingCartProducts(user.Id).Select(x => new ShoppingCartProductViewModel
            {
                Id = x.ProductId,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity,
                TotalPrice = x.Quantity * x.Product.Price,
            }).ToListAsync();

            var cartAddons = await this.shoppingCartService.GetShoppingCartAddons(user.Id).Select(x => new ShoppingCartAddonViewModel
            {
                Id = x.AddonId,
                Name = x.Addon.Name,
                Price = x.Addon.Price,
                Quantity = x.Quantity,
                TotalPrice = x.Quantity * x.Addon.Price,
            }).ToListAsync();

            if (cartProducts.Count == 0)
            {
                return this.RedirectToAction("Index", "Menu");
            }

            model.ShoppingCart = new ShoppingCartViewModel();
            model.ShoppingCart.Products = cartProducts;
            model.ShoppingCart.Addons = cartAddons;
            var productsTotal = cartProducts.Select(x => x.TotalPrice).Sum();
            var addonsTotal = cartAddons.Select(x => x.TotalPrice).Sum();
            var orderTotal = productsTotal + addonsTotal;
            model.ShoppingCart.ProductsTotal = orderTotal;
            model.ShoppingCart.DeliveryPrice = orderTotal < 10 ? 2 : 1;

            model.ShoppingCart.ShoppingCartTotal = orderTotal + model.ShoppingCart.DeliveryPrice;

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                var firstName = model.FirstName;
                if (user.FirstName == null)
                {
                    user.FirstName = firstName;
                    await this.userManager.UpdateAsync(user);
                }

                var lastName = model.LastName;
                if (user.LastName == null)
                {
                    user.LastName = lastName;
                    await this.userManager.UpdateAsync(user);
                }

                var phoneNumber = model.PhoneNumber;
                if (user.PhoneNumber == null)
                {
                    user.PhoneNumber = phoneNumber;
                    await this.userManager.UpdateAsync(user);
                }

                var address = model.Address;
                if (user.Address == null)
                {
                    user.Address = address;
                }

                var products = this.shoppingCartService.GetAllShoppingCartProducts(user.Id).AsEnumerable();
                var addons = this.shoppingCartService.GetShoppingCartAddons(user.Id).AsEnumerable();

                try
                {
                    await this.ordersService.CreateOrderAsync(user.Id, address, phoneNumber, products, addons);
                    await this.shoppingCartService.ClearShoppingCart(user.Id);
                    return this.Redirect(nameof(this.Confirmation));
                }
                catch (Exception)
                {
                    this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Index", "Checkout");
                }
            }
            else
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                return this.RedirectToAction(nameof(this.Index), model);
            }
        }

        public IActionResult Confirmation()
        {
            return this.View();
        }
    }
}