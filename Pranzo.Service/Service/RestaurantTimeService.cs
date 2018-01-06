using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;

namespace Lazeez.Service.Service
{
    public class RestaurantTimeService : BaseService<tbl_RestaurantTime, RestaurantTime>
    {
        public RestaurantTimeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantTime>();
        }

        #region BaseService Operations

        #endregion

        #region Business Operations

        public JQueryDataTables<RestaurantTimeSearch> Search(RestaurantTimeSearchParams prms)
        {
            // Parameters
            var ignoreDay = prms.Day == null;

            var restaurantTimes = (from rt in repository.Table
                                   where rt.IsDeleted == false
                                         && rt.RestaurantID == prms.RestaurantID
                                         && (ignoreDay || rt.Day == prms.Day)
                                   select new RestaurantTimeSearch
                                   {
                                       ID = rt.ID,
                                       Day = ((Enums.WeekDays)rt.Day).ToString(),
                                       OpenTime = rt.OpenTime,
                                       CloseTime = rt.CloseTime
                                   })
                                   .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantTimeSearch>
            {
                iTotalRecords = restaurantTimes.Count(),
                iTotalDisplayRecords = restaurantTimes.Count(),
                aaData = restaurantTimes.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}