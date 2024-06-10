using AkaShop.Application.Users.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace AkaShop.SwaggerExamples
{
    public class UserLoginRequestExample : IExamplesProvider<UserModel>
    {
        public UserModel GetExamples()
        {
            return new UserModel()
            {
                Email = "akaki123",
                Password = "Akaki1!"
            };
        }
    }
}
