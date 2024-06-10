using AkaShop.Application.BaseModels;
using AkaShop.Application.Products;
using AkaShop.Application.Products.Requests;
using AkaShop.Application.Products.Responses;
using AkaShop.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkaShop.Controllers
{
    public class ProductsController(IProductService productService) : BaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ProductListResponseModel> Index([FromQuery]BaseListRequestModel baseListRequestModel, CancellationToken cancellationToken)
        {
            return await _productService.GetProductsAsync(baseListRequestModel,cancellationToken);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ProductResponseModel> Get(int id, CancellationToken cancellationToken)
        {
            return await _productService.GetProductByIdAsync(id, cancellationToken);
        }
        [HttpPost]
        public async Task<int?> Create([FromBody] ProductCreateRequestModel productRequestModel, CancellationToken cancellationToken)
        {
            productRequestModel.UserId = UserId.Value;
            return await _productService.CreateProductAsync(productRequestModel, cancellationToken);
        }
        [HttpGet("UserProducts")]
        public async Task<ProductListResponseModel> GetMyProducts(CancellationToken cancellationToken)
        {
            return await _productService.GetMyProducts(UserId.Value,cancellationToken);
        }
        [HttpPut]
        public async Task<bool> EditProduct([FromBody] ProductEditRequestModel productRequestModel, CancellationToken cancellationToken)
        {
            return await _productService.EditProduct(productRequestModel, cancellationToken);
        }
    }
}
