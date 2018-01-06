using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.ViewModel
{
    // Model In Case Add edit Form 
    public class RestaurantTableModel
    {
        // Headre Edit Or Add Case 
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantTable _restaurantTableVm;
        public RestaurantTable RestaurantTableVM
        {
            get { return _restaurantTableVm; }
            set
            {
                _restaurantTableVm = value;
                // Header = edit in case id > 0 and  Add in case id <0
                Header = _restaurantTableVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }
    }

    // Model to fill DataTable Grid View 
    public class RestaurantTableSearch
    {
        public int ID { get; set; }
        public int GuestCount { get; set; }
        public string Description { get; set; }
    }

    // Search Criteria in Table Form 
    public class RestaurantTableSearchParams : JQueryDataTablesModel
    {
        public int? RestaurantID { get; set; }
        public int? GuestCountFrom { get; set; }
        public int? GuestCountTo { get; set; }
     
    }
}
