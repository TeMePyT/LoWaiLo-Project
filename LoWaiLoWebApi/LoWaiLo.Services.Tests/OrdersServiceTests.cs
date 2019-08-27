namespace LoWaiLo.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Data.Models.Enums;

    using Microsoft.EntityFrameworkCore;

    using Xunit;

    public class OrdersServiceTests
    {
        private readonly IRepository<Order> ordersRepository;
        private readonly OrdersService ordersService;
        private readonly LoWaiLoDbContext context;

        public OrdersServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            this.context = new LoWaiLoDbContext(options);

            this.ordersRepository = new DbRepository<Order>(context);
            this.ordersService = new OrdersService(ordersRepository);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder_Succesfully()
        {

            var addons = new List<ShoppingCartAddon>
                {
                    new ShoppingCartAddon
                    {
                        Addon=new Addon
                        {
                            Price=2,
                        },

                        Quantity=1,
                    },
                    new ShoppingCartAddon
                    {
                         Addon=new Addon
                        {
                            Price=3,
                        },
                        Quantity=2,
                    },
                };

            var products = new List<ShoppingCartProduct>
                {
                    new ShoppingCartProduct
                    {
                        Quantity=3,
                       Product=new Product
                       {
                           Price=2
                       },
                    },
                    new ShoppingCartProduct
                    {
                        Quantity =1,
                        Product=new Product
                        {
                            Price=2,
                        }
                    },
                };
            var userId = "123";
            var address = "Pleven, Hristo Botev 89, A, 12";
            var phoneNumber = "064831122";

            var order = await this.ordersService.CreateOrderAsync(userId, address, phoneNumber, products, addons);

            Assert.Equal(2, order.OrderProducts.Count);
            Assert.Equal(2, order.OrderAddons.Count);
            Assert.Equal(17, order.TotalPrice);
            Assert.Equal("Pleven, Hristo Botev 89, A, 12", order.DeliveryAddress);
        }

        [Fact]
        public void Exists_ShouldReturn_False()
        {
            string id = "123";
            var result = this.ordersService.Exists(id);

            Assert.False(result);
        }

        [Fact]
        public async Task Exists_ShouldReturn_True()
        {
            var addons = new List<ShoppingCartAddon>
                {
                    new ShoppingCartAddon
                    {
                        Addon=new Addon
                        {
                            Price=2,
                        },

                        Quantity=1,
                    },
                    new ShoppingCartAddon
                    {
                         Addon=new Addon
                        {
                            Price=3,
                        },
                        Quantity=2,
                    },
                };

            var products = new List<ShoppingCartProduct>
                {
                    new ShoppingCartProduct
                    {
                        Quantity=3,
                       Product=new Product
                       {
                           Price=2
                       },
                    },
                    new ShoppingCartProduct
                    {
                        Quantity =1,
                        Product=new Product
                        {
                            Price=2,
                        }
                    },
                };
            var userId = "123";
            var address = "Pleven, Hristo Botev 89, A, 12";
            var phoneNumber = "064831122";
            var order = await this.ordersService.CreateOrderAsync(userId, address, phoneNumber, products, addons);

            var orderId = this.ordersRepository.All().First().Id;

            var result = this.ordersService.Exists(orderId);

            Assert.True(result);
        }

        [Theory]
        [InlineData(OrderStatus.Approved, 1)]
        [InlineData(OrderStatus.Canceled, 1)]
        [InlineData(OrderStatus.Delivered, 1)]
        [InlineData(OrderStatus.Shipping, 1)]
        public async Task ProcessOrderAsync_ShouldProcessOrder_And_GetOrderByStatus_ShouldReturn_Correct(OrderStatus status, int expectedQty)
        {
            var addons = new List<ShoppingCartAddon>
                {
                    new ShoppingCartAddon
                    {
                        Addon=new Addon
                        {
                            Price=2,
                        },

                        Quantity=1,
                    },
                    new ShoppingCartAddon
                    {
                         Addon=new Addon
                        {
                            Price=3,
                        },
                        Quantity=2,
                    },
                };

            var products = new List<ShoppingCartProduct>
                {
                    new ShoppingCartProduct
                    {
                        Quantity=3,
                       Product=new Product
                       {
                           Price=2
                       },
                    },
                    new ShoppingCartProduct
                    {
                        Quantity =1,
                        Product=new Product
                        {
                            Price=2,
                        }
                    },
                };
            var userId = "123";
            var address = "Pleven, Hristo Botev 89, A, 12";
            var phoneNumber = "064831122";
            var order = await this.ordersService.CreateOrderAsync(userId, address, phoneNumber, products, addons);

            Assert.Equal("Pending", order.Status.ToString());

            var orderId = this.ordersRepository.All().First().Id;

            await this.ordersService.ProcessOrderAsync(orderId, status);

            Assert.Equal(status, order.Status);

            var result = this.ordersService.GetOrdersByStatus(status).Count();

            Assert.Equal(expectedQty, result);

        }

        [Fact]
        public async Task GetOrderById_ShouldReturn_Order()
        {
            var addons = new List<ShoppingCartAddon>
                {
                    new ShoppingCartAddon
                    {
                        Addon=new Addon
                        {
                            Price=2,
                        },

                        Quantity=1,
                    },
                    new ShoppingCartAddon
                    {
                         Addon=new Addon
                        {
                            Price=3,
                        },
                        Quantity=2,
                    },
                };

            var products = new List<ShoppingCartProduct>
                {
                    new ShoppingCartProduct
                    {
                        Quantity=3,
                       Product=new Product
                       {
                           Price=2
                       },
                    },
                    new ShoppingCartProduct
                    {
                        Quantity =1,
                        Product=new Product
                        {
                            Price=2,
                        }
                    },
                };
            var userId = "123";
            var address = "Pleven, Hristo Botev 89, A, 12";
            var phoneNumber = "064831122";
            var order = await this.ordersService.CreateOrderAsync(userId, address, phoneNumber, products, addons);

            var orderId = this.ordersRepository.All().First().Id;

            var result = this.ordersService.GetOrderById(orderId);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public void GetOrderById_ShouldReturn_Null()
        {
            var result = this.ordersService.GetOrderById("123");

            Assert.Null(result);
        }
    }
}
