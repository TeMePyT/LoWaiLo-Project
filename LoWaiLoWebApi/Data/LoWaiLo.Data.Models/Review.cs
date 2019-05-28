namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Review : BaseModel<int>
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
