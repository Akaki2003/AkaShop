using System.Text.Json.Serialization;

namespace AkaShop.Application.Products.Requests
{
    public class ProductCreateRequestModel
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
