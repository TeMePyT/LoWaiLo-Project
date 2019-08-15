namespace LoWaiLo.WebAPI.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels;
    using LoWaiLo.WebAPI.ViewModels.Categories;
    using LoWaiLo.WebAPI.ViewModels.Home;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var categories = await this.categoriesService.All().To<CategoryViewModel>().ToListAsync();

            model.Categories = categories;

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
