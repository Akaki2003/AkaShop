using Microsoft.AspNetCore.Identity;

namespace AkaShop.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; }
    }


    public class UserRole : IdentityRole<int>
    {
    }

}
