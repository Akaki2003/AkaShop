using AkaShop.Application.Products.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace AkaShop.SwaggerExamples
{
        public class ProductCreate : IMultipleExamplesProvider<ProductCreateRequestModel>
        {
            public IEnumerable<SwaggerExample<ProductCreateRequestModel>> GetExamples()
            {
                yield return SwaggerExample.Create("Example 1", new ProductCreateRequestModel
                {
                    Name = "Product 1",
                    Description = "Description of product 1",
                    Price = 100,
                    ImageUrl = "https://via.placeholder.com/150"
                });

                yield return SwaggerExample.Create("Example 2", new ProductCreateRequestModel
                {
                    Name = "Product 2",
                    Description = "Description of product 2",
                    Price = 200,
                    ImageUrl = "https://via.placeholder.com/150"
                });
            }
        }
}
