using AkaShop.Application.BaseModels;

namespace AkaShop.Application.Products.Responses
{
    public class ProductListResponseModel
    {
        public List<ProductResponseModel> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
