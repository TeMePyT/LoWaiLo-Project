namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Addons
{
    using System.Collections.Generic;

    public class AllAddonsViewModel
    {
        public AllAddonsViewModel()
        {
            this.Addons = new List<AddonViewModel>();
        }

        public IEnumerable<AddonViewModel> Addons { get; set; }
    }
}
