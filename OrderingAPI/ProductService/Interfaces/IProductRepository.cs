using ProductService.DTOs.ProductDTOs;
using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProduct(int productID);
        Task AddProduct(AddProductDTO product);
        Task<bool> UpdateProduct(int productID, UpdateProductDTO product);
    }
}
