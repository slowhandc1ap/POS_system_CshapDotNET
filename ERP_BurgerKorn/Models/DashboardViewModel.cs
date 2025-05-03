namespace ERP_BurgerKorn.Models
{
    public class DashboardViewModel
    {
        public decimal TotalSalesToday { get; set; }
        public int TotalOrdersToday { get; set; }
        public int LowStockCount { get; set; }
        public int NewCustomersToday { get; set; }
    }
}
