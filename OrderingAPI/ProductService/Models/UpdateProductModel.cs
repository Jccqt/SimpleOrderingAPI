namespace ProductService.Models
{
    public class UpdateProductModel
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
