namespace OrderService.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public required int UserID { get; set; }
        public required string Status { get; set; }
    }
}
