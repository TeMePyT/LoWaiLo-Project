namespace LoWaiLo.Data.Models
{
    public class OrderAddon
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int AddonId { get; set; }

        public virtual Addon Addon { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
