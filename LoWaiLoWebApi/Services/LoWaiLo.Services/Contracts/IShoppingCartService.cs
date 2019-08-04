﻿namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    public interface IShoppingCartService
    {
        Task AddProductInCart(int productId, string userId, int? quantity = null);

        bool AnyProdicts(string userId);

        Task DeleteProdictFromCart(int productId, string userId);

        Task UpdateShoppingCartProductQuantity(int productId, string userId, int quantity);

        IEnumerable<ShoppingCartProduct> GetAllShoppingCartProducts(string userId);
    }
}