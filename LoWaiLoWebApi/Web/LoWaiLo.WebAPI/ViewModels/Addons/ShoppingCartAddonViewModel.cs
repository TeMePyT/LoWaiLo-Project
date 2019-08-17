namespace LoWaiLo.WebAPI.ViewModels.Addons
{
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ShoppingCartAddonViewModel : IMapFrom<Addon>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
