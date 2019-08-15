namespace LoWaiLo.WebAPI.ViewModels.Reviews.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateProductReviewInputModel
    {
        private const int ProductReviewMinimumLength = 6;
        private const string ProductReviewErrorMessage = "Съдържанието трябва да е дълго минимум 6 символа.";

        [Required(ErrorMessage = "Полето е задължително")]
        [MinLength(ProductReviewMinimumLength, ErrorMessage = ProductReviewErrorMessage)]
        public string Content { get; set; }

        public int Rating { get; set; }

        public int ProductId { get; set; }
    }
}
