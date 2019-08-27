    namespace LoWaiLo.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models.Enums;

    public class Order : BaseModel<string>
    {
        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        public int OrderNumber { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public DateTime? ShippedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public virtual ICollection<OrderAddon> OrderAddons { get; set; }
    }
}
