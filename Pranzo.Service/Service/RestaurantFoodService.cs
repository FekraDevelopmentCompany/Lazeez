using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;

namespace Lazeez.Service.Service
{
    public class RestaurantFoodService : BaseService<tbl_RestaurantFood, RestaurantFood>
    {
        public RestaurantFoodService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantFood>();
        }

        #region BaseService Operations

        #endregion

        #region Business Operations

        public JQueryDataTables<RestaurantFoodSearch> Search(RestaurantFoodSearchParams prms)
        {
            // Parameters
            var ignoreFoodTypeId = prms.FoodTypeID == null;

            var foodTypeRepositry = unitOfWork.Repository<lkp_FoodType>();

            var restaurantFoods = (from rf in repository.Table
                                   join foodType in foodTypeRepositry.Table on rf.FoodTypeID equals foodType.ID
                                   where rf.IsDeleted == false
                                      && rf.RestaurantID == prms.RestaurantID
                                      && (ignoreFoodTypeId || rf.FoodTypeID == prms.FoodTypeID)
                                   select new RestaurantFoodSearch
                                   {
                                       ID = rf.ID,
                                       FoodType = foodType.Name
                                   })
                                   .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantFoodSearch>
            {
                iTotalRecords = restaurantFoods.Count(),
                iTotalDisplayRecords = restaurantFoods.Count(),
                aaData = restaurantFoods.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}