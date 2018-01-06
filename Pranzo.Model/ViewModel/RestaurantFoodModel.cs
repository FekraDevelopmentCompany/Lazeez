using System.Collections.Generic;
using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.ViewModel
{
    public class RestaurantFoodModel
    {
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantFood _restaurantFoodVm;
        public RestaurantFood RestaurantFoodVM
        {
            get { return _restaurantFoodVm; }
            set
            {
                _restaurantFoodVm = value;
                Header = _restaurantFoodVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }

        public SelectList ListFoodTypes { get; set; }
    }

    public class RestaurantFoodSearch
    {
        public int ID { get; set; }
        public string FoodType { get; set; }
    }

    public class RestaurantFoodForAPI
    {
        public int ID { get; set; }
        public string FoodType { get; set; }
        public string ImagePath { get; set; }
        public List<RestaurantsForAPI> Restaurants { get; set; }
    }

    public class RestaurantsForAPI
    {
        public int ID { get; set; }
        public string Name { get; set; } 
    }


    public class RestaurantFoodSearchParams : JQueryDataTablesModel
    {
        public int RestaurantID { get; set; }
        public short? FoodTypeID { get; set; }
    }
}
