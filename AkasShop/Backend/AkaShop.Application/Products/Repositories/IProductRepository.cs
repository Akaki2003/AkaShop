using AkaShop.Domain.Entities;

namespace AkaShop.Application.Products.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken);
        IQueryable<Product?> GetUserProductsAsync(int userId, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        IQueryable<Product> GetProductsAsync();
        Task<bool> EditProduct(Product product, CancellationToken cancellationToken);
        Task<int> GetProductsCountAsync();
        Task RemoveOldProducts();
    }
}
