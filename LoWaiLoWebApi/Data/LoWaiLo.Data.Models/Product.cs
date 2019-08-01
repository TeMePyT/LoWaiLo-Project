namespace LoWaiLo.Data.Models
{
    using System.Collections.Generic;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Services.Mapping;

    public class Product : BaseModel<int>
    {
        public string Name { get; set; }

        public int MenuNumber { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<ProductReview> Reviews { get; set; }
    }
}
