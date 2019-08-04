namespace LoWaiLo.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Moq;
    using Xunit;

    public class ShoppingCartServiceTests
    {
        private readonly IRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly LoWaiLoDbContext context;
        private readonly Mock<IProductsService> productsService;
        public ShoppingCartServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            this.context = new LoWaiLoDbContext(options);

            this.shoppingCartProductsRepository = new DbRepository<ShoppingCartProduct>(context);
            this.productsService = new Mock<IProductsService>();
        }

        [Fact]
        public async Task AddProductInCart_ShouldAddProductInCart()
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var productId = 1;
            this.productsService.Setup(p => p.GetProductById(productId))
                .Returns(new Product
                {
                    Id=productId,
                    Name = "Панирани зеленчуци",
                    MenuNumber = 3,
                    Price = 3.80m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
                });

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            await shoppingCartService.AddProductInCart(productId, userId);
            var result1 = shoppingCartProductsRepository.All().ToList();

            Assert.Single(result1);
            Assert.Equal(user.ShoppingCartId, result1.First().ShoppingCartId);
            Assert.Equal(1, result1.First().ProductId);
        }

        [Fact]
        public async Task AddProductInCart_WithInvalidUser_ShouldNotAddProduct()
        {
            var userId = "123";
            ApplicationUser user = null;

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(user);
            var userManager = TestUserManager(userStore.Object);

            var productId = 1;
            this.productsService.Setup(p => p.GetProductById(productId))
                .Returns(new Product
                {
                    Id = 123,
                    Name = "Панирани зеленчуци",
                    MenuNumber = 3,
                    Price = 3.80m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
                });

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);
            await shoppingCartService.AddProductInCart(productId, userId);

            var result = shoppingCartProductsRepository.All().ToList();
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddProductInCart_WithInvalidProduct_ShouldNotAddProduct()
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var productId = 1;
            Product product = null;
            this.productsService.Setup(p => p.GetProductById(productId))
              .Returns(product);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);
            await shoppingCartService.AddProductInCart(productId, userId);

            var result = shoppingCartProductsRepository.All().ToList();
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddProductInCart_WithExistingProduct_ShouldNotAddProduct()
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            Product product = new Product
            {
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };
            context.Products.Add(product);
            context.SaveChanges();

            this.productsService.Setup(p => p.GetProductById(product.Id)).Returns(product);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            await shoppingCartService.AddProductInCart(product.Id, user.Id);
            await shoppingCartService.AddProductInCart(product.Id, user.Id);

            var result = this.shoppingCartProductsRepository.All().ToList();

            Assert.Single(result);
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public async Task AddProductInCart_ShouldAddDefaultOrPassedQuantity(int? quantity, int expectedQuantity)
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            Product product = new Product
            {
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };
            context.Products.Add(product);
            context.SaveChanges();

            this.productsService.Setup(p => p.GetProductById(product.Id)).Returns(product);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            await shoppingCartService.AddProductInCart(product.Id, user.Id, quantity);

            var result = this.shoppingCartProductsRepository.All().ToList().First().Quantity;

            Assert.Equal(expectedQuantity, result);
        }

        [Fact]
        public void GetAllShoppingCartProducts_ShouldReturnAllShopingCartProductsForUser()
        {
            var products = new List<Product>
            {
                 new Product
            {
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            },
                  new Product
                {
                    Name = "Супа от морски водорасли",
                    MenuNumber = 29,
                    Price = 2.30m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/drla3ojeual1qvx/29.jpg?dl=0",
                  },
                   new Product
                {
                    Name = "Ориз със скариди, яйца и шунка",
                    MenuNumber = 35,
                    Price = 6.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/u05yopm9pexbxi0/40.jpg?dl=0",
                }
        };

            context.Products.AddRange(products);

            var shoppingCartProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct{Product = products.First()},
                new ShoppingCartProduct{Product = products.Last()},
            };

            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart { ShoppingCartProducts = shoppingCartProducts } };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);
            var result = shoppingCartService.GetAllShoppingCartProducts(user.Id).Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetAllShoppingCartProducts_WithInvalidUser_ShouldReturnNull()
        {
            var userId = "123";
            ApplicationUser user = null;

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(user);
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            var result = shoppingCartService.GetAllShoppingCartProducts(userId);

            Assert.Null(result);
        }

        [Fact]
        public void AnyProducts_ShouldReturnFalse_WhenThereAreNoProducts()
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            var result = shoppingCartService.AnyProdicts(userId);

            Assert.False(result);
        }

        [Fact]
        public async Task AnyProducts_ShouldReturnTrue_WhenThereAreProducts()
        {
            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart() };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            var productId = 1;
            this.productsService.Setup(p => p.GetProductById(productId))
                .Returns(new Product
                {
                    Name = "Панирани зеленчуци",
                    MenuNumber = 3,
                    Price = 3.80m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
                });

            await shoppingCartService.AddProductInCart(productId, user.Id, 2);

            var result = shoppingCartService.AnyProdicts(user.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductFromCart_ShouldDeleteProductFromCart()
        {
            var products = new List<Product>
            {
                 new Product
            {
                     Id=1,
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            },
                  new Product
                {
                      Id=2,
                    Name = "Супа от морски водорасли",
                    MenuNumber = 29,
                    Price = 2.30m,
                    Weight = 300,
                    Image = "https://www.dropbox.com/s/drla3ojeual1qvx/29.jpg?dl=0",
                  },
                   new Product
                {
                       Id=3,
                    Name = "Ориз със скариди, яйца и шунка",
                    MenuNumber = 35,
                    Price = 6.80m,
                    Weight = 800,
                    Image = "https://www.dropbox.com/s/u05yopm9pexbxi0/40.jpg?dl=0",
                }
        };

            context.Products.AddRange(products);

            var shoppingCartProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct{Product = products.First()},
                new ShoppingCartProduct{Product = products.Last()},
            };

            var userId = "123";
            var userName = "user@abv.bg";
            var user = new ApplicationUser { Id = userId, UserName = userName, ShoppingCart = new ShoppingCart { ShoppingCartProducts = shoppingCartProducts } };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(userId, CancellationToken.None)).ReturnsAsync(context.Users.First(x => x.Id == userId));
            var userManager = TestUserManager(userStore.Object);

            productsService.Setup(x => x.GetProductById(products.First().Id)).Returns(products.First());

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            var firstCheck = shoppingCartService.GetAllShoppingCartProducts(userId).ToList().Count();
            Assert.Equal(2, firstCheck);

            await shoppingCartService.DeleteProdictFromCart(products.First().Id, userId);
            var secondCheck = shoppingCartService.GetAllShoppingCartProducts(userId);

            Assert.Single(secondCheck);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(-1, 1)]
        [InlineData(0, 1)]
        [InlineData(4, 4)]
        public async Task UpdateShoppingCartProductQuantity_ShouldUpdateSoppingCartProductQuantity(int quantity, int expectedQty)
        {
            var product = new Product
            {
                Name = "Панирани зеленчуци",
                MenuNumber = 3,
                Price = 3.80m,
                Weight = 300,
                Image = "https://www.dropbox.com/s/r0r2ax7al6qtbjk/03.jpg?dl=0",
            };
            context.Products.Add(product);
     

            var shoppingCart = new ShoppingCart
            {
                ShoppingCartProducts = new List<ShoppingCartProduct>
                {
                   new ShoppingCartProduct { Product = product, Quantity = 1 },
                }
            };

            ApplicationUser user = new ApplicationUser
            {
                ShoppingCart = shoppingCart,
            };
            context.Users.Add(user);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userStore.Setup(s => s.FindByIdAsync(user.Id, CancellationToken.None)).ReturnsAsync(user);
            var userManager = TestUserManager(userStore.Object);

            productsService.Setup(x => x.GetProductById(product.Id)).Returns(product);

            var shoppingCartService = new ShoppingCartService(shoppingCartProductsRepository, productsService.Object, userManager);

            await shoppingCartService.UpdateShoppingCartProductQuantity(product.Id, user.Id, quantity);

            var result = shoppingCartProductsRepository
                .All()
                .FirstOrDefault(x => x.ProductId == product.Id && x.ShoppingCartId == user.ShoppingCartId);

            Assert.Equal(expectedQty, result.Quantity);
        }
        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }
    }
}
