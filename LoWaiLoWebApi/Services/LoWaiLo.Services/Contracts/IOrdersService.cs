namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Models.Enums;

    public interface IOrdersService
    {
        Task<Order> CreateOrderAsync(string userId, string deliveryAddress, string phoneNumber, IEnumerable<ShoppingCartProduct> products, IEnumerable<ShoppingCartAddon> addons);

        Task ProcessOrderAsync(string orderId, OrderStatus status);

        IEnumerable<Order> GetOrdersByStatus(OrderStatus status);

        IEnumerable<Order> GetUserOrders(string userId);

        bool Exists(string orderId);
    }
}
