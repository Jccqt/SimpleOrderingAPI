namespace OrderingAPI.DTOs.ProductDTOs
{
    public class ProductsDTO
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }
}
