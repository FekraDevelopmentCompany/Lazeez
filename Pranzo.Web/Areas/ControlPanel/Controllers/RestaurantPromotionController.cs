﻿using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Resource.Shared;
using Lazeez.Service.Helpers;
using Lazeez.Service.Service;
using Lazeez.Web.Controllers;
using Lazeez.Web.Helpers;
using System;
using System.Web.Mvc;
using Lazeez.Web.Helpers.Filters;

namespace Lazeez.Web.Areas.ControlPanel.Controllers
{
    public class RestaurantPromotionController : BaseController
    {
        #region Search

        // GET: ControlPanel/RestaurantPromotion
        [HttpGet]
        [AuthorizeUser]
        public ActionResult Index(int restaurantId)
        {
            // Get Restaurant Name 
            var restaurantService = unitOfWork.Service<RestaurantService>();
            ViewBag.RestaurantID = restaurantId;
            ViewBag.RestaurantName = restaurantService.GetName(restaurantId);

            return View();
        }

        [HttpGet]
        public JsonResult Search(RestaurantPromotionSearchParams prms)
        {
            // Search in Restaurant Promotions
            var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
            var restaurantPromotions = restaurantPromotionService.Search(prms);
            return Json(restaurantPromotions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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
        public ActionResult Delete(ConfirmDialog confirmDialog)
        {
            var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
            restaurantPromotionService.Delete(int.Parse(confirmDialog.Id));
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

            // Get All Restaurant Meals
            var lstRestaurantMeals = listService.Fill_RestaurantMeal(restaurantId);

            var model = new RestaurantPromotionModel
            {
                RestaurantName = restaurantService.GetName(restaurantId)
            };

            // add load all meals in un selected List 
            if (id == -1) // Add
            {
                model.RestaurantPromotionVM = new RestaurantPromotion { RestaurantID = restaurantId };
                model.ListPromotionMeals = ListSettings.Load(lstRestaurantMeals);
            }
            else // Edit
            {
                var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
                model.RestaurantPromotionVM = restaurantPromotionService.GetById(id);

                var restaurantPromotionMealService = unitOfWork.Service<RestaurantPromotionMealService>();
                var promotedMeals = restaurantPromotionMealService.GetPromotionMeals(id);
                model.ListPromotionMeals = ListSettings.Load(lstRestaurantMeals, promotedMeals);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(RestaurantPromotionModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
                
                if (model.RestaurantPromotionVM.ID > 0)    // Edit
                {
                    restaurantPromotionService.Update(model.RestaurantPromotionVM);
                }
                else // Add
                {
                    model.RestaurantPromotionVM.ID = restaurantPromotionService.Insert(model.RestaurantPromotionVM, true);
                }

                unitOfWork.Save();

                // Save meal Promotion 
                if (model.SelectedPromotionMeals.Count > 0)
                {
                    var restaurantPromotionMealService = unitOfWork.Service<RestaurantPromotionMealService>();
                    restaurantPromotionMealService.Insert(model.RestaurantPromotionVM.ID, model.SelectedPromotionMeals);
                    unitOfWork.Save();
                }

                return Json(new JsonResponse { Status = JsonResponse.ResultStatus.Success, RedirectToUrl = Url.Action("Index", new { restaurantId = model.RestaurantPromotionVM.RestaurantID }), Message = Messages.Msg_SaveDone });
            }

            return RedirectToAction("AddEdit", model);
        }

        public JsonResult IsUnique(RestaurantPromotionModel model)
        {
            // Check if Promotion Name  exist or not 
            var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
            var exists = restaurantPromotionService.Exists(rm => rm.ID != model.RestaurantPromotionVM.ID
                                                   && rm.RestaurantID == model.RestaurantPromotionVM.RestaurantID
                                                   && rm.Name == model.RestaurantPromotionVM.Name);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}