namespace LoWaiLo.WebAPI.ViewModels.Menu
{
    using System.Collections.Generic;

    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Categories;

    public class MenuViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<AddonViewModel> Addons { get; set; }
    }
}
