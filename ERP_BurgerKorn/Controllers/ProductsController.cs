using Microsoft.AspNetCore.Mvc;
using ERP_BurgerKorn.Context;
using ERP_BurgerKorn.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERP_BurgerKorn.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Supplier).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Supplier).FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Add Product Success! ";

                return RedirectToAction(nameof(Index));

            }
            // >>> เพิ่มตรงนี้ <<< //
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine("Validation Error: " + error.ErrorMessage);
                    TempData["ErrorMessage"] = "Something wrong please try again";
                }
            }
       

            ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name");
            return View(product);
        }


        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.SupplierList = new SelectList(_context.Suppliers, "SupplierId", "Name");
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Edit Product Success!";
                return RedirectToAction(nameof(Index));
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine("Validation Error: " + error.ErrorMessage);
                    TempData["ErrorMessage"] = "Error whiile saving please try again.";
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
