namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.InputModels.Categories;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Categories;

    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : AdministratorController
    {
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = new AllCategoriesViewModel();
            model.Categories = this.categoriesService.All().To<CategoryViewModel>();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = this.categoriesService.FindById(id);

            var model = Mapper.Map<EditCategoryInputModel>(category);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryInputModel model)
        {
            try
            {
                this.categoriesService.Update(model.Id, model.Name, model.Image);
                this.TempData["SuccessMessage"] = $"Успешно обновихте категория{model.Name}";
                return this.RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Category");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new CreateCategoryInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            try
            {
                await this.categoriesService.CreateAsync(model.Name, model.Image);
                this.TempData["SuccessMessage"] = $"Успешно създадохте категория{model.Name}";
                return this.RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.Redirect(nameof(this.Create));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this.categoriesService.DeleteAsync(id);
                if (result)
                {
                    this.TempData["SuccessMessage"] = $"Успешно изтрихте категорията";
                    return this.RedirectToAction("All", "Category");
                }
                else
                {
                    this.TempData["ErrorMessage"] = "Моля изтрийте продуктите от категорията преди да продължите";
                    return this.RedirectToAction("All", "Category");
                }
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Category");
            }
        }
    }
}