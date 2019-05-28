namespace LoWaiLo.Data.Models
{
    public class OrderAddon
    {
        public int Id { get; set; }

        public int AddonId { get; set; }

        public Addon Addon { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
