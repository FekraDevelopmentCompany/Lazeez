using Lazeez.Model.ViewModel;
using Lazeez.Service.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lazeez.WebApi.Controllers
{
    public class RestaurantMealController : BaseController
    {
        [HttpGet]
        public HttpResponseMessage GetMealById(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, @"ID cannot be zero");

                var restaurantMealService = unitOfWork.Service<RestaurantMealService>();
                var meal = restaurantMealService.GetMealById(id);

                if (meal != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true, Data = meal });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "No Meal Found." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetFilteredResturantMeal(ResturantMealFilter filter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                var restaurantMealService = unitOfWork.Service<RestaurantMealService>();
                var lstMeals = restaurantMealService.GetFilteredResturantMeal(filter);

                if (lstMeals != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true, Data = lstMeals });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "No Meal Found." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }
    }
}