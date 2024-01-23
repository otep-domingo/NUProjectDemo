using Microsoft.AspNetCore.Mvc;

namespace NUProjectDemo.Controllers
{
    public class EmptyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
