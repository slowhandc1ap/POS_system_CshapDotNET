using System.Diagnostics;
using ERP_BurgerKorn.Context;
using ERP_BurgerKorn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ERP_BurgerKorn.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

      

        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            var todayOrders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderDate.Date == today)
                .ToList();

            var totalSales = todayOrders
                .SelectMany(o => o.OrderItems)
                .Sum(oi => oi.UnitPrice * oi.Quantity);

            var orderCount = todayOrders.Count();

            var lowStock = _context.Products.Count(p => p.StockQuantity < 1000);

            var newCustomers = _context.Customers
                .Where(c => c.CreatedAt.Date == today)
                .Count();

            var viewModel = new DashboardViewModel
            {
                TotalSalesToday = totalSales,
                TotalOrdersToday = orderCount,
                LowStockCount = lowStock,
                NewCustomersToday = newCustomers
            };
            Console.WriteLine($"value: Total sales = {totalSales} , Total Order Today = {orderCount}, LowStockCount = {lowStock} , New Cusotmer Today = {newCustomers}");

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
