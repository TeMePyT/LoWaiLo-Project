namespace LoWaiLo.WebAPI.Areas.Administrator.InputModels.Addons
{
    using System.ComponentModel.DataAnnotations;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    public class CreateAddonInputModel : IMapTo<Addon>
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [StringLength(40, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [StringLength(40, ErrorMessage = "{0} трябва да бъде най-малко {2} и с най-много {1} знака.", MinimumLength = 3)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Range(0.1, 10000, ErrorMessage = "Полето \"{0}\" трябва да е число в диапазона от {1} до {2}")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
