namespace LoWaiLo.Data.Models
{
    public class UserFavoriteProduct
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
