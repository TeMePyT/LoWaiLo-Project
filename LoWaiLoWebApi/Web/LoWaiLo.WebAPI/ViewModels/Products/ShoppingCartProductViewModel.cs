namespace LoWaiLo.WebAPI.ViewModels.Products
{
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class ShoppingCartProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
