namespace AkaShop.Application.Products.Responses
{
    public class ProductCreateResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
