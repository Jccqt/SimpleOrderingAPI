namespace ProductService.DTOs.ProductDTOs
{
    public class ProductsDTO
    {
        public required int ProductID { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}
