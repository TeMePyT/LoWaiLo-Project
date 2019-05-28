namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Like : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
