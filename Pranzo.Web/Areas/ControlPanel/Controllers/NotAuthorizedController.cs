using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Resource.Shared;
using Lazeez.Web.Controllers;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class NotAuthorizedController : BaseController
    {
        // GET: ControlPanel/NotAuthorized
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AjaxNotAuthorized()
        {
            return PartialView("Dialogs/_Message", new MessageDialog
            {
                Title = Resources.Res_NotAuthorized,
                Message = Messages.Msg_NotAuthorized,
                Type = MessageDialog.ButtonType.Error
            });
        }
    }
}