namespace LoWaiLo.WebAPI.ViewModels.Addons
{
    using System.ComponentModel.DataAnnotations;

    using LoWaiLo.Data.Models;
    using LoWaiLo.Services.Mapping;
 
    public class AddonViewModel : IMapFrom<Addon>
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name="Описание")]
        public string Description { get; set; }

        [Display(Name="Цена")]
        public decimal Price { get; set; }
    }
}
