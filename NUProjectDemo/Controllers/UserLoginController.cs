using Microsoft.AspNetCore.Mvc;
using NUProjectDemo.Models;
using NUProjectDemo.Data;

namespace NUProjectDemo.Controllers
{
    public class UserLoginController : Controller
    {
        public const string LogoutMessage = "You have been logged out";
        public const string SessionKeyName = "_Name";
        private readonly ApplicationDbContext _context;

        public UserLoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is to display the login page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

    
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(u); //preparing the SQL statement for add
                    _context.SaveChanges(); //responsible for executing the add sql
                    return RedirectToAction(nameof(Index)); // return View("Index");
                }
                catch
                {
                    return View();
                }
                return View();
            }
            return View(u);
        }

        public IActionResult Authenticate(Login u)
        {
            if (u != null)
            {
                if (ModelState.IsValid)
                {
                    var account = from m in _context.Users select m;
                    account = account.Where(s => s.email.Contains(u.email));
                    //select * from users where email = u.email
                    if (account.Count() != 0)
                    {
                        if(account.First().password == u.password)
                        {
                            //set the session
                            HttpContext.Session.SetString(SessionKeyName, u.email);
                            return View("../Home/Index");
                        }
                    }
                    else
                    {
                        return View("Invalid");
                    }
                }
            }
            return View("Invalid");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
