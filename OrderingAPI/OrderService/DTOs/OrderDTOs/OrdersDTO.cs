namespace OrderService.DTOs.OrderDTOs
{
    public class OrdersDTO
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
