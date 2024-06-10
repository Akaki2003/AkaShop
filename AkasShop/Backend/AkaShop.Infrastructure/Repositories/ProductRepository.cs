using AkaShop.Application.Products.Repositories;
using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AkaShop.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Product> GetProductsAsync()
        {
            return _dbSet.OrderByDescending(x => x.Id);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
        public IQueryable<Product?> GetUserProductsAsync(int userId, CancellationToken cancellationToken)
        {
            return _dbSet.Where(x => x.UserId == userId);
        }
        public async Task<bool> EditProduct(Product product, CancellationToken cancellationToken)
        {
            product.ModifiedAt = DateTime.UtcNow;
            _dbSet.Update(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task RemoveOldProducts()
        {
            await _dbSet.Where(x => (DateTime.UtcNow - x.CreatedAt).Value.Days > 6).ForEachAsync(x => x.DeletedAt = DateTime.UtcNow);
            await _context.SaveChangesAsync();
        }
    }
}
