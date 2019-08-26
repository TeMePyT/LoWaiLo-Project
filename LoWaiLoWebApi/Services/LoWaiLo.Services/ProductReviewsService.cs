namespace LoWaiLo.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Contracts;

    using Microsoft.EntityFrameworkCore;

    public class ProductReviewsService : IProductReviewsService
    {
        private readonly IRepository<ProductReview> reviewsRepository;

        public ProductReviewsService(IRepository<ProductReview> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public bool Any()
        {
            return this.reviewsRepository.All().Any();
        }

        public async Task<ProductReview> CreateAsync(int rating, string content, string authorId, int productId)
        {
            var review = new ProductReview
            {
                Content = content,
                Rating = rating,
                AuthorId = authorId,
                ProductId = productId,
                ModifiedOn = DateTime.UtcNow.AddHours(3),
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();

            return review;
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = this.reviewsRepository
                .All()
                .First(r => r.Id == reviewId);

            this.reviewsRepository.Delete(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public bool Exists(int reviewId)
        {
            return this.reviewsRepository
                 .All()
                 .Any(r => r.Id == reviewId);
        }

        public string FindReviewAuthorById(int reviewId)
        {
            return this.reviewsRepository
                .All()
                .Include(r => r.Author)
                .First(r => r.Id == reviewId)
                .Author
                .UserName;
        }

        public IQueryable<ProductReview> GetReviews(int productId)
        {
            return this.reviewsRepository
                .All()
                .Include(r => r.Author)
                .Where(r => r.ProductId == productId);
        }
    }
}
