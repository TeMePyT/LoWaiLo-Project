namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordInputModel
    {
        private const string RequiredError = "Полето е задължително.";
        private const string EmailError = "Форматът трябва да бъде name@domain.com.";

        [Required(ErrorMessage = RequiredError)]
        [EmailAddress(ErrorMessage = EmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Паролата и паролата за потвърждение не съвпадат.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
