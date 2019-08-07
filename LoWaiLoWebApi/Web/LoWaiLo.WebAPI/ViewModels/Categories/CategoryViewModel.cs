namespace LoWaiLo.WebAPI.ViewModels.Categories
{
    using System.Collections.Generic;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;
    using LoWaiLo.WebAPI.ViewModels.Products;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
