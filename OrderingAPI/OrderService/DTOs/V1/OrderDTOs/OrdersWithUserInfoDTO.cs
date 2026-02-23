namespace OrderService.DTOs.V1.OrderDTOs
{
    public class OrdersWithUserInfoDTO
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
