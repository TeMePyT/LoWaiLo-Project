namespace LoWaiLo.Data.Models
{
    public class ShoppingCartAddon
    {
        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int AddonId { get; set; }

        public virtual Addon Addon { get; set; }

        public int Quantity { get; set; }
    }
}
