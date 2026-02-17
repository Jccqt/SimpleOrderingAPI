namespace OrderService.DTOs.PaymentDTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
