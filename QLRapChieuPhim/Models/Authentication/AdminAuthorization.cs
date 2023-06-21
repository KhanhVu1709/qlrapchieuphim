using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QLRapChieuPhim.Models.Authentication
{
    public class AdminAuthorization : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.User.IsInRole("Admin") == false)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Home" },
                        {"Action", "Index" }
                    });
            }
        }
    }
}
