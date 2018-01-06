using System.Web.Mvc;
using Lazeez.Web.Controllers;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}