namespace ProductService.Models
{
    public class AddProductModel
    {
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}
