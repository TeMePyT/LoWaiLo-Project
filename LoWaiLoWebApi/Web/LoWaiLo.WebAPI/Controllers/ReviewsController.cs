namespace LoWaiLo.WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels.Reviews.InputModels;
    using LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Microsoft.EntityFrameworkCore;
    using X.PagedList;

    public class ReviewsController : Controller
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;

        private readonly ISiteReviewsService siteReviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(ISiteReviewsService siteReviewsService, UserManager<ApplicationUser> userManager)
        {
            this.siteReviewsService = siteReviewsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(SiteReviewViewModel model)
        {
            var reviews = await this.siteReviewsService.GetReviews().To<ReviewViewModel>().OrderByDescending(r => r.ModifiedOn).ToListAsync();

            int pageNumber = model.PageNumber ?? DefaultPageNumber;
            int pageSize = model.PageSize ?? DefaultPageSize;

            model.Reviews = reviews.ToPagedList(pageNumber, pageSize);
            model.InnerModel = new CreateReviewInputModel();
            return this.View(model);
        }

        public IActionResult AddReview()
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(CreateReviewInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var author = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                    var content = Regex.Replace(model.Content, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    await this.siteReviewsService.CreateAsync(model.Rating, content, author.Id);
                    return this.RedirectToAction(nameof(this.Index));
                }
                catch (Exception)
                {
                    this.ViewBag.ErrorMessage = "Нещо се обърка при обработката на заявката ви.";
                    return this.RedirectToAction("Error", "Home");
                }
            }

            this.ViewBag.ErrorMessage = "Нещо се обърка при обработката на заявката ви.";
            return this.RedirectToAction("Error", "Home");
        }
    }
}