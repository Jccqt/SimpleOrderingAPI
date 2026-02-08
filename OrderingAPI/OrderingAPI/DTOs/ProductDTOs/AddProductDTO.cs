namespace OrderingAPI.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public required string ProductName { get; set; }
        public required decimal price { get; set; }
        public required int stock { get; set; }
    }
}
