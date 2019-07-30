namespace LoWaiLo.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PaymentType
    {
        [Display(Name = "ePay.bg")]
        Epay = 1,

        [Display(Name = "В брой (При доставка)")]
        EasyPay = 2,

        [Display(Name = "Visa, MasterCard и др.")]
        Card = 3,
    }
}
