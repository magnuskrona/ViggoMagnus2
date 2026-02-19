using Microsoft.AspNetCore.Mvc;

namespace ViggoMagnus.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user = Request.Cookies["user"];
        ViewBag.User = user;
        return View();
    }
}
