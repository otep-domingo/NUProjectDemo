using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUProjectDemo.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;

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
            //check if the user is logged in via session variable _Name
            try
            {
                var u = HttpContext.Session.GetString("_Name");
                if (u == null)
                {
                    return View("../UserLogin/Index");
                }
            }
            catch (Exception ex)
            {
                return View("../UserLogin/Index");
            }
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
            //check if the user is logged in via session variable _Name
            try
            {
                var u = HttpContext.Session.GetString("_Name");
                if (u == null)
                {
                    return View("../UserLogin/Index");
                }
            }
            catch (Exception ex)
            {
                return View("../UserLogin/Index");
            }
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
            //check if the user is logged in via session variable _Name
            try
            {
                var u = HttpContext.Session.GetString("_Name");
                if (u == null)
                {
                    return View("../UserLogin/Index");
                }
            }
            catch (Exception ex)
            {
                return View("../UserLogin/Index");
            }
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

        public ActionResult PrintAll()
        {
            byte[] pdfBytes = null;
            using(var stream = new MemoryStream())
            using(var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))
            using(var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate(),false))
            {
                document.Add(new Paragraph("This is a test")); //this is how to add a text to pdf

                //to add a table
                Table table = new Table(2,false);
                Cell h1 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("ID"))
                    .SetBold();
                table.AddCell(h1);
                Cell h2 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Product Name"))
                    .SetBold();
                table.AddCell(h2);

                foreach(var i in _context.Products)
                {
                    Cell idvalue = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                        .Add(new Paragraph(i.idproducts.ToString()));
                    Cell productname = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                        .Add(new Paragraph(i.productname.ToString()));
                    table.AddCell(idvalue);
                    table.AddCell(productname);

                    //set a border
                    Border b = new SolidBorder(1.0f);
                    table.SetBorderBottom(b);
                    document.Add(table);
                }
                document.Close();
                document.Flush();
                pdfBytes = stream.ToArray();
            }
            //download
            var filename = "ProductsList" + DateTime.Now.ToString() + ".pdf";
            HttpContext.Response.Headers.Add("content-disposition", "inline;filename=" + filename); //you can automate the "sample.pdf" to change to other filename
            return File(fileContents: pdfBytes, "application/pdf");
        }

        public ActionResult PrintId(int id)
        {
            byte[] pdfBytes = null;
            using (var stream = new MemoryStream())
            using (var wri = new PdfWriter(stream))
            using (var pdf = new PdfDocument(wri))
            using (var document = new Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate(), false))
            {
                document.Add(new Paragraph("This is a test")); //this is how to add a text to pdf

                //to add a table
                Table table = new Table(2, false);
                Cell h1 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("ID"))
                    .SetBold();
                table.AddCell(h1);
                Cell h2 = new Cell()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Product Name"))
                    .SetBold();
                table.AddCell(h2);

                foreach (var i in _context.Products.Where(p=>p.idproducts==id))
                {
                    Cell idvalue = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                        .Add(new Paragraph(i.idproducts.ToString()));
                    Cell productname = new Cell()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                        .Add(new Paragraph(i.productname.ToString()));
                    table.AddCell(idvalue);
                    table.AddCell(productname);

                    //set a border
                    Border b = new SolidBorder(1.0f);
                    table.SetBorderBottom(b);
                    document.Add(table);
                }
                document.Close();
                document.Flush();
                pdfBytes = stream.ToArray();
            }
            //download
            var filename = "ProductsList" + DateTime.Now.ToString() + ".pdf";
            HttpContext.Response.Headers.Add("content-disposition", "inline;filename=" + filename); //you can automate the "sample.pdf" to change to other filename
            return File(fileContents: pdfBytes, "application/pdf");
        }
    }
}
