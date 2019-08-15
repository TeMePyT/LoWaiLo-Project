namespace LoWaiLo.WebAPI.ViewModels.Menu
{
    using System.Collections.Generic;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.Reviews.InputModels;
    using LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels;

    public class DetailsViewModel
    {
        public ProductViewModel Product { get; set; }

        public CreateProductReviewInputModel InnerModel { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
