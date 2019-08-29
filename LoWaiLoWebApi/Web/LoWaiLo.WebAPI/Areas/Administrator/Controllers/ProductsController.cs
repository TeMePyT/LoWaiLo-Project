namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.InputModels.Products;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using X.PagedList;

    public class ProductsController : AdministratorController
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;

        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService)
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

        public IActionResult Edit(int id)
        {
            try
            {
                var model = Mapper.Map<EditProductInputModel>(this.productsService.GetProductById(id));

                var categories = this.categoriesService
                    .All()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                    })
                    .ToList();

                model.Categories = categories;
                return this.View(model);
            }
          catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Products");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var categories = this.categoriesService
                    .All()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                    })
                    .ToList();

                model.Categories = categories;
                return this.View(model);
            }

            var product = Mapper.Map<Product>(model);

            try
            {
                this.TempData["SuccessMessage"] = $"Успешно обновихте продукт {model.Name}";
                await this.productsService.EditAsync(product);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                var categories = this.categoriesService
                    .All()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                    })
                    .ToList();

                model.Categories = categories;
                return this.View(model);
            }
        }

        public IActionResult Create()
        {
            var categories = this.categoriesService
                    .All()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                    })
                    .ToList();

            var model = new CreateProductInputModel();
            model.Categories = categories;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["ErrorMessage"] = "Нещо не е наред, опитайте пак";
                var categories = this.categoriesService
                 .All()
                 .Select(x => new SelectListItem
                 {
                     Value = x.Id.ToString(),
                     Text = x.Name,
                 })
                 .ToList();

                model.Categories = categories;

                return this.View(model);
            }

            var product = Mapper.Map<Product>(model);
            try
            {
                await this.productsService.AddAsync(product);
                this.TempData["SuccessMessage"] = $"Успешно добавихте продукт {product.Name}";
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка";
                return this.RedirectToAction(nameof(this.All));
            }

        }
    }
}