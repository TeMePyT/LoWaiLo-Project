namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class SetPasswordInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди новата парола")]
        [Compare("NewPassword", ErrorMessage = "Новата парола и паролата за потвърждение не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}