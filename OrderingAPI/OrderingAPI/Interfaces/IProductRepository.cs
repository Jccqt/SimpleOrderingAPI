using OrderingAPI.DTOs.ProductDTOs;
using OrderingAPI.Models;

namespace OrderingAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProduct(int id);
        Task AddProduct(AddProductDTO product);
        Task<bool> UpdateProduct(int id, UpdateProductDTO product);
    }
}
