namespace LoWaiLo.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Models;

    public interface ISiteReviewsService
    {
        Task<SiteReview> CreateAsync(int rating, string text, string authorId);

        IQueryable<SiteReview> GetReviews();

        Task DeleteReviewAsync(int reviewId);

        bool Exists(int reviewId);

        string FindReviewAuthorById(int reviewId);

        bool Any();
    }
}
