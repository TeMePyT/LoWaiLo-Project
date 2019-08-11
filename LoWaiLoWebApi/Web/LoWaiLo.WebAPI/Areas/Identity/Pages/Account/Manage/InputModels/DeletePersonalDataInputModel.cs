namespace LoWaiLo.WebAPI.Areas.Identity.Pages.Account.Manage.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class DeletePersonalDataInputModel
    {
        private const string RequiredError = "Полето е задължително.";

        [Required(ErrorMessage = RequiredError)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
