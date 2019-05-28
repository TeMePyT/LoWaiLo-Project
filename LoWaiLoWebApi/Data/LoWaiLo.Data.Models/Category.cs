namespace LoWaiLo.Data.Models
{
    using System.Collections.Generic;

    using LoWaiLo.Data.Common;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
