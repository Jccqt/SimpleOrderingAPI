namespace OrderService.Models
{
    public class Payments
    {
        public int payment_id { get; set; }
        public required int order_id { get; set; }
        public required string payment_method { get; set; }
        public required decimal amount { get; set; }
        public required DateTime payment_date { get; set; }
    }
}
