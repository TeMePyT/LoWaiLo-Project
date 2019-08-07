namespace LoWaiLo.WebAPI.ViewModels.Home
{
    using System.Collections.Generic;

    using LoWaiLo.WebAPI.ViewModels.Categories;
    using LoWaiLo.WebAPI.ViewModels.Products;
    using LoWaiLo.WebAPI.ViewModels.Reviews.ViewModels;

    public class IndexViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
