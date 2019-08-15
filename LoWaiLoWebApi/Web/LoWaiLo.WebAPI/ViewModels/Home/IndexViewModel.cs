namespace LoWaiLo.WebAPI.ViewModels.Home
{
    using System.Collections.Generic;

    using LoWaiLo.WebAPI.ViewModels.Categories;

    public class IndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
