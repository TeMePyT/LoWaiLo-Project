namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Administrator.InputModels.Addons;
    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Addons;
    using Microsoft.AspNetCore.Mvc;

    public class AddonsController : AdministratorController
    {
        private readonly IAddonsService addonsService;

        public AddonsController(IAddonsService addonsService)
        {
            this.addonsService = addonsService;
        }

        public IActionResult All()
        {
            var model = new AllAddonsViewModel();
            var addons = this.addonsService.All().To<AddonViewModel>();

            model.Addons = addons;

            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var addon = this.addonsService.GetAddonById(id);
                var model = Mapper.Map<EditAddonInputModel>(addon);

                return this.View(model);
            }
           catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Addons");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAddonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["ErrorMessage"] = "Нещо не е наред, опитайте пак";
                return this.View(model);
            }

            var addon = Mapper.Map<Addon>(model);

            try
            {
                await this.addonsService.EditAsync(addon);
                this.TempData["SuccessMessage"] = $"Успешно обновихте добавка {model.Name}";
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.addonsService.DeleteAsync(id);
                this.TempData["SuccessMessage"] = "Успешно изтрихте добавката";
                return this.RedirectToAction("All", "Addons");
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо се обърка, опитайте пак";
                return this.RedirectToAction("All", "Addons");
            }
        }

        public IActionResult Create()
        {
            var model = new CreateAddonInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAddonInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["ErrorMessage"] = "Нещо не е наред, опитайте пак";
                return this.View(model);
            }

            var addon = Mapper.Map<Addon>(model);

            try
            {
                await this.addonsService.AddAsync(addon);
                this.TempData["SuccessMessage"] = $"Успешно създадохте добавка {addon.Name}";
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                this.TempData["ErrorMessage"] = "Нещо не е наред, опитайте пак";
                return this.View(model);
            }
        }
    }
}