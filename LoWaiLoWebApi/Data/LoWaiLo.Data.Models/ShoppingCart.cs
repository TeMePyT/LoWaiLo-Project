namespace LoWaiLo.Data.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
    }
}
