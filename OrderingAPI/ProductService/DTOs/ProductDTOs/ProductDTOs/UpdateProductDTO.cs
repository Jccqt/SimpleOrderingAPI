namespace ProductService.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
