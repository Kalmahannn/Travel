using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TravelistaMVC.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
