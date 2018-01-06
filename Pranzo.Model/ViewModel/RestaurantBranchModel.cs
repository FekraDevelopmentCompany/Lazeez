﻿using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.ViewModel
{
    // Model In Case Add edit Form 
    public class RestaurantBranchModel
    {
        // Headre Edit Or Add Case 
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantBranch _restaurantBranchVm;
        public RestaurantBranch RestaurantBranchVM
        {
            get { return _restaurantBranchVm; }
            set
            {
                _restaurantBranchVm = value;
                // Header = edit in case id > 0 and  Add in case id <0
                Header = _restaurantBranchVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }

        // List of District
        public SelectList ListDistrict { get; set; }
    }

    // Model to fill DataTable Grid View 
    public class RestaurantBranchSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsMain { get; set; }
    }

    // Search Criteria in Branch Form 
    public class RestaurantBranchSearchParams : JQueryDataTablesModel
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public int? DistrictID { get; set; }
        public string Address { get; set; }
        //public bool? IsMain { get; set; }
    }
}
