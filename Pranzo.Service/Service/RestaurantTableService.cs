using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;

namespace Lazeez.Service.Service
{
    public class RestaurantTableService : BaseService<tbl_RestaurantTable, RestaurantTable>
    {
        public RestaurantTableService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantTable>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations

        /// <summary>
        /// Search By GuestCount Range in RestaurantTables 
        /// </summary>
        /// <param name="prms"></param>
        /// <returns></returns>
        public JQueryDataTables<RestaurantTableSearch> Search(RestaurantTableSearchParams prms)
        {
            // Parameters
            var ignoreGuestCountFrom = !prms.GuestCountFrom.HasValue;
            var ignoreGuestCountTo = !prms.GuestCountTo.HasValue;

            var restaurantTables = (from restTable in repository.Table
                                    where restTable.IsDeleted == false
                                       && restTable.RestaurantID == prms.RestaurantID
                                       && (ignoreGuestCountFrom || restTable.GuestCount >= prms.GuestCountFrom)
                                       && (ignoreGuestCountTo || restTable.GuestCount <= prms.GuestCountTo)

                                    select new RestaurantTableSearch
                                    {
                                        ID = restTable.ID,
                                        GuestCount = restTable.GuestCount,
                                        Description = restTable.Description
                                    })
                                    .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantTableSearch>
            {
                iTotalRecords = restaurantTables.Count(),
                iTotalDisplayRecords = restaurantTables.Count(),
                aaData = restaurantTables.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}