using AkaShop.Application.BaseModels;
using AkaShop.Application.Products.Repositories;
using AkaShop.Application.Products.Requests;
using AkaShop.Application.Products.Responses;
using AkaShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AkaShop.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<int?> CreateProductAsync(ProductCreateRequestModel product, CancellationToken cancellationToken)
        {
            var productToAdd = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                UserId = product.UserId,
                ImageUrl = product.ImageUrl
            };
            var createdProduct = await _productRepository.CreateProductAsync(productToAdd, cancellationToken);
            return createdProduct?.Id;
        }

        public async Task<ProductResponseModel> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (product == null) return null;
            else
            {
                var result = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    UserId = product.UserId,
                    ImageUrl = product.ImageUrl,
                    CreatedAt = product.CreatedAt
                };
                return result;
            }
        }


        public async Task<ProductListResponseModel> GetProductsAsync(BaseListRequestModel baseListRequestModel, CancellationToken cancellationToken)
        {
            var result = new ProductListResponseModel()
            {
                Items = await _productRepository.GetProductsAsync().Select(product => new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CreatedAt = product.CreatedAt,
                    ImageUrl = product.ImageUrl,
                }).Skip((baseListRequestModel.Page) * baseListRequestModel.PageSize).Take(baseListRequestModel.PageSize).ToListAsync(cancellationToken),
                ItemsCount = await _productRepository.GetProductsCountAsync()
            };
            return result;
        }

        public async Task<ProductListResponseModel> GetMyProducts(int userId, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetUserProductsAsync(userId, cancellationToken).Select(
                x => new ProductResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    UserId = x.UserId,
                    ImageUrl = x.ImageUrl,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description
                }).ToListAsync(cancellationToken);

            var result = new ProductListResponseModel
            {
                Items = products,
                ItemsCount = await _productRepository.GetProductsCountAsync()
            };
            return result;
        }
        public async Task<bool> EditProduct(ProductEditRequestModel product, CancellationToken cancellationToken)
        {
            var productToEdit = await _productRepository.GetProductByIdAsync(product.Id, cancellationToken);
            if (productToEdit == null) return false;
            productToEdit.Name = product.Name;
            productToEdit.Description = product.Description;
            productToEdit.Price = product.Price;
            productToEdit.ImageUrl = product.ImageUrl;
            return await _productRepository.EditProduct(productToEdit, cancellationToken);
        }
    }
}
