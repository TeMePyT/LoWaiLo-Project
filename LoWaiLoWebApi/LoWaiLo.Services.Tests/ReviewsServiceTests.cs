namespace LoWaiLo.Services.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data;
    using LoWaiLo.Data.Common;
    using LoWaiLo.Data.Models;

    using Microsoft.EntityFrameworkCore;

    using Xunit;
    public class ReviewsServiceTests
    {
        private readonly IRepository<ProductReview> reviewsRepository;
        private readonly ProductReviewsService reviewsService;

        public ReviewsServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoWaiLoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            var context = new LoWaiLoDbContext(options);

            this.reviewsRepository = new DbRepository<ProductReview>(context);
            this.reviewsService = new ProductReviewsService(reviewsRepository);
        }

        [Fact]
        public void AnyShouldReturnFalse()
        {
            var result = this.reviewsService.Any();
            Assert.False(result);
        }
        [Fact]
        public async Task CreateAsyncShouldCreateReviewSuccesfuly()
        {
            var result = await this.reviewsService.CreateAsync(5, "Delicious", "123", 456);

            Assert.Equal(1, reviewsRepository.All().Count());
            Assert.Equal("Delicious", result.Content);
            Assert.Equal(5, result.Rating);
            Assert.Equal("123", result.AuthorId);
            Assert.Equal(456, result.ProductId);
        }

        [Fact]
        public async Task AnyShouldReturnTrue()
        {
            await this.reviewsService.CreateAsync(5, "Delicious", "123", 456);

            var result = this.reviewsService.Any();
            Assert.True(result);
        }
        
        [Fact]
        public void GetReviewsShouldReturnEmpty()
        {
            var reviews = reviewsService.GetReviews(1234);
            Assert.Empty(reviews);
        }

        [Fact]
        public async Task ExistsShouldReturnFalse()
        {
            await this.reviewsService.CreateAsync(5, "Delicious", "123", 456);

            var result = reviewsService.Exists(123);

            Assert.False(result);
        }

        [Fact]
        public async Task ExistsShouldReturnTrue()
        {
            await this.reviewsService.CreateAsync(5, "Delicious", "123", 456);
            await this.reviewsService.CreateAsync(1, "Awful", "111", 654);

            var review1Id = reviewsRepository.All().First().Id;
            var review2Id = reviewsRepository.All().Last().Id;

            var result1 = reviewsService.Exists(review1Id);
            var result2 = reviewsService.Exists(review2Id);

            Assert.True(result1);
            Assert.True(result2);
        }
        
        [Fact]
        public async Task DeleteShouldDeleteReviewSuccesfuly()
        {
            await this.reviewsService.CreateAsync(5, "Delicious", "123", 456);
            await this.reviewsService.CreateAsync(1, "Aweful", "111", 654);

            var review1Id = reviewsRepository.All().First().Id;
            await reviewsService.DeleteReviewAsync(review1Id);

            var resultCount = this.reviewsRepository.All().Count();
            var content = this.reviewsRepository.All().First().Content;

            Assert.Equal(1, resultCount);
            Assert.Equal("Aweful", content);
        }

        [Fact]
        public async Task FindReviewAuthourShouldReturnCorrectValue()
        {
            var review = new ProductReview
            {
                Id = 1,
                Content = "Content",
                Rating = 5,
                Author = new ApplicationUser
                {
                    Id = "Id",
                    UserName = "UsernameTest"
                },
                ProductId = 12
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();

            var authorUserName = reviewsService.FindReviewAuthorById(1);
            Assert.Equal("UsernameTest", authorUserName);
        }
    }
}
