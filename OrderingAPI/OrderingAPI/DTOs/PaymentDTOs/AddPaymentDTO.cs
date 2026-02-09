namespace OrderingAPI.DTOs.PaymentDTOs
{
    public class AddPaymentDTO
    {
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
