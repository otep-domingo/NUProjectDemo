using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUProjectDemo.Data;

namespace NUProjectDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public ActionResult Index2()
        {
            return View(_context.Products.ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var p = _context.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public ActionResult Create([Bind()] Models.Products product)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(product); //preparing the SQL statement for add
                    _context.SaveChanges(); //responsible for executing the add sql
                    return RedirectToAction(nameof(Index)); // return View("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var p = _context.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Models.Products p = new Models.Products();
                p.idproducts = int.Parse(collection["idproducts"]);
                p.productname = collection["productname"];
                p.datetimeadded = DateTime.Parse(collection["datetimeadded"]);
                p.category = collection["category"];
                p.price = double.Parse(collection["price"]);
                p.description = collection["description"];

                _context.Update(p);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //public ActionResult Edit(int id, [Bind()]Models.Products product)
        //{
        //    if (id != product.idproducts)
        //    {
        //        return View("InvalidAction");

        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product); //preparing the SQL statement for update
        //            _context.SaveChanges(); //responsible for executint eh update
        //            return RedirectToAction(nameof(Index)); // return View("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //    return View(product);
        //}

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var p = _context.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind()] Models.Products product)
        {
            {
                if (id != product.idproducts)
                {
                    return View("InvalidAction");

                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Remove(product); //preparing the SQL statement for delete
                        _context.SaveChanges(); //responsible for executing the delete
                        return RedirectToAction(nameof(Index)); // return View("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }
                return View(product);
            }
        }
    }
}
