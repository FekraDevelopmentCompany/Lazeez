using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;
using Lazeez.Service.Service;
using Lazeez.Web.Controllers;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateUser(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = unitOfWork.Service<UserService>();
                var isValid = userService.IsValidUser(model.Name, model.Password);
                return Json(isValid ? new JsonResponse { RedirectToUrl = Url.Action("Index", "Home"), Status = JsonResponse.ResultStatus.Success } : new JsonResponse { Status = JsonResponse.ResultStatus.Error });
            }

            return Json(new JsonResponse { Status = JsonResponse.ResultStatus.Error });
        }
    }
}