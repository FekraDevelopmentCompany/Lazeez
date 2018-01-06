using System.Collections.Generic;
using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;

namespace Lazeez.Service.Helpers
{
    public class ListService
    {
        readonly UnitOfWork _unitOfWork;

        public ListService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  Fill Restaurant DropDownlist
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_Restaurant()
        {
            var repository = _unitOfWork.Repository<tbl_Restaurant>();

            return repository.Table
                .Where(r => r.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        ///  Fill Food DropDownlist
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_FoodType()
        {
            var repository = _unitOfWork.Repository<lkp_FoodType>();

            return repository.Table
                .Where(f => f.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill Food DropDownlist
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public List<KeyValuePair> Fill_FoodType(int restaurantId)
        {
            // Declare Repository
            var foodTyperepository = _unitOfWork.Repository<lkp_FoodType>();
            var restaurantFoodRepositry = _unitOfWork.Repository<tbl_RestaurantFood>();

            return (from foodType in foodTyperepository.Table
                    join restFood in restaurantFoodRepositry.Table on foodType.ID equals restFood.FoodTypeID
                    where foodType.IsDeleted == false && restFood.RestaurantID == restaurantId
                    select new KeyValuePair
                    {
                        Value = foodType.ID.ToString(),
                        Text = foodType.Name
                    }).ToList();
        }

        /// <summary>
        /// Fill Country Drop DownList
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_Country()
        {
            var repository = _unitOfWork.Repository<lkp_Country>();

            return repository.Table
                .Where(f => f.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill City Drop DownList
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_City()
        {
            var repository = _unitOfWork.Repository<lkp_City>();

            return repository.Table
                .Where(f => f.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill District Drop DownList
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_District()
        {
            var repository = _unitOfWork.Repository<lkp_District>();

            return repository.Table
                .Where(f => f.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill Meal Exception Drop DownList
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_MealException()
        {
            var repository = _unitOfWork.Repository<lkp_MealException>();

            return repository.Table
                .Where(f => f.IsDeleted == false)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill Restaurant Meal Drop DownList 
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair> Fill_RestaurantMeal(int restaurantId)
        {
            var repository = _unitOfWork.Repository<tbl_RestaurantMeal>();

            return repository.Table
                .Where(f => f.IsDeleted == false && f.RestaurantID == restaurantId)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill Restaurant Menu Drop DownList
        /// </summary>
        /// <param name="restaurantId">RestaurantId</param>
        /// <returns></returns>
        public List<KeyValuePair> Fill_RestaurantMenu(int restaurantId)
        {
            var repository = _unitOfWork.Repository<tbl_RestaurantMenu>();

            return repository.Table
                .Where(f => f.IsDeleted == false && f.RestaurantID == restaurantId)
                .Select(f => new KeyValuePair
                {
                    Value = f.ID.ToString(),
                    Text = f.Name
                }).ToList();
        }

        /// <summary>
        /// Fill Restaurant Branches
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public List<KeyValuePair> Fill_RestaurantBranches(int userId)
        {
            var repository = _unitOfWork.Repository<tbl_RestaurantBranch>();

            return repository.Table
                             .Where(b => b.IsDeleted == false && b.CreatorUserID == userId)
                             .Select(f => new KeyValuePair
                             {
                                 Value = f.ID.ToString(),
                                 Text = f.Name
                             }).ToList();
        }
    }
}