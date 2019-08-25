namespace LoWaiLo.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    public interface IShoppingCartService
    {
        Task AddProductInCart(int productId, string userId, int? quantity = null);

        Task AddAddonInCart(int productId, string userId, int? quantity = null);

        bool AnyProdicts(string userId);

        bool AnyAddons(string userId);

        Task DeleteProdictFromCart(int productId, string userId);

        Task DeleteAddonFromCart(int addonId, string userId);

        Task UpdateShoppingCartProductQuantity(int productId, string userId, int quantity);

        Task UpdateShoppingCartAddonQuantity(int addonId, string userId, int quantity);

        IQueryable<ShoppingCartProduct> GetAllShoppingCartProducts(string userId);

        IQueryable<ShoppingCartAddon> GetShoppingCartAddons(string userId);

        Task ClearShoppingCart(string userId);
    }
}
