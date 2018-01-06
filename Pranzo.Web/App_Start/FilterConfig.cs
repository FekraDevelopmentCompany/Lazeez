using System.Web.Mvc;
using Lazeez.Web.Helpers.Filters;

namespace Lazeez.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandlerAttribute()); // Custom HandleErrorAttribute
        }
    }
}