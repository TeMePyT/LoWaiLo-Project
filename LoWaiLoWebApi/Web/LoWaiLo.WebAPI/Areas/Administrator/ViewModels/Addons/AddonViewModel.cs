namespace LoWaiLo.WebAPI.Areas.Administrator.ViewModels.Addons
{
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class AddonViewModel : IMapFrom<Addon>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
