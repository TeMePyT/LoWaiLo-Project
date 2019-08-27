namespace LoWaiLo.WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using AutoMapper;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Categories;
    using LoWaiLo.WebAPI.ViewModels.Menu;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.Reviews.InputModels;
    using LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using X.PagedList;

    public class MenuController : BaseController
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;

        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly IAddonsService addonsService;
        private readonly IProductReviewsService productReviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public MenuController(ICategoriesService categoriesService, IProductsService productsService, IProductReviewsService productReviewsService, IAddonsService addonsService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;

            this.productsService = productsService;

            this.addonsService = addonsService;

            this.productReviewsService = productReviewsService;

            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(MenuViewModel model)
        {
            model.Categories = await this.categoriesService.All().To<CategoryViewModel>().ToListAsync();

            model.Addons = await this.addonsService.All().To<AddonViewModel>().ToListAsync();

            return this.View(model);
        }

        public ActionResult Details(int id)
        {
            var model = new DetailsViewModel();
            var product = Mapper.Map<ProductViewModel>(this.productsService.GetProductById(id));

            model.Product = product;

            int pageNumber = model.PageNumber ?? DefaultPageNumber;
            int pageSize = model.PageSize ?? DefaultPageSize;
            model.Product.Reviews = this.productReviewsService.GetReviews(id).To<ReviewViewModel>().OrderByDescending(r => r.ModifiedOn).ToPagedList(pageNumber, pageSize);
            model.InnerModel = new CreateProductReviewInputModel();

            this.ViewBag.Id = id;
            this.TempData["ProductId"] = id;

            return this.View(model);
        }

        public IActionResult AddReview()
        {
            return this.RedirectToAction(nameof(this.Details), new { id = (int)this.TempData["ProductId"] });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(CreateProductReviewInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var author = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                    var content = Regex.Replace(model.Content, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (model.ProductId > 0)
                    {
                        await this.productReviewsService.CreateAsync(model.Rating, content, author.Id, (int)model.ProductId);
                    }

                    var productId = model.ProductId;
                    return this.RedirectToAction(nameof(this.Details), new { id = productId });
                }
                catch (Exception)
                {
                    this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Error", "Home");
                }
            }

            this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
            return this.RedirectToAction("Error", "Home");
        }

        [Authorize]
        public async Task<IActionResult> DeleteProductReview(int id)
        {
            if (this.productReviewsService.GetReviews((int)this.TempData["ProductId"]).Any(x => x.Id == id))
            {
                try
                {
                    await this.productReviewsService.DeleteReviewAsync(id);

                    return this.RedirectToAction(nameof(this.Details), new { id = (int)this.TempData["ProductId"] });
                }
                catch (Exception)
                {
                    this.TempData["ErrorMessage"] = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Error", "Home");
                }
            }
            else
            {
                this.TempData["ErrorMessage"] = $"Мнение с номер {id} не беше намерено.";
                return this.RedirectToAction("Error", "Home");
            }
        }
    }
}