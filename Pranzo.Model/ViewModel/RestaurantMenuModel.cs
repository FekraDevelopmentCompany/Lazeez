using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.ViewModel
{
    public class RestaurantMenuModel
    {
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantMenu _restaurantMenuVm;
        public RestaurantMenu RestaurantMenuVM
        {
            get { return _restaurantMenuVm; }
            set
            {
                _restaurantMenuVm = value;
                Header = _restaurantMenuVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }

        public SelectList ListFoodTypes { get; set; }
    }

    public class RestaurantMenuSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RestaurantMenuSearchParams : JQueryDataTablesModel
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
    }
}
