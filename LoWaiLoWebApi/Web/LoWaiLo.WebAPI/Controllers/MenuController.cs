namespace LoWaiLo.WebAPI.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;

    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels.Categories;
    using LoWaiLo.WebAPI.ViewModels.Menu;
    using LoWaiLo.WebAPI.ViewModels.Products;

    using Microsoft.AspNetCore.Mvc;

    using Microsoft.EntityFrameworkCore;

    public class MenuController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public MenuController(ICategoriesService categoriesService, IProductsService productsService)
        {
            this.categoriesService = categoriesService;

            this.productsService = productsService;
        }

        public async Task<IActionResult> All(MenuViewModel model)
        {
            model.Categories = await this.categoriesService.All().To<CategoryViewModel>().ToListAsync();

            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();

            var product = Mapper.Map<ProductViewModel>(this.productsService.GetProductById(id));

            model.Product = product;

            return this.View(model);
        }
    }
}