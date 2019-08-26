namespace LoWaiLo.WebAPI.ViewModels.Checkout
{
    using System.ComponentModel.DataAnnotations;
    using LoWaiLo.WebAPI.ViewModels.ShoppingCart;

    public class CheckoutInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Име")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\\s]+$", ErrorMessage = "Името трябва да съдържа само букви")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\\s]+$", ErrorMessage = "Фамилията трябва да съдържа само букви")]
        public string LastName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 6)]
        public string Address { get; set; }

        [Phone]
        [Display(Name = "Телефонен номер")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"/^([+]\d{2})?\d{10}$/", ErrorMessage = "Моля въведете валиден номер")]
        public string PhoneNumber { get; set; }

        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}
