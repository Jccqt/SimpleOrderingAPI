namespace OrderingAPI.DTOs.OrderItemDTOs
{
    public class AddOrderItemDTO
    {
        public required int OrderID { get; set; }
        public required int ProductID { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
    }
}
