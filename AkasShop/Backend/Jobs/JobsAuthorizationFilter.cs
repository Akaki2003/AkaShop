using Hangfire.Dashboard;

namespace Jobs
{
    public class JobsAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
