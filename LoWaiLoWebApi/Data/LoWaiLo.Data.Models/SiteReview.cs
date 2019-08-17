namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class SiteReview : BaseModel<int>
    {
        public int Rating { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
