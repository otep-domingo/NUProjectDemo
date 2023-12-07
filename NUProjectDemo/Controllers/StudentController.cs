using Microsoft.AspNetCore.Mvc;

namespace NUProjectDemo.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            int status = 0; //0 for maintenance, 1 for ok status
            ViewData["Message"] = "This handles all student concerns in NU. You must have an account to access the other modules.";
            if (status == 0)
            {
                return View("Views/ErrorCollection/Maintenance.cshtml");
            }
            else
            {
                return View("Views/Home/Privacy.cshtml");
            }
            return View("Views/Home/Privacy.cshtml");
        }
    }
    
}
