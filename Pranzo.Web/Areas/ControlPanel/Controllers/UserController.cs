using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;
using Lazeez.Resource.Shared;
using Lazeez.Service.Service;
using Lazeez.Web.Controllers;
using Lazeez.Model.DBModel;
using Lazeez.Web.Helpers;
using Lazeez.Service.Helpers;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class UserController : BaseController
    {
        #region Search

        // GET: ControlPanel/Restaurant
        // [AuthorizeUser]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Search(UserSearchParams prms)
        {
            ViewBag.UserTypeId = (int)GlobalSettings.CurrentUserTypeID;

            var userService = unitOfWork.Service<UserService>();
            var Users = userService.Search(prms);
            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        // [AuthorizeUser]
        public PartialViewResult Delete(string id)
        {
            return PartialView("Dialogs/_Confirm", new ConfirmDialog
            {
                Id = id,
                Action = RouteData.Values["action"].ToString(),
                Controller = RouteData.Values["controller"].ToString(),
                Title = Resources.Res_Confirm,
                Message = Messages.Msg_ConfirmDelete
            });
        }

        [HttpPost]
        // [AuthorizeUser]
        public ActionResult Delete(ConfirmDialog confirmDialog)
        {
            var userService = unitOfWork.Service<UserService>();
            userService.Delete(int.Parse(confirmDialog.Id));
            unitOfWork.Save();
            return JavaScript("refreshGrid();");
        }

        #endregion

        #region AddEdit

        public ActionResult AddEdit(int id = -1)
        {
            ListService listService = unitOfWork.Service<ListService>();
            UserModel model = new UserModel
            {
                userType = GlobalSettings.CurrentUserTypeID,
                ListBranches = ListSettings.Load(listService.Fill_RestaurantBranches(GlobalSettings.CurrentUserID), string.Empty)
            };

            if (id == -1) // Add
            {
                model.UserVM = new User();
            }
            else // Edit
            {
                var userService = unitOfWork.Service<UserService>();
                model.UserVM = userService.GetById(id);
                //model.UserVM.ConfirmPassword = model.UserVM.Password;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = unitOfWork.Service<UserService>();

                //if (GlobalSettings.CurrentUserTypeID == Enums.UserType.LazeezAdmin)
                //{
                //    model.UserVM.UserTypeID = (int)Enums.UserType.RestaurantAdmin;
                //}

                if (model.UserVM.ID > 0)    // Edit
                {
                    // Get password from database 
                    User user = userService.GetById(model.UserVM.ID);
                    if (user.Password != model.UserVM.Password)
                        model.UserVM.Password = StringCipher.Hash(model.UserVM.Password);

                    userService.Update(model.UserVM);
                    unitOfWork.Save();
                }
                else // Add
                {
                    model.UserVM.Password = StringCipher.Hash(model.UserVM.Password);
                    model.UserVM.ID = userService.Insert(model.UserVM, true);
                }

                return Json(new JsonResponse { ID = model.UserVM.ID, Status = JsonResponse.ResultStatus.Success, RedirectToUrl = Url.Action("Index"), Message = Messages.Msg_SaveDone });
            }

            return RedirectToAction("AddEdit", model);
        }

        public JsonResult IsUnique(UserModel model)
        {
            var userService = unitOfWork.Service<UserService>();
            int cureentUser = GlobalSettings.CurrentUserID;
            var exists = userService.Exists(r => r.ID != model.UserVM.ID && r.Name == model.UserVM.Name && r.CreatorUserID == cureentUser);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
