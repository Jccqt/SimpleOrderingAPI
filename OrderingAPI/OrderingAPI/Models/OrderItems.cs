namespace OrderingAPI.Models
{
    public class OrderItems
    {
        public int order_item_id { get; set; }
        public required int order_id { get; set; }
        public required int product_id { get; set; }
        public required int quantity { get; set; }
        public required decimal price { get; set; }
    }
}
