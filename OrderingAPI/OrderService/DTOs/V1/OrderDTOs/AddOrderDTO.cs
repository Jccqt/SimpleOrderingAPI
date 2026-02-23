namespace OrderService.DTOs.V1.OrderDTOs
{
    public class AddOrderDTO
    {
        public required int UserID { get; set; }
        public required string Status { get; set; }
    }
}
