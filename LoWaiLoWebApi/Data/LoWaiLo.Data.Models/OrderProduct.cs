namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class OrderProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
