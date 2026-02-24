namespace OrderService.Models.Payment
{
    public class AddPaymentModel
    {
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
