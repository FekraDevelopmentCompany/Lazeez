using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lazeez.WebApi.Controllers
{
    public class LocationController : ApiController
    {
        public HttpResponseMessage GetAllLocations()
        {
            try
            {
                IUnitOfWork unitOfWork = new UnitOfWork();
                CityService Service = unitOfWork.Service<CityService>();
                List<City> LstLocation = Service.GetLocation();

                if (LstLocation != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = true , Data = LstLocation});
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new APIResponse { State = false, Message = "No Location Found." });
            }
            catch (Exception ex)
            {
                HttpError ErrorMsg = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMsg);
            }
        }
    }
}
