namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Like : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
