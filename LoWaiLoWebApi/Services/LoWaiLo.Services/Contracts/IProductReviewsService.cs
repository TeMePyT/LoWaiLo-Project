namespace LoWaiLo.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    public interface IProductReviewsService
    {
        Task<ProductReview> CreateAsync(int rating, string text, string authorId, int productId);

        IEnumerable<ProductReview> GetReviews(int productId);

        Task DeleteReviewAsync(int reviewId);

        bool Exists(int reviewId);

        string FindReviewAuthorById(int reviewId);

        bool Any();
    }
}
