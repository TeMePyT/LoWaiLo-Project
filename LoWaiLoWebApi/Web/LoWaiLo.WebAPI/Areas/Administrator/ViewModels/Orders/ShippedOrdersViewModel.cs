namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders
{
    using System.Collections.Generic;

    public class ShippedOrdersViewModel
    {
        public ShippedOrdersViewModel()
        {
            this.ShippedOrders = new List<OrderViewModel>();
        }

        public IEnumerable<OrderViewModel> ShippedOrders { get; set; }
    }
}
