namespace LoWaiLo.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartService : IShoppingCartService
    {
        private const int DefaultQuantity = 1;

        private readonly IRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IRepository<ShoppingCartAddon> shoppingCartAddonsRepository;
        private readonly IProductsService productsService;
        private readonly IAddonsService addonsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartService(
            IRepository<ShoppingCartProduct> shoppingCartProducts,
            IProductsService productsService,
            IRepository<ShoppingCartAddon> shoppingCartAddonsRepository,
            IAddonsService addonsService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartProductsRepository = shoppingCartProducts;
            this.shoppingCartAddonsRepository = shoppingCartAddonsRepository;
            this.productsService = productsService;
            this.addonsService = addonsService;
            this.userManager = userManager;
        }

        public async Task AddProductInCart(int productId, string userId, int? quantity = null)
        {
            var product = this.productsService.GetProductById(productId);

            var user = this.userManager
                .FindByIdAsync(userId)
                .GetAwaiter()
                .GetResult();

            if (product == null || user == null)
            {
                return;
            }

            var shoppingCartProduct = this.GetShoppingCartProduct(productId, user.ShoppingCartId);

            if (shoppingCartProduct != null)
            {
                return;
            }

            shoppingCartProduct = new ShoppingCartProduct
            {
                Product = product,
                Quantity = quantity == null ? DefaultQuantity : quantity.Value,
                ShoppingCartId = user.ShoppingCartId,
            };

            await this.shoppingCartProductsRepository.AddAsync(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public async Task AddAddonInCart(int addonId, string userId, int? quantity = null)
        {
            var addon = this.addonsService.GetAddonById(addonId);

            var user = this.userManager
            .FindByIdAsync(userId)
            .GetAwaiter()
            .GetResult();

            if (addon == null || user == null)
            {
                return;
            }

            var shoppingCartAddon = new ShoppingCartAddon
            {
                Addon = addon,
                Quantity = quantity == null ? DefaultQuantity : quantity.Value,
                ShoppingCartId = user.ShoppingCartId,
            };

            await this.shoppingCartAddonsRepository.AddAsync(shoppingCartAddon);
            await this.shoppingCartAddonsRepository.SaveChangesAsync();
        }

        public bool AnyProdicts(string userId)
        {
            return this.shoppingCartProductsRepository.All().Any(x => x.ShoppingCart.User.Id == userId);
        }

        public bool AnyAddons(string userId)
        {
            return this.shoppingCartAddonsRepository.All().Any(x => x.ShoppingCart.User.Id == userId);
        }

        public async Task DeleteProdictFromCart(int productId, string userId)
        {
            var product = this.productsService.GetProductById(productId);

            var user = this.userManager
               .FindByIdAsync(userId)
               .GetAwaiter()
               .GetResult();

            if (product == null || user == null)
            {
                return;
            }

            var shoppingCartProduct = this.GetShoppingCartProduct(productId, user.ShoppingCartId);

            this.shoppingCartProductsRepository.Delete(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public async Task DeleteAddonFromCart(int addonId, string userId)
        {
            var addon = this.addonsService.GetAddonById(addonId);

            var user = this.userManager
             .FindByIdAsync(userId)
             .GetAwaiter()
             .GetResult();

            if (addon == null || user == null)
            {
                return;
            }

            var shoppingCartAddon = this.GetShoppingCartAddon(addonId, user.ShoppingCartId);

            this.shoppingCartAddonsRepository.Delete(shoppingCartAddon);
            await this.shoppingCartAddonsRepository.SaveChangesAsync();
        }

        public async Task UpdateShoppingCartProductQuantity(int productId, string userId, int quantity)
        {
            var product = this.productsService.GetProductById(productId);

            var user = this.userManager
             .FindByIdAsync(userId)
             .GetAwaiter()
             .GetResult();
            if (product == null || user == null || quantity <= 0)
            {
                return;
            }

            var shoppingCartProduct = this.GetShoppingCartProduct(productId, user.ShoppingCartId);

            if (shoppingCartProduct == null)
            {
                return;
            }

            shoppingCartProduct.Quantity = quantity;

            this.shoppingCartProductsRepository.Update(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public async Task UpdateShoppingCartAddonQuantity(int addonId, string userId, int quantity)
        {
            var addon = this.addonsService.GetAddonById(addonId);

            var user = this.userManager
              .FindByIdAsync(userId)
              .GetAwaiter()
              .GetResult();
            if (addon == null || user == null || quantity <= 0)
            {
                return;
            }

            var shoppingCartAddon = this.GetShoppingCartAddon(addonId, user.ShoppingCartId);

            if (shoppingCartAddon == null)
            {
                return;
            }

            this.shoppingCartAddonsRepository.Update(shoppingCartAddon);
            await this.shoppingCartAddonsRepository.SaveChangesAsync();
        }

        public IQueryable<ShoppingCartProduct> GetAllShoppingCartProducts(string userId)
        {
            var user = this.userManager
              .FindByIdAsync(userId)
              .GetAwaiter()
              .GetResult();

            if (user == null)
            {
                return null;
            }

            return this.shoppingCartProductsRepository
                .All()
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.ShoppingCart)
                .Where(x => x.ShoppingCart.User.Id == userId);
        }

        public IQueryable<ShoppingCartAddon> GetShoppingCartAddons(string userId)
        {
            var user = this.userManager
                .FindByIdAsync(userId)
                .GetAwaiter()
                .GetResult();

            if (user == null)
            {
                return null;
            }

            return this.shoppingCartAddonsRepository
                .All()
                .AsNoTracking()
                .Include(x => x.Addon)
                .Include(x => x.ShoppingCart)
                .Where(x => x.ShoppingCart.User.Id == userId);
        }

        private ShoppingCartProduct GetShoppingCartProduct(int productId, int shoppingCartId)
        {
            return this.shoppingCartProductsRepository.All().FirstOrDefault(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        }

        private ShoppingCartAddon GetShoppingCartAddon(int addonId, int shoppingCartId)
        {
            return this.shoppingCartAddonsRepository.All().FirstOrDefault(x => x.ShoppingCartId == shoppingCartId && x.AddonId == addonId);
        }
    }
}
