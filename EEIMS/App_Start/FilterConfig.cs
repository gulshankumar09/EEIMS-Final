using System.Web;
using System.Web.Mvc;

namespace EEIMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //to apply global [Authorize] attribute
        }
    }
}
