namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Orders
{
    using System.Collections.Generic;

    public class HistoryViewModel
    {
        public HistoryViewModel()
        {
            this.DeliveredOrders = new List<OrderViewModel>();
            this.CanceledOrders = new List<OrderViewModel>();
        }

        public IEnumerable<OrderViewModel> DeliveredOrders { get; set; }

        public IEnumerable<OrderViewModel> CanceledOrders { get; set; }
    }
}