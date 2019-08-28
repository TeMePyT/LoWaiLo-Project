namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel
    {

        public AllCategoriesViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
