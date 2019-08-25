namespace LoWaiLo.WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using LoWaiLo.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.WebAPI.Helpers;
    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.ShoppingCart;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : Controller
    {
        private const int DefaultQuantity = 1;

        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductsService productsService;
        private readonly IAddonsService addonsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(
            IShoppingCartService shoppingCartService,
            IProductsService productsService,
            IAddonsService addonsService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.productsService = productsService;
            this.addonsService = addonsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddProduct(int id, int? quantity)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                try
                {
                    await this.shoppingCartService.AddProductInCart(id, user.Id, quantity);
                }
                catch (Exception)
                {
                    this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Error", "Home");
                }
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    session = new ShoppingCartViewModel();
                }

                if (!session.Products.Any(x => x.Id == id))
                {
                    var product = Mapper.Map<ShoppingCartProductViewModel>(this.productsService.GetProductById(id));
                    product.Quantity = quantity == null ? DefaultQuantity : quantity.Value;
                    product.TotalPrice = product.Quantity * product.Price;

                    session.Products.Add(product);

                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            return this.RedirectToAction("Index", "Menu");
        }

        public async Task<IActionResult> AddAddon(int id, int? quantity)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                try
                {
                    await this.shoppingCartService.AddAddonInCart(id, user.Id, quantity);
                }
                catch (Exception)
                {
                    this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Error", "Home");
                }
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    session = new ShoppingCartViewModel();
                }

                if (!session.Addons.Any(x => x.Id == id))
                {
                    var addon = Mapper.Map<ShoppingCartAddonViewModel>(this.addonsService.GetAddonById(id));
                    addon.Quantity = quantity == null ? DefaultQuantity : quantity.Value;
                    addon.TotalPrice = addon.Quantity * addon.Price;

                    session.Addons.Add(addon);

                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            return this.RedirectToAction("Index", "Menu");
        }

        public async Task<IActionResult> DeleteProduct(int id, string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                await this.shoppingCartService.DeleteProdictFromCart(id, user.Id);
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    this.ViewBag.ErrorMessage = "Вашата количка е празна.";
                    return this.RedirectToAction("Error", "Home");
                }

                if (session.Products.Any(x => x.Id == id))
                {
                    var product = session.Products.First(x => x.Id == id);
                    session.Products.Remove(product);
                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Menu");
            }
        }

        public async Task<IActionResult> DeleteAddon(int id, string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                await this.shoppingCartService.DeleteAddonFromCart(id, user.Id);
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    this.TempData["ErrorMessage"] = "Вашата количка е празна.";
                    return this.RedirectToAction("Error", "Home");
                }

                if (session.Addons.Any(x => x.Id == id))
                {
                    var addon = session.Addons.First(x => x.Id == id);
                    session.Addons.Remove(addon);
                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Menu");
            }
        }

        public async Task<IActionResult> EditProductQuantity(int id, int quantity, string returnUrl = null)
        {
            if (quantity <= 0)
            {
                return this.RedirectToAction(nameof(this.DeleteProduct), new { id, returnUrl });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                await this.shoppingCartService.UpdateShoppingCartProductQuantity(id, user.Id, quantity);
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    this.ViewBag.ErrorMessage = "Вашата количка е празна.";
                    return this.RedirectToAction("Error", "Home");
                }

                if (session.Products.Any(x => x.Id == id))
                {
                    var product = session.Products.First(x => x.Id == id);
                    product.Quantity = quantity;
                    product.TotalPrice = quantity * product.Price;
                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Menu");
            }
        }

        public async Task<IActionResult> EditAddonQuantity(int id, int quantity, string returnUrl = null)
        {
            if (quantity <= 0)
            {
                return this.RedirectToAction(nameof(this.DeleteAddon), new { id, returnUrl });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                await this.shoppingCartService.UpdateShoppingCartAddonQuantity(id, user.Id, quantity);
            }
            else
            {
                ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
                if (session == null)
                {
                    this.ViewBag.ErrorMessage = "Вашата количка е празна.";
                    return this.RedirectToAction("Error", "Home");
                }

                if (session.Addons.Any(x => x.Id == id))
                {
                    var addon = session.Addons.First(x => x.Id == id);
                    addon.Quantity = quantity;
                    addon.TotalPrice = quantity * addon.Price;
                    SessionHelper.SetObjectAsJson(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey, session);
                }
            }

            if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("Index", "Menu");
            }
        }
    }
}