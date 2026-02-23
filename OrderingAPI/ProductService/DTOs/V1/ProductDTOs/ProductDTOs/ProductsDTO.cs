namespace ProductService.DTOs.V1.ProductDTOs.ProductDTOs
{
    public class ProductsDTO
    {
        public required int ProductID { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}
