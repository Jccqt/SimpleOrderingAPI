using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProduct(int productID);
        Task AddProduct(AddProductModel product);
        Task<bool> UpdateProduct(int productID, UpdateProductModel product);
    }
}
