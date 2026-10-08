using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace WarehouseManagementSystemWeb.Auth
{
    /// <summary>
    /// Global safety net. When any service call to the API ends with 401 / 403 and the controller did not
    /// handle it, the user is sent to the login page / "access denied" page instead of seeing a crash.
    /// </summary>
    public class ApiExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is not HttpRequestException ex)
            {
                return;
            }

            var http = context.HttpContext;
            var tempData = http.RequestServices
                .GetRequiredService<ITempDataDictionaryFactory>()
                .GetTempData(http);

            if (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                tempData["ErrorMessage"] = "Your session has ended. Please log in again.";

                var returnUrl = HttpMethods.IsGet(http.Request.Method)
                    ? http.Request.Path + http.Request.QueryString
                    : null;

                context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
                context.ExceptionHandled = true;
            }
            else if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                tempData["ErrorMessage"] = "You do not have permission to perform this action.";

                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                context.ExceptionHandled = true;
            }
        }
    }
}
