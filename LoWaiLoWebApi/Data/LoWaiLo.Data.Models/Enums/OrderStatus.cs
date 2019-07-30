namespace LoWaiLo.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum OrderStatus
    {
        [Display(Name = "В процес...")]
        Pending = 1,

        [Display(Name = "Приета")]
        Approved = 2,

        [Display(Name = "Изпратена")]
        Shipping = 3,

        [Display(Name = "Доставена")]
        Delivered = 4,
    }
}
