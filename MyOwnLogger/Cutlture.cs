using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyOwnLogger
{
    [Route("api/[controller]")]
    public class Culture : Controller
    {
        [HttpGet("{id:int}")]
        public ActionResult SetCulture(int id)
        {
            IRequestCultureFeature culture = HttpContext.Features.Get<IRequestCultureFeature>();
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(new string[] { "en", "ar" }.Where(s=>s!= culture.RequestCulture.Culture.Name).FirstOrDefault())));
            return Redirect($"/studentedit/{id}");
        }
    }
}

