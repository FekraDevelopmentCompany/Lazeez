using System.Web.Mvc;

namespace Pranzo.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restaurant()
        {
            return View();
        }

        public ActionResult Trending()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Collection()
        {
            return View();
        }

        public ActionResult OrderHistory()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}