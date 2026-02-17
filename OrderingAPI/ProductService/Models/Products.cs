namespace ProductService.Models
{
    public class Products
    {
        public int product_id { get; set; }
        public required string product_name { get; set; }
        public required decimal price { get; set; }
        public required int stock { get; set; }
    }
}
