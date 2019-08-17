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

    public class NavbarBasketComponent : ViewComponent
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public NavbarBasketComponent(IShoppingCartService shoppingCartService, UserManager<ApplicationUser> userManager)
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

                var viewModel = new ShoppingCartMenuViewModel();
                viewModel.Count = cartAddons.Count() + cartProducts.Count();
                var productsTotal = cartProducts.Select(x => x.TotalPrice).Sum();
                var addonsTotal = cartAddons.Select(x => x.TotalPrice).Sum();
                viewModel.TotalPrice = productsTotal + addonsTotal;
                return await Task.FromResult((IViewComponentResult)this.View("Default", viewModel));
            }

            var model = new ShoppingCartMenuViewModel();
            ShoppingCartViewModel session = SessionHelper.GetObjectFromJson<ShoppingCartViewModel>(this.HttpContext.Session, GlobalConstants.SessionShoppingCartKey);
            if (session == null)
            {
                return await Task.FromResult((IViewComponentResult)this.View("Default", model));
            }

            var sessionProductsTotal = session.Products.Select(x => x.TotalPrice).Sum();
            var sessionProductsCount = session.Products.Count();
            var sessionAddonsTotal = session.Addons.Select(x => x.TotalPrice).Sum();
            var sessionAddonsCount = session.Addons.Count();
            model.Count = sessionAddonsCount + sessionProductsCount;
            model.TotalPrice = sessionProductsTotal + sessionAddonsTotal;
            return await Task.FromResult((IViewComponentResult)this.View("Default", model));
        }
    }
}
