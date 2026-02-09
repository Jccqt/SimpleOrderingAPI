namespace OrderingAPI.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}
