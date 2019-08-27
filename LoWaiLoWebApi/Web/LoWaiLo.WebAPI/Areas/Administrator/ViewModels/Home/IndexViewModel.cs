namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Home
{
    using System.Collections.Generic;

    using LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.PendingOrders = new List<OrderViewModel>();
            this.ApprovedOrders = new List<OrderViewModel>();
        }

        public IEnumerable<OrderViewModel> PendingOrders { get; set; }

        public IEnumerable<OrderViewModel> ApprovedOrders { get; set; }
    }
}
