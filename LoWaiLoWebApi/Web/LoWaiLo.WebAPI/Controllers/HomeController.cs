namespace LoWaiLo.WebAPI.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels;
    using LoWaiLo.WebAPI.ViewModels.Categories;
    using LoWaiLo.WebAPI.ViewModels.Home;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ISiteReviewsService reviewService;
        private readonly IProductsService productsService;

        public HomeController(ICategoriesService categoriesService, ISiteReviewsService reviewService, IProductsService productsService)
        {
            this.categoriesService = categoriesService;

            this.reviewService = reviewService;

            this.productsService = productsService;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var categories = await this.categoriesService.All().To<CategoryViewModel>().ToListAsync();

            var siteReviews = await this.reviewService.GetReviews().To<ReviewViewModel>().ToListAsync();
            var reviews = siteReviews.OrderByDescending(x => x.ModifiedOn).Take(3);

            var allProducts = await this.productsService.All().To<ProductViewModel>().ToListAsync();
            var products = allProducts.OrderByDescending(x => x.CreatedOn).Take(5);

            model.Categories = categories;
            model.Reviews = reviews;
            model.Products = products;

            return this.View(model);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
