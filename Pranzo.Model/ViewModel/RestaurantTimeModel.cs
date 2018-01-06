using System;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.ViewModel
{
    public class RestaurantTimeModel
    {
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantTime _restaurantTimeVm;
        public RestaurantTime RestaurantTimeVM
        {
            get { return _restaurantTimeVm; }
            set
            {
                _restaurantTimeVm = value;
                Header = _restaurantTimeVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }
    }

    public class RestaurantTimeSearch
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }

    public class RestaurantTimeSearchParams : JQueryDataTablesModel
    {
        public int RestaurantID { get; set; }
        public int? Day { get; set; }
    }
}