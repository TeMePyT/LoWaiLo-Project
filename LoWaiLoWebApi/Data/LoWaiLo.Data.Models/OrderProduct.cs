﻿namespace LoWaiLo.Data.Models
{
    public class OrderProduct
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
