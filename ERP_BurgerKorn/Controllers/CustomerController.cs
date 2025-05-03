using Microsoft.AspNetCore.Mvc;
using ERP_BurgerKorn.Context;
using ERP_BurgerKorn.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ERP_BurgerKorn.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index: Display all customers
        public IActionResult Index()
        {
            try
            {
                var customers = _context.Customers.ToList();
                return View(customers);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = $"An error occurred while fetching customers. {ex}";
                return View("Error");
            }
        }

        // Create: Display the form to create a new customer
        public IActionResult Create()
        {
            return View();
        }

        // Create: Handle form submission to add a new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            Console.WriteLine("ADD Process");
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = "An error occurred while creating the customer.";
                return View("Error");
            }

        }

        // Edit: Display the form to edit an existing customer
        public IActionResult Edit(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = $"An error occurred while fetching the customer for editing.{ex}";
                return View("Error");
            }
        }

        // Edit: Handle form submission to update an existing customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    _context.Update(customer);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = $"An error occurred while updating the customer.{ex}" ;
                return View("Error");
            }
        }

        // Delete: Display the confirmation page to delete a customer
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = $"An error occurred while fetching the customer for deletion. {ex}";
                return View("Error");
            }
        }

        // Delete: Handle the deletion of a customer
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the error and return an error view
                ViewBag.ErrorMessage = $"An error occurred while deleting the customer. {ex}";
                return View("Error");
            }
        }
    }
}
