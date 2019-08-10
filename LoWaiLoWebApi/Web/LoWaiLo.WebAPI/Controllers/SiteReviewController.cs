namespace LoWaiLo.WebAPI.Controllers
{
    using System.Linq;
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

    public class SiteReviewController : Controller
    {
        private readonly ISiteReviewsService siteReviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SiteReviewController(ISiteReviewsService siteReviewsService, UserManager<ApplicationUser> userManager)
        {
            this.siteReviewsService = siteReviewsService;
            this.userManager = userManager;
        }

        [BindProperty]
        public CreateReviewInputModel Input { get; set; }

        public async Task<IActionResult> All()
        {
            SiteReviewViewModel model = new SiteReviewViewModel();
            model.Reviews = await this.siteReviewsService.GetReviews().To<ReviewViewModel>().OrderBy(r => r.Id).Take(6).ToListAsync();
            model.InnerModel = new CreateReviewInputModel();
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateReviewInputModel();
            return this.PartialView("~/Views/Shared/Partials/_ReviewBox.cshtml", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var author = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                await this.siteReviewsService.CreateAsync(model.Rating, model.Content, author.Id);
                return this.RedirectToAction(nameof(this.All));
            }

            return this.PartialView("~/Views/Shared/Partials/_ReviewBox.cshtml", model);
        }
    }
}