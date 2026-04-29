using OrderingAPI.Shared.Models.Responses;
using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductRepository
    {
        Task<ServiceResponse<object>> GetAllProducts();
        Task<ServiceResponse<object>> GetProduct(int productID);
        Task AddProduct(AddProductModel product);
        Task<bool> UpdateProduct(int productID, UpdateProductModel product);
    }
}
