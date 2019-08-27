namespace LoWaiLo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Models.Enums;
    using LoWaiLo.Services.Contracts;

    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> ordersRepository;

        public OrdersService(IRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<Order> CreateOrderAsync(string userId, string deliveryAddress, string phoneNumber, IEnumerable<ShoppingCartProduct> products, IEnumerable<ShoppingCartAddon> addons)
        {
            var order = new Order
            {
                CustomerId = userId,
                Status = OrderStatus.Pending,
                DeliveryAddress = deliveryAddress,
                PhoneNumber = phoneNumber,
                CreatedOn = DateTime.UtcNow.AddHours(3),
            };

            var orderProducts = products.Select(x => new OrderProduct
            {
                OrderId = order.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Product.Price * x.Quantity,
            }).ToList();

            order.OrderProducts = orderProducts;

            var orderAddons = addons.Select(x => new OrderAddon
            {
                OrderId = order.Id,
                AddonId = x.AddonId,
                Quantity = x.Quantity,
                Price = x.Quantity * x.Addon.Price,
            }).ToList();

            order.OrderAddons = orderAddons;

            var productsPrice = orderProducts.Select(x => x.Price).Sum();
            var addonsPrice = orderAddons.Select(x => x.Price).Sum();

            var deliveryPrice = productsPrice + addonsPrice < 10 ? 2 : 1;

            order.DeliveryPrice = deliveryPrice;
            order.TotalPrice = productsPrice + addonsPrice + deliveryPrice;

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            var returnOrder = this.ordersRepository
                .All()
                .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(o => o.OrderAddons)
                .ThenInclude(a => a.Addon)
                .First(o => o.Id == order.Id);

            return returnOrder;
        }

        public bool Exists(string orderId)
        {
            return this.ordersRepository.All()
                .Any(o => o.Id == orderId);
        }

        public IQueryable<Order> GetOrdersByStatus(OrderStatus status)
        {
            return this.ordersRepository
                .All()
                .Include(o => o.OrderAddons)
                .ThenInclude(a => a.Addon)
                .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(o => o.Customer)
                .Where(o => o.Status == status);
        }

        public Order GetOrderById(string id)
        {
            var order = this.ordersRepository
                .All()
                .Include(o => o.OrderAddons)
                .ThenInclude(a => a.Addon)
                .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(o => o.Customer)
                .FirstOrDefault(x => x.Id == id);

            return order;
        }

        public IQueryable<Order> GetUserOrders(string userId)
        {
            return this.ordersRepository
                .All()
                .Include(o => o.OrderAddons)
                .ThenInclude(a => a.Addon)
                .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == userId);
        }

        public async Task ProcessOrderAsync(string orderId, OrderStatus status)
        {
            var order = this.ordersRepository
                .All()
                .First(o => o.Id == orderId);

            order.Status = status;

            switch (status)
            {
                case OrderStatus.Approved:
                    order.ApprovedOn = DateTime.UtcNow.AddHours(3);
                    break;
                case OrderStatus.Shipping:
                    order.ShippedOn = DateTime.UtcNow.AddHours(3);
                    break;
                default:
                    break;
            }

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
