namespace LoWaiLo.Services.Data.Models.Products
{
    using System.Collections.Generic;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Data.Models.Categories;

    public class ProductDto
    {
        public int Id { get; set; }

        public int MenuNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public IEnumerable<ApplicationUser> Likes { get; set; }
    }
}
