﻿using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;
using Lazeez.Resource.Shared;
using Lazeez.Service.Service;
using Lazeez.Web.Controllers;
using Lazeez.Model.DBModel;
using Lazeez.Service.Helpers;
using Lazeez.Web.Helpers;
using Lazeez.Web.Helpers.Filters;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class RestaurantMenuController : BaseController
    {
        #region Search

        // GET: ControlPanel/Restaurant Menu
        [HttpGet]
        [AuthorizeUser]
        public ActionResult Index(int restaurantId)
        {
            var restaurantService = unitOfWork.Service<RestaurantService>();
            ViewBag.RestaurantID = restaurantId;
            ViewBag.RestaurantName = restaurantService.GetName(restaurantId);

            return View();
        }

        [HttpGet]
        public JsonResult Search(RestaurantMenuSearchParams prms)
        {
            var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();
            var restaurantMenus = restaurantMenuService.Search(prms);
            return Json(restaurantMenus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizeUser]
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
        [AuthorizeUser]
        public ActionResult Delete(ConfirmDialog confirmDialog)
        {
            var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();
            restaurantMenuService.Delete(int.Parse(confirmDialog.Id));
            unitOfWork.Save();
            return JavaScript("refreshGrid();");
        }

        #endregion

        #region AddEdit

        [HttpGet]
        [AuthorizeUser]
        public ActionResult AddEdit(int id, int restaurantId)
        {
            var restaurantService = unitOfWork.Service<RestaurantService>();
            var listService = unitOfWork.Service<ListService>();

            var lstFoodTypes = listService.Fill_FoodType(restaurantId);

            var model = new RestaurantMenuModel
            {
                RestaurantName = restaurantService.GetName(restaurantId),
                ListFoodTypes = ListSettings.Load(lstFoodTypes, string.Empty)
            };

            if (id == -1) // Add
            {
                model.RestaurantMenuVM = new RestaurantMenu { RestaurantID = restaurantId };
            }
            else // Edit
            {
                var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();
                model.RestaurantMenuVM = restaurantMenuService.GetById(id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(RestaurantMenuModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();

                if (model.RestaurantMenuVM.ID > 0) // Edit
                {
                    restaurantMenuService.Update(model.RestaurantMenuVM);
                    unitOfWork.Save();
                }
                else // Add
                {
                    model.RestaurantMenuVM.ID = restaurantMenuService.Insert(model.RestaurantMenuVM, true);
                }

                return Json(new JsonResponse { ID = model.RestaurantMenuVM.ID, Status = JsonResponse.ResultStatus.Success, RedirectToUrl = Url.Action("Index", new { restaurantId = model.RestaurantMenuVM.RestaurantID }), Message = Messages.Msg_SaveDone });
            }

            return RedirectToAction("AddEdit", model);
        }

        public JsonResult IsUnique(RestaurantMenuModel model)
        {
            var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();
            var exists = restaurantMenuService.Exists(rm => rm.ID != model.RestaurantMenuVM.ID
                                                   && rm.RestaurantID == model.RestaurantMenuVM.RestaurantID
                                                   && rm.Name == model.RestaurantMenuVM.Name);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}