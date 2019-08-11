namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordInputModel
    {
        private const string RequiredError = "Полето е задължително.";
        private const string EmailError = "Форматът трябва да бъде name@domain.com.";

        [Required(ErrorMessage = RequiredError)]
        [EmailAddress(ErrorMessage = EmailError)]
        public string Email { get; set; }
    }
}
