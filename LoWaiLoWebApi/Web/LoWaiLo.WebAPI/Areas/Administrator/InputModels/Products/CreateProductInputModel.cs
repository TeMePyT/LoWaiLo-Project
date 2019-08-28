namespace LoWaiLo.WebAPI.Areas.Administrator.InputModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateProductInputModel : IMapTo<Product>
    {
        private const string RequiredError = "Полето е задължително.";

        [Display(Name = "Име")]
        [Required(ErrorMessage = RequiredError)]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Name { get; set; }

        [Display(Name = "Номер в менюто")]
        [Required(ErrorMessage = RequiredError)]
        public int MenuNumber { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = RequiredError)]
        [Range(1, 10000, ErrorMessage = "Полето \"{0}\" трябва да е число в диапазона от {1} до {2}")]
        public decimal Price { get; set; }

        [Display(Name = "Тегло")]
        [Required(ErrorMessage = RequiredError)]
        [Range(1, 1500, ErrorMessage = "Полето \"{0}\" трябва да е число в диапазона от {1} до {2} в грамове")]
        public int Weight { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = RequiredError)]
        public int CategoryId { get; set; }

        [Display(Name = "Снимка")]
        [Required(ErrorMessage = RequiredError)]
        public string Image { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }
    }
}
