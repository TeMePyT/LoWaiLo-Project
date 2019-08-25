namespace LoWaiLo.WebAPI.ViewModels.Checkout
{
    using System.ComponentModel.DataAnnotations;
    using LoWaiLo.WebAPI.ViewModels.ShoppingCart;

    public class CheckoutInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Адрес")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 6)]
        public string Address { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        public ShoppingCartViewModel ShoppingCart { get; set; }
    }
}
