namespace OrderService.Models.Order
{
    public class OrdersWithUserInfoModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
