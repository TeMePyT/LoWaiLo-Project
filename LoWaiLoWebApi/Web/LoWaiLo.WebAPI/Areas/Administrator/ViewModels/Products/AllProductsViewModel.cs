namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Products
{
    using System.Collections.Generic;

    public class AllProductsViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
