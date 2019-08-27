namespace LoWaiLo.WebAPI.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administrator")]
    [Authorize(Roles = "Administrator, Merchant")]
    public class AdministratorController : Controller
    {
    }
}