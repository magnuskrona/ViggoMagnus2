using Microsoft.AspNetCore.Mvc;
using ViggoMagnus.Services;

namespace ViggoMagnus.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _users;

    public AccountController(IUserService users)
    {
        _users = users;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Username and password are required.";
            return View();
        }

        var ok = _users.Register(username, password);
        if (!ok)
        {
            ViewBag.Error = "User already exists.";
            return View();
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (_users.Validate(username, password))
        {
            // Simple: set a cookie with username (not secure, demo only)
            Response.Cookies.Append("user", username);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid credentials.";
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("user");
        return RedirectToAction("Index", "Home");
    }
}
