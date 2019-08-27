namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class IndexInputModel
    {
        private const string RequiredError = "Полето е задължително.";
        private const string EmailError = "Форматът трябва да бъде name@domain.com.";

        [Required(ErrorMessage = RequiredError)]
        [EmailAddress(ErrorMessage = EmailError)]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Телефонен номер")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name ="Име")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\\s]+$", ErrorMessage ="Името трябва да съдържа само букви")]
        public string FirstName { get; set; }

        [Display(Name ="Фамилия")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\\s]+$", ErrorMessage = "Фамилията трябва да съдържа само букви")]
        public string LastName { get; set; }

        [Display(Name ="Адрес")]
        public string Adress { get; set; }
    }
}
