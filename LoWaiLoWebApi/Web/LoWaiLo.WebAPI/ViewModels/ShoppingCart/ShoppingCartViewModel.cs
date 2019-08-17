namespace LoWaiLo.WebAPI.ViewModels.ShoppingCart
{
    using System.Collections.Generic;

    using LoWaiLo.WebAPI.ViewModels.Addons;
    using LoWaiLo.WebAPI.ViewModels.Products;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.Products = new List<ShoppingCartProductViewModel>();
            this.Addons = new List<ShoppingCartAddonViewModel>();
        }

        public List<ShoppingCartProductViewModel> Products { get; set; }

        public List<ShoppingCartAddonViewModel> Addons { get; set; }

        public int DeliveryPrice { get; set; }

        public decimal ProductsTotal { get; set; }

        public decimal ShoppingCartTotal { get; set; }
    }
}
