using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;
using System.Collections.Generic;

namespace Lazeez.Service.Service
{
    public class RestaurantPromotionMealService : BaseService<tbl_RestaurantPromotionMeal, RestaurantPromotionMeal>
    {
        public RestaurantPromotionMealService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantPromotionMeal>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations


        /// <summary>
        /// return meals that related to specific pormotion
        /// </summary>
        /// <param name="pormotionId"></param>
        /// <returns></returns>
        public List<int> GetPromotionMeals(int restaurnatPormotionId)
        {
            // Get All Meal That Related to specific pormotion
            return (from restPromotionMeal in repository.Table

                    where restPromotionMeal.IsDeleted == false
                       && restPromotionMeal.RestaurantPromotionID == restaurnatPormotionId
                    // Get Values From RestaurantMeal
                    select restPromotionMeal.RestaurantMealID).ToList();
        }
        
        /// <summary>
        /// Insert into ResturantPormotionMeal Tables List Of Pormoted Meals
        /// </summary>
        /// <param name="pormotionId"></param>
        /// <param name="PromotionMealIds"></param>
        public void Insert(int pormotionId, List<int> restaurantMealIds)
        {
            // Get Meal That Related to Pormotion in DataBase
            List<int> existPromotionMeal = (from restPromotionMeal in repository.Table
                                            where restPromotionMeal.RestaurantPromotionID == pormotionId
                                            select restPromotionMeal.RestaurantMealID
                                             ).ToList();

            // Get New item That Exist in pormotionMeals and Not Exist in DataBase
            List<int> NewPromotionMealIds = restaurantMealIds.Where(x => !existPromotionMeal.Contains(x)).ToList();
            // Insert Meals pormotion That is not exist in dataBase
            foreach (int PromotionMealId in NewPromotionMealIds)
            {
                RestaurantPromotionMeal restPromotionMeal = new RestaurantPromotionMeal();
                restPromotionMeal.RestaurantPromotionID = pormotionId;
                restPromotionMeal.RestaurantMealID = PromotionMealId;
                Insert(restPromotionMeal);
            }

            // Get Deleted Items That Exist In database And not exist on The form
            List<int> DeletedPromotionMealIds = existPromotionMeal.Where(x => !restaurantMealIds.Contains(x)).ToList();
            // delete All Meals That Related to pormotion and not exist in the form
            foreach (int PromotionMealId in DeletedPromotionMealIds)
            {
                int id = (from restPromotionMeal in repository.Table
                          where restPromotionMeal.RestaurantMealID == PromotionMealId
                          where restPromotionMeal.RestaurantPromotionID == pormotionId
                          select restPromotionMeal.ID).FirstOrDefault();
                Delete(id);
            }
        }

        #endregion
    }
}
