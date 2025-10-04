using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

public class Controller1 : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}