using Microsoft.AspNetCore.Mvc;

namespace MVCAPP.Controllers;

public class BandsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}