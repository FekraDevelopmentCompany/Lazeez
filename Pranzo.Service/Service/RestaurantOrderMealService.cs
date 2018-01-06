using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using System.Collections.Generic;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class RestaurantOrderMealService : BaseService<tbl_RestaurantOrderMeal, RestaurantOrderMeal>
    {
        public RestaurantOrderMealService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantOrderMeal>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations
        
        /// <summary>
        /// return all meals that related to specific order
        /// </summary>
        /// <param name="restaurantOrderId"></param>
        /// <returns></returns>
        public List<int> GetRestaurantMealIds(int restaurantOrderId)
        {
            // Get all meals that related to specific order
            return (from restOrderMeal in repository.Table
                    where restOrderMeal.IsDeleted == false
                       && restOrderMeal.RestaurantOrderID == restaurantOrderId
                    select restOrderMeal.RestaurantMealID).ToList();
        }

        #endregion
    }
}