using System;
using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Repository.UnitOfWork;

namespace Lazeez.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork unitOfWork = new UnitOfWork();

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            Localization.SetCulture(GlobalSettings.CurrentCulture);
            return base.BeginExecuteCore(callback, state);
        }

        public void SetCulture(string culture, string returnUrl)
        {
            // Set current language
            GlobalSettings.CurrentCulture = culture == "ar" ? Enums.Culture.Ar : Enums.Culture.En;

            // Redirect user
            Response.Redirect(Server.UrlDecode(returnUrl));
        }
    }
}