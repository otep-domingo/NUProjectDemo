using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

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
            return View();
        }
        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
    
}
