namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class ProductReview : BaseModel<int>
    {
        public int Rating { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
