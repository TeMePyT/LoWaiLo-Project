namespace LoWaiLo.WebAPI.Controllers
{
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

    public class SiteReviewController : Controller
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;

        private readonly ISiteReviewsService siteReviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public SiteReviewController(ISiteReviewsService siteReviewsService, UserManager<ApplicationUser> userManager)
        {
            this.siteReviewsService = siteReviewsService;
            this.userManager = userManager;
        }

        [BindProperty]
        public CreateReviewInputModel Input { get; set; }

        public async Task<IActionResult> All(SiteReviewViewModel model)
        {
            var reviews = await this.siteReviewsService.GetReviews().To<ReviewViewModel>().OrderByDescending(r => r.ModifiedOn).ToListAsync();

            int pageNumber = model.PageNumber ?? DefaultPageNumber;
            int pageSize = model.PageSize ?? DefaultPageSize;

            model.Reviews = reviews.ToPagedList(pageNumber, pageSize);
            model.InnerModel = new CreateReviewInputModel();
            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var author = await this.userManager.FindByNameAsync(this.User.Identity.Name);
                var content = Regex.Replace(model.Content, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

                await this.siteReviewsService.CreateAsync(model.Rating, content, author.Id);
                return this.RedirectToAction(nameof(this.All));
            }

            return this.RedirectToAction(nameof(this.All));
        }
    }
}