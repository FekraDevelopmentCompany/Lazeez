using System.Web.Http;
using System.Web.Mvc; 

namespace Lazeez.WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
        }
    }
}
