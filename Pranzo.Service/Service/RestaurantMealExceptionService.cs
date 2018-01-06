using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using System.Collections.Generic;

namespace Lazeez.Service.Service
{
    public class RestaurantMealExceptionService : BaseService<tbl_RestaurantMealException, RestaurantMealException>
    {
        public RestaurantMealExceptionService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantMealException>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations

        public List<int> GetMealExceptionID(int restaurantMealId)
        {
            return repository.Table
                             .Where(m => m.RestaurantMealID == restaurantMealId)
                             .Select(m => m.MealExceptionID)
                             .ToList();
        }

        #endregion
    }
}