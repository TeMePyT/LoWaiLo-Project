namespace LoWaiLo.WebAPI.ViewModels.Reviews.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewInputModel
    {
        private const int ReviewMinimumLength = 4;
        private const string ReviewErrorMessage = "Съдържанието трябва да е дълго минимум 6 символа.";

        [Required]
        [MinLength(ReviewMinimumLength, ErrorMessage = ReviewErrorMessage)]
        public string Content { get; set; }


        public int Rating { get; set; }
    }
}
