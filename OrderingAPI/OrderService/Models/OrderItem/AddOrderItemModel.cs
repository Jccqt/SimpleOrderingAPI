namespace OrderService.Models.OrderItem
{
    public class AddOrderItemModel
    {
        public required int OrderID { get; set; }
        public required int ProductID { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
    }
}
