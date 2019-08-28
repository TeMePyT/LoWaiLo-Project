namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using X.PagedList;

    public class ProductsController : AdministratorController
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;

        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(
            IProductsService productsService
            , ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All(AllProductsViewModel model)
        {
            var products = await this.productsService
                .All()
                .To<ProductViewModel>()
                .OrderBy(x => x.CategoryId)
                .ThenBy(x => x.Id)
                .ToListAsync();

            int pageNumber = model.PageNumber ?? DefaultPageNumber;
            int pageSize = model.PageSize ?? DefaultPageSize;

            model.Products = products.ToPagedList(pageNumber, pageSize);

            return this.View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.productsService.DeleteAsync(id);
                this.TempData["SuccessMessage"] = "Успешно изтрихте продукта";
                return this.RedirectToAction("All", "Products");
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Products");
            }
        }
    }
}