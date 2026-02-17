namespace OrderService.DTOs.OrderDTOs
{
    public class OrderItemDetailsDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
