namespace LoWaiLo.WebAPI.Components
{
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.WebAPI.Helpers;
    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.ShoppingCart;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartComponent : ViewComponent
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartComponent(
            IShoppingCartService shoppingCartService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
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

                var viewModel = new ShoppingCartViewModel();
                viewModel.Products = cartProducts;
                viewModel.Addons = cartAddons;
                var productsTotal = cartProducts.Select(x => x.TotalPrice).Sum();
                var addonsTotal = cartAddons.Select(x => x.TotalPrice).Sum();
                var orderTotal = productsTotal + addonsTotal;
                viewModel.ProductsTotal = orderTotal;
                if (orderTotal == 0)
                {
                    viewModel.DeliveryPrice = 0;
                }
                else
                {
                    viewModel.DeliveryPrice = orderTotal < 10 ? 2 : 1;
                }

                viewModel.ShoppingCartTotal = orderTotal + viewModel.DeliveryPrice;
                return await Task.FromResult((IViewComponentResult)this.View("Default", viewModel));
            }

            ShoppingCartViewModel sessionModel = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
            if (sessionModel == null)
            {
                return await Task.FromResult((IViewComponentResult)this.View("Default", new ShoppingCartViewModel()));
            }

            var sessionProductsTotal = sessionModel.Products.Select(x => x.TotalPrice).Sum();
            var sessionAddonsTotal = sessionModel.Addons.Select(x => x.TotalPrice).Sum();
            var sessionTotal = sessionAddonsTotal + sessionProductsTotal;
            sessionModel.ProductsTotal = sessionTotal;
            if (sessionTotal == 0)
            {
                sessionModel.DeliveryPrice = 0;
            }
            else
            {
                sessionModel.DeliveryPrice = sessionTotal < 10 ? 2 : 1;
            }

            sessionModel.ShoppingCartTotal = sessionAddonsTotal + sessionProductsTotal + sessionModel.DeliveryPrice;
            return await Task.FromResult((IViewComponentResult)this.View("Default", sessionModel));
        }
    }
}
