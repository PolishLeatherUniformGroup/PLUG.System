using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ONPA.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class LanguageController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture, culture)));
            }

            return LocalRedirect(redirectUri);
        }
    }
}
