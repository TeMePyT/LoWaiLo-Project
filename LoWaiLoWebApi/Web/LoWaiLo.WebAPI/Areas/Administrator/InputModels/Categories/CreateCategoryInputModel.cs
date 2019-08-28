namespace LoWaiLo.WebAPI.Areas.Administrator.InputModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [StringLength(40, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = "Url на снимката")]
        [DataType(DataType.Url, ErrorMessage = "Моля въведете валиден Url адрес")]
        public string Image { get; set; }
    }
}
