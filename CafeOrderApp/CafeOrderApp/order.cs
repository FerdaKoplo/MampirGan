namespace CafeOrderApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}

