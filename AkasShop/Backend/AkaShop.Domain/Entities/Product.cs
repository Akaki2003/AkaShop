using AkaShop.Domain.Abstraction;

namespace AkaShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public User User { get; set; }
    }
}
