namespace OrderService.Models.Order
{
    public class AddOrderModel
    {
        public required int UserID { get; set; }
        public required string Status { get; set; }
    }
}
