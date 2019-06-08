namespace LoWaiLo.Data.Models
{
    using System.Collections.Generic;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models.Enums;

    public class Order : BaseModel<string>
    {
        public string CustomerId { get; set; }

        public ApplicationUser Customer { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderProduct> Products { get; set; }

        public ICollection<OrderAddon> Addons { get; set; }
    }
}
