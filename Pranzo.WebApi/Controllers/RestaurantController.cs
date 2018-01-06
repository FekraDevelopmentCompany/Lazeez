using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lazeez.WebApi.Controllers
{
    public class RestaurantController : BaseController
    {
        [HttpPost]
        public HttpResponseMessage Resturant(ResturantFilter FilterModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                RestaurantService Service = unitOfWork.Service<RestaurantService>();
                List<RestaurantForAPI> LstResturant = Service.GetFilteredResturant(FilterModel);

                if (LstResturant != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true, Data = LstResturant });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "No Resturant Found." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

        [HttpPost]
        public HttpResponseMessage Menu(int RestaurantID)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                RestaurantService Service = unitOfWork.Service<RestaurantService>();
                List<RestaurantForAPI> LstResturant = Service.GetResturantMenu(RestaurantID);

                if (LstResturant != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true, Data = LstResturant });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "No Resturant Found." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }

    }
}