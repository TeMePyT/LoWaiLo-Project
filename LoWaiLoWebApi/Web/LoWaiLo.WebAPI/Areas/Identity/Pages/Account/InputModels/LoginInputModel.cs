namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage =RequiredError)]
        [Display(Name = "Потребителско име:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола:")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }
}
