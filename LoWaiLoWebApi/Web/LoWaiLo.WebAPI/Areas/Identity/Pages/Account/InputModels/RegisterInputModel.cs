namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        private const string RequiredError = "Полето е задължително.";
        private const string EmailError = "Форматът трябва да бъде name@domain.com.";

        [Required(ErrorMessage = RequiredError)]
        [EmailAddress(ErrorMessage = EmailError)]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Паролата и паролата за потвърждение не съвпадат.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Име")]
        public string FirsName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
