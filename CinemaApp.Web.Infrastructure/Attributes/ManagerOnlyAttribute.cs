namespace CinemaApp.Web.Infrastructure.Attributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using static Common.ApplicationConstants;

    public class ManagerOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (context != null)
            {
                bool retrieveCookieRes =
                    context.HttpContext.Request.Cookies.TryGetValue(IsManagerCookieName, out string isManagerStr);
                if (!retrieveCookieRes)
                {
                    context.Result = new UnauthorizedResult();
                }

                bool canParseValue = bool.TryParse(isManagerStr, out bool isManager);
                if (!canParseValue)
                {
                    context.Result = new UnauthorizedResult();
                }

                if (!isManager)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
