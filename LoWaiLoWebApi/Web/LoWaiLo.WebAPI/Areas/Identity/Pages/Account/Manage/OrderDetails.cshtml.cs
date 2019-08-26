namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class OrderDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;

        public OrderDetailsModel(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
        }

        public OrderDetailsViewModel Order { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var order = this.ordersService.GetUserOrders(user.Id).To<OrderDetailsViewModel>().FirstOrDefault(x => x.Id == id);

            this.Order = order;

            return this.Page();
        }
    }
}