using System;
using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lazeez.Service.Service
{
    public class RestaurantMealService : BaseService<tbl_RestaurantMeal, RestaurantMeal>
    {
        public RestaurantMealService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantMeal>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // Delete Source Image
            var sourceImageService = unitOfWork.Service<SourceImageService>();
            sourceImageService.Delete(s => s.SourceID == id && s.SourceTypeID == (short)Enums.SourceType.RestaurantMeal);

            // Delete Restaurant MealException
            var restaurantMealExceptionService = unitOfWork.Service<RestaurantMealExceptionService>();
            restaurantMealExceptionService.Delete(m => m.RestaurantMealID == id);

            // Delete Restaurant Meal
            base.Delete(id);
        }

        public override void Delete(Expression<Func<RestaurantMeal, bool>> whereCondition)
        {
            var sourceImageService = unitOfWork.Service<SourceImageService>();
            var restaurantMealExceptionService = unitOfWork.Service<RestaurantMealExceptionService>();
            var restaurantMeal = GetAll(whereCondition);
            int id;

            for (int i = 0; i < restaurantMeal.Count; i++)
            {
                id = restaurantMeal[i].ID;

                // Delete Source Image
                sourceImageService.Delete(s => s.SourceID == id && s.SourceTypeID == (short)Enums.SourceType.RestaurantMeal);

                // Delete Restaurant MealException
                restaurantMealExceptionService.Delete(m => m.RestaurantMealID == id);
            }

            // Delete Restaurant Meal
            base.Delete(whereCondition);
        }

        #endregion

        #region Business Operations

        public JQueryDataTables<RestaurantMealSearch> Search(RestaurantMealSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);
            var ignoreRestaurantMenu = !prms.RestaurantMenuID.HasValue;
            var ignoreMealException = !prms.RestaurantMealExceptionID.HasValue;
            var mealExceptionFilteration = new List<int>();

            // Declare Repository
            var restaurantMenuRepositry = unitOfWork.Repository<tbl_RestaurantMenu>();

            if (!ignoreMealException)
            {
                mealExceptionFilteration = unitOfWork.Repository<tbl_RestaurantMealException>().Table
                                          .Where(x => x.MealExceptionID == prms.RestaurantMealExceptionID)
                                          .Select(y => y.RestaurantMealID).ToList();
            }

            var restaurantMeals = (from restMeal in repository.Table
                                   join restMenu in restaurantMenuRepositry.Table on restMeal.RestaurantMenuID equals restMenu.ID
                                   where restMeal.IsDeleted == false
                                      && restMeal.RestaurantID == prms.RestaurantID
                                      && (ignoreName || restMeal.Name.Contains(prms.Name))
                                      && (ignoreRestaurantMenu || restMeal.RestaurantMenuID == prms.RestaurantMenuID.Value)
                                      && (ignoreMealException || mealExceptionFilteration.Contains(restMeal.ID))
                                   select new RestaurantMealSearch
                                   {
                                       ID = restMeal.ID,
                                       Name = restMeal.Name,
                                       RestaurantMenu = restMenu.Name,
                                       Calories = restMeal.Calories,
                                       Cost = restMeal.Cost,
                                       NumberOfPerson = restMeal.NumberOfPersons,
                                       Temperature = restMeal.Temperature,
                                       TimeOfMeal = restMeal.TimeOfMeal
                                   })
                                   .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantMealSearch>
            {
                iTotalRecords = restaurantMeals.Count(),
                iTotalDisplayRecords = restaurantMeals.Count(),
                aaData = restaurantMeals.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        public decimal GetCostWithPromotion(int id)
        {
            // Declare Repository
            var restaurantPromotionRepositry = unitOfWork.Repository<tbl_RestaurantPromotion>();
            var restaurantPromotionMealRepositry = unitOfWork.Repository<tbl_RestaurantPromotionMeal>();
            var currentDate = DateTime.Now.Date;

            return (from restMeal in repository.Table

                    join restProMeal in restaurantPromotionMealRepositry.Table on restMeal.ID equals restProMeal.RestaurantMealID into restProMealTemp
                    from restProMeal in restProMealTemp.DefaultIfEmpty()

                    join restPro in restaurantPromotionRepositry.Table on restProMeal.RestaurantPromotionID equals restPro.ID into restProTemp
                    from restPro in restProTemp.Where(restPro => restPro.StartDate <= currentDate && restPro.EndDate >= currentDate).DefaultIfEmpty()

                    where restMeal.IsDeleted == false && restMeal.ID == id
                    select restMeal.Cost - (restMeal.Cost * ((decimal?)restPro.Percentage ?? 0) / 100))
                    .FirstOrDefault();
        }

        public RestaurantMealForAPI GetMealById(int id)
        {
            // Declare Repository
            var restaurantMenuRepositry = unitOfWork.Repository<tbl_RestaurantMenu>();
            var restaurantPromotionRepositry = unitOfWork.Repository<tbl_RestaurantPromotion>();
            var restaurantPromotionMealRepositry = unitOfWork.Repository<tbl_RestaurantPromotionMeal>();

            // Declare Services
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            var currentDate = DateTime.Now.Date;

            var result = (from restMeal in repository.Table

                          join restMenu in restaurantMenuRepositry.Table on restMeal.RestaurantMenuID equals restMenu.ID into restMenuTemp
                          from restMenu in restMenuTemp.DefaultIfEmpty()

                          join restProMeal in restaurantPromotionMealRepositry.Table on restMeal.ID equals restProMeal.RestaurantMealID into restProMealTemp
                          from restProMeal in restProMealTemp.DefaultIfEmpty()

                          join restPro in restaurantPromotionRepositry.Table on restProMeal.RestaurantPromotionID equals restPro.ID into restProTemp
                          from restPro in restProTemp.Where(restPro => restPro.StartDate <= currentDate && restPro.EndDate >= currentDate).DefaultIfEmpty()

                          where restMeal.IsDeleted == false && restMeal.ID == id
                          select new RestaurantMealForAPI
                          {
                              Name = restMeal.Name,
                              RestaurantMenu = restMenu.Name,
                              Contents = restMeal.Contents,
                              Calories = restMeal.Calories,
                              Cost = restMeal.Cost,
                              CostAfterPromotion = restMeal.Cost - (restMeal.Cost * ((decimal?)restPro.Percentage ?? 0) / 100),
                              NumberOfPerson = restMeal.NumberOfPersons,
                              Temperature = restMeal.Temperature,
                              TimeOfMeal = restMeal.TimeOfMeal,
                              PromotionName = restPro.Name,
                              PromotionPercentage = restPro.Percentage
                          })
                         .FirstOrDefault();

            if (result != null)
            {
                result.ListOfImages = sourceImageService.GetImagesURL(id, Enums.SourceType.RestaurantMeal);
            }

            return result;
        }

        public List<RestaurantMealForAPI> GetFilteredResturantMeal(ResturantMealFilter filter)
        {
            // Declare Repository
            var restaurantMenuRepositry = unitOfWork.Repository<tbl_RestaurantMenu>();
            var restaurantPromotionRepositry = unitOfWork.Repository<tbl_RestaurantPromotion>();
            var restaurantPromotionMealRepositry = unitOfWork.Repository<tbl_RestaurantPromotionMeal>();

            // Declare Services
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            var currentDate = DateTime.Now.Date;

            var result = (from restMeal in repository.Table

                          join restMenu in restaurantMenuRepositry.Table on restMeal.RestaurantMenuID equals restMenu.ID into restMenuTemp
                          from restMenu in restMenuTemp.DefaultIfEmpty()

                          join restProMeal in restaurantPromotionMealRepositry.Table on restMeal.ID equals restProMeal.RestaurantMealID into restProMealTemp
                          from restProMeal in restProMealTemp.DefaultIfEmpty()

                          join restPro in restaurantPromotionRepositry.Table on restProMeal.RestaurantPromotionID equals restPro.ID into restProTemp
                          from restPro in restProTemp.Where(restPro => restPro.StartDate <= currentDate && restPro.EndDate >= currentDate).DefaultIfEmpty()

                          where restMeal.IsDeleted == false
                          select new RestaurantMealForAPI
                          {
                              ID = restMeal.ID,
                              Name = restMeal.Name,
                              RestaurantMenu = restMenu.Name,
                              Contents = restMeal.Contents,
                              Calories = restMeal.Calories,
                              Cost = restMeal.Cost,
                              CostAfterPromotion = restMeal.Cost - (restMeal.Cost * ((decimal?)restPro.Percentage ?? 0) / 100),
                              NumberOfPerson = restMeal.NumberOfPersons,
                              Temperature = restMeal.Temperature,
                              TimeOfMeal = restMeal.TimeOfMeal,
                              CreationDate = restMeal.CreationDate,
                              PromotionName = restPro.Name,
                              PromotionPercentage = restPro.Percentage,
                              MealOrderCount = restMeal.tbl_RestaurantOrderMeal.Count
                          })
                         .AsQueryable();

            //** if Type not equal 1 or 0 system will retrive all entities
            // Type = Meal
            //if (filter.Type == 1)
            //    result = result.Where(ex => ex.Type == 1).AsQueryable();

            //else if (Filter.Type == 2) //Coffe
            //    result = result.Where(ex => ex.Type == 2).AsQueryable();

            // if (filter.Type == 3) //Breakfast 
            //    result = result.Where(ex => ex.LstFoodType.Contains("Breakfast")).AsQueryable();

            //else if (filter.Type == 4) //Lunch 
            //    result = result.Where(ex => ex.LstFoodType.Contains("Lunch")).AsQueryable();

            //else if (filter.Type == 5) //Dinner 
            //    result = result.Where(ex => ex.LstFoodType.Contains("Dinner")).AsQueryable();

            //else if (Filter.Type == 6) //Night 
            //    result = result.Where(ex => ex.LstFoodType.Contains("Night")).AsQueryable();

            // Most Ordered
            if (filter.FilterType == 1)
                result = result.OrderByDescending(Ex => Ex.MealOrderCount).AsQueryable();
            else if (filter.FilterType == 2) //Recently Added
                result = result.OrderByDescending(Ex => Ex.CreationDate).AsQueryable();

            var lstMeals = result.Skip(filter.PageNumber * filter.Count).Take(filter.Count).ToList();

            // TODO: Need to refactor to get better performance
            foreach (var meal in lstMeals)
            {
                meal.ListOfImages = sourceImageService.GetImagesURL(meal.ID, Enums.SourceType.RestaurantMeal);
            }

            return lstMeals;
        }

        #endregion
    }
}