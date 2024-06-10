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
                Email = "Admin@gmail.com",
                Password = "Admin123!"
            };
        }
    }
}
