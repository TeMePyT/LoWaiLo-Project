namespace LoWaiLo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartService : IShoppingCartService
    {
        private const int DefaultProductQuantity = 1;

        private readonly IRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartService(IRepository<ShoppingCartProduct> shoppingCartProducts, IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartProductsRepository = shoppingCartProducts;
            this.productsService = productsService;
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
                Quantity = quantity == null ? DefaultProductQuantity : quantity.Value,
                ShoppingCartId = user.ShoppingCartId,
            };

            await this.shoppingCartProductsRepository.AddAsync(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();
        }

        public bool AnyProdicts(string userId)
        {
            return this.shoppingCartProductsRepository.All().Any(x => x.ShoppingCart.User.Id == userId);
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

            var shoppingCartProduct = this.GetShoppingCartProduct(product.Id, user.ShoppingCartId);

            this.shoppingCartProductsRepository.Delete(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync();
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

        private ShoppingCartProduct GetShoppingCartProduct(int productId, int shoppingCartId)
        {
            return this.shoppingCartProductsRepository.All().FirstOrDefault(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        }
    }
}
