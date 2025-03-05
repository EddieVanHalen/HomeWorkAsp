using Microsoft.AspNetCore.Mvc;

namespace MVCAPP.Controllers;

public class AlbumsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}