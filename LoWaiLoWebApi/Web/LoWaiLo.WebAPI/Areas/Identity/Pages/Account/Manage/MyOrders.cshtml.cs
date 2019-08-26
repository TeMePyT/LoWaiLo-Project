namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.ViewModels;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class MyOrdersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersService ordersService;

        public MyOrdersModel(
            UserManager<ApplicationUser> userManager,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.ordersService = ordersService;
        }

        public IList<OrderListViewModel> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var orders = this.ordersService.GetUserOrders(user.Id).To<OrderListViewModel>();
            this.Orders = orders.OrderByDescending(x => x.CreatedOn).ToList();

            return this.Page();
        }
    }
}