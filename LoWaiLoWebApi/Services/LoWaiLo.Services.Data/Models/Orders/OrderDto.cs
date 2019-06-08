namespace LoWaiLo.Services.Data.Models.Orders
{
    using System;
    using System.Collections.Generic;

    public class OrderDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderProductDto> OrderProducts { get; set; }

        public IEnumerable<OrderAddonDto> OrderAddons { get; set; }
    }
}
