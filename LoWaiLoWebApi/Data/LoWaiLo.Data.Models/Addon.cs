namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Addon : BaseModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
