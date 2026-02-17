namespace OrderService.Models
{
    public class Orders
    {
        public required int order_id { get; set; }
        public required int user_id { get; set; }
        public required DateTime order_date { get; set; }
        public required string status { get; set; }
    }
}
