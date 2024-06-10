using AkaShop.Application.BaseModels;
using AkaShop.Application.Products.Requests;
using AkaShop.Application.Products.Responses;

namespace AkaShop.Application.Products
{
    public interface IProductService
    {
        Task<int?> CreateProductAsync(ProductCreateRequestModel product,CancellationToken cancellationToken);
        Task<bool> EditProduct(ProductEditRequestModel product, CancellationToken cancellationToken);
        Task<ProductListResponseModel> GetMyProducts(int userId, CancellationToken cancellationToken);
        Task<ProductResponseModel> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task<ProductListResponseModel> GetProductsAsync(BaseListRequestModel baseListRequestModel, CancellationToken cancellationToken);
    }
}
