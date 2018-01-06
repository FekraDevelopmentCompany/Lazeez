using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class RestaurantService : BaseService<tbl_Restaurant, Restaurant>
    {
        public RestaurantService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_Restaurant>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // Delete Restaurant Branch
            var restaurantBranchService = unitOfWork.Service<RestaurantBranchService>();
            restaurantBranchService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Food
            var restaurantFoodService = unitOfWork.Service<RestaurantFoodService>();
            restaurantFoodService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Menu
            var restaurantMenuService = unitOfWork.Service<RestaurantMenuService>();
            restaurantMenuService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Time
            var restaurantTimeService = unitOfWork.Service<RestaurantTimeService>();
            restaurantTimeService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Meal
            var restaurantMealService = unitOfWork.Service<RestaurantMealService>();
            restaurantMealService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Promotion
            var restaurantPromotionService = unitOfWork.Service<RestaurantPromotionService>();
            restaurantPromotionService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Table
            var restaurantTableService = unitOfWork.Service<RestaurantTableService>();
            restaurantTableService.Delete(r => r.RestaurantID == id);

            // Delete Restaurant Order
            var restaurantOrderService = unitOfWork.Service<RestaurantOrderService>();
            restaurantOrderService.Delete(r => r.RestaurantID == id);

            // Delete Source Image
            var sourceImageService = unitOfWork.Service<SourceImageService>();
            sourceImageService.Delete(s => s.SourceID == id && s.SourceTypeID == (short)Enums.SourceType.Restaurant);

            // Delete Restaurant
            base.Delete(id);
        }


        #endregion

        #region Business Operations

        public JQueryDataTables<RestaurantSearch> Search(RestaurantSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);
            var ignoreMobile = string.IsNullOrEmpty(prms.Mobile);
            var ignorePhone = string.IsNullOrEmpty(prms.Phone);
            var ignoreCanReserveTables = string.IsNullOrEmpty(prms.CanReserveTables);
            var ignoreAllowSmoking = string.IsNullOrEmpty(prms.AllowSmoking);
            var ignoreType = prms.Type == null;

            var restaurants = (from restaurant in repository.Table
                               where restaurant.IsDeleted == false && restaurant.CreatorUserID == GlobalSettings.CurrentUserID
                                     && (ignoreName || restaurant.Name.Contains(prms.Name))
                                     && (ignoreMobile || restaurant.Name.Contains(prms.Mobile))
                                     && (ignorePhone || restaurant.Name.Contains(prms.Phone))
                                     && (ignoreCanReserveTables || restaurant.CanReserveTables == prms.CanReserveTables.Equals("1"))
                                     && (ignoreAllowSmoking || restaurant.AllowSmoking == prms.AllowSmoking.Equals("1"))
                                     && (ignoreType || restaurant.Type == prms.Type.Value)
                               select new RestaurantSearch
                               {
                                   ID = restaurant.ID,
                                   Name = restaurant.Name,
                                   Type = ((Enums.RestaurantType)restaurant.Type).ToString(),
                                   Mobile = restaurant.Mobile,
                                   Phone = restaurant.Phone,
                                   NumberOfStaff = restaurant.NumberOfStaff,
                                   NumberOfTables = restaurant.NumberOfTables,
                                   TimeToDelivery = restaurant.TimeToDelivery
                               })
                               .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantSearch>
            {
                iTotalRecords = restaurants.Count(),
                iTotalDisplayRecords = restaurants.Count(),
                aaData = restaurants.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        public string GetName(int id)
        {
            return repository.Table
                             .Where(c => c.ID == id)
                             .Select(c => c.Name)
                             .FirstOrDefault();
        }

        public int GetNumberOfRestaurant(int userId)
        {
            return repository.Table
                             .Where(u => u.IsDeleted == false && u.CreatorUserID == userId)
                             .Select(u => u.ID)
                             .Count();
        }

        public List<RestaurantForAPI> GetResturantByCityID(int cityId, int count)
        {
            var Branchrepository = unitOfWork.Repository<tbl_RestaurantBranch>();

            var LstResturant = (from branch in Branchrepository.Table
                                join restaurant in repository.Table on branch.RestaurantID equals restaurant.ID
                                where branch.lkp_District.lkp_City.ID == cityId
                                where restaurant.IsDeleted == false
                                select new RestaurantForAPI
                                {
                                    ID = restaurant.ID,
                                    Name = restaurant.Name,
                                    Location = branch.lkp_District.Name + ", " + branch.lkp_District.lkp_City.Name,
                                    //TODO: Get Type Dynmaic and get Image from Attatchments
                                    Type = 1,
                                    ImagePath = ""
                                }).OrderBy(Ex => Ex.Name).Take(count).ToList();
            return LstResturant;
        }

        public List<RestaurantForAPI> GetResturantMenu(int RestaurantID)
        {
            var FoodRepository = unitOfWork.Repository<tbl_RestaurantMenu>();

            var LstResturant = (from restaurant in repository.Table
                                join foodType in FoodRepository.Table
                                on restaurant.ID equals foodType.RestaurantID
                                where restaurant.ID == RestaurantID
                                where restaurant.IsDeleted == false
                                select new RestaurantForAPI
                                {
                                    ID = foodType.ID,
                                    Name = foodType.Name
                                }).OrderBy(q => q.Name).ToList();
            return LstResturant;
        }

        public List<RestaurantForAPI> GetFilteredResturant(ResturantFilter filter)
        {
            var Branchrepository = unitOfWork.Repository<tbl_RestaurantBranch>();
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            var LstResturant = (from branch in Branchrepository.Table
                                join restaurant in repository.Table on branch.RestaurantID equals restaurant.ID
                                where branch.lkp_District.lkp_City.ID == filter.CityID
                                where restaurant.IsDeleted == false
                                select new RestaurantForAPI
                                {
                                    ID = restaurant.ID,
                                    Name = restaurant.Name,
                                    Location = branch.lkp_District.Name + ", " + branch.lkp_District.lkp_City.Name,
                                    OrderCount = restaurant.tbl_RestaurantOrder.Count,
                                    Type = restaurant.Type,
                                    LstFoodType = restaurant.tbl_RestaurantFood.Select(ex => ex.lkp_FoodType.Name).ToList(),

                                    CreationDate = restaurant.CreationDate
                                }).AsQueryable();

            //** if Type not equal 1 or 0 system will retrive all entities
            // Type = Resutrant
            if (filter.Type == 1)
                LstResturant = LstResturant.Where(ex => ex.Type == 1).AsQueryable();

            else if (filter.Type == 2) //Coffe
                LstResturant = LstResturant.Where(ex => ex.Type == 2).AsQueryable();

            else if (filter.Type == 3) //Breakfast 
                LstResturant = LstResturant.Where(ex => ex.LstFoodType.Contains("Breakfast")).AsQueryable();

            else if (filter.Type == 4) //Lunch 
                LstResturant = LstResturant.Where(ex => ex.LstFoodType.Contains("Lunch")).AsQueryable();

            else if (filter.Type == 5) //Dinner 
                LstResturant = LstResturant.Where(ex => ex.LstFoodType.Contains("Dinner")).AsQueryable();

            else if (filter.Type == 6) //Night 
                LstResturant = LstResturant.Where(ex => ex.LstFoodType.Contains("Night")).AsQueryable();

            //Most Ordered
            if (filter.FilterType == 1)
                LstResturant = LstResturant.OrderByDescending(Ex => Ex.OrderCount).AsQueryable();
            else if (filter.FilterType == 2) //Recently Added
                LstResturant = LstResturant.OrderByDescending(Ex => Ex.CreationDate).AsQueryable();

            List<RestaurantForAPI> result = LstResturant.Take(filter.Count).ToList();

            //TODO: Need to refactor to get better performance
            foreach (var restaurant in result)
            {
                restaurant.ImagePath = sourceImageService.GetDefaultImageURL(restaurant.ID, Enums.SourceType.Restaurant);
            }
            return result;
        }

        public List<RestaurantFoodForAPI> GetResturants()
        {
            var FoodTyperepository = unitOfWork.Repository<lkp_FoodType>();
            var RestaurantFoodrepository = unitOfWork.Repository<tbl_RestaurantFood>();
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            var LstResturant = (from foodType in FoodTyperepository.Table
                                where foodType.IsDeleted == false
                                select new RestaurantFoodForAPI
                                {
                                    ID = foodType.ID,
                                    FoodType = foodType.Name,
                                    Restaurants = (from restaurant in repository.Table
                                                   where restaurant.IsDeleted == false && restaurant.Type == foodType.ID
                                                   select new RestaurantsForAPI
                                                   {
                                                       ID = restaurant.ID,
                                                       Name = restaurant.Name
                                                   }).ToList()
                                }).AsQueryable();

            List<RestaurantFoodForAPI> result = LstResturant.ToList();

            //TODO: Need to refactor to get better performance
            foreach (var restaurant in result)
            {
                restaurant.ImagePath = sourceImageService.GetDefaultImageURL(restaurant.ID, Enums.SourceType.Restaurant);
            }
            return result;
        }

        public List<RestaurantPromotionForAPI> GetPromotions(int RestaurantID = -1)
        {
            var Promorepository = unitOfWork.Repository<tbl_RestaurantPromotion>();
            var sourceImageService = unitOfWork.Service<SourceImageService>();

            var LstResturant = (from promo in Promorepository.Table
                                join restaurant in repository.Table on promo.RestaurantID equals restaurant.ID
                                where restaurant.IsDeleted == false
                                select new RestaurantPromotionForAPI
                                {
                                    PromotionID = promo.ID,
                                    PromotionName = promo.Name,
                                    Percentage = promo.Percentage,
                                    Restaurant = restaurant.Name,
                                    RestaurantID = restaurant.ID
                                }).AsQueryable();



            List<RestaurantPromotionForAPI> result = LstResturant.ToList();

            //TODO: Need to refactor to get better performance
            foreach (var restaurant in result)
            {
                restaurant.ImagePath = sourceImageService.GetDefaultImageURL(
                    restaurant.PromotionID,
                    Enums.SourceType.Restaurant);
            }
            return result;
        }

        public bool IsValidNumberOfRestaurant(int userId, out int maxNumberOfRestaurant)
        {
            var userService = unitOfWork.Service<UserService>();

            // Get number of restaurants allowed for this user "restaurant admin"
            maxNumberOfRestaurant = userService.GetNumberOfRestaurant(userId);

            // Get current number of restaurants related to this user "restaurant admin"
            var currentNumberOfRestaurant = GetNumberOfRestaurant(userId);

            return (currentNumberOfRestaurant < maxNumberOfRestaurant);
        }

        #endregion
    }
}