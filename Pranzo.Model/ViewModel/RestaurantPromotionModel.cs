﻿using System.Web.Mvc;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;
using System;
using System.Collections.Generic;

namespace Lazeez.Model.ViewModel
{
    // Model In Case Add edit Form 
    public class RestaurantPromotionModel
    {
        // Headre Edit Or Add Case 
        public string Header { get; set; }
        public string RestaurantName { get; set; }

        private RestaurantPromotion _restaurantPromotionVm;
        public RestaurantPromotion RestaurantPromotionVM
        {
            get { return _restaurantPromotionVm; }
            set
            {
                _restaurantPromotionVm = value;
                // Header = edit in case id > 0 and  Add in case id <0
                Header = _restaurantPromotionVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }

        public List<int> SelectedPromotionMeals { get; set; }
        public MultiSelectList ListPromotionMeals { get; set; }

    }

    // Model to fill DataTable Grid View 
    public class RestaurantPromotionSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ForAllUsers { get; set; }
    }

    // Search Criteria in RestaurantPromotion Form 
    public class RestaurantPromotionSearchParams : JQueryDataTablesModel
    {
        public int RestaurantID { get; set; }
        public string Name { get; set; }
        public DateTime? PromotionDateFrom { get; set; }
        public DateTime? PromotionDateTo { get; set; }
    }


    public class RestaurantPromotionForAPI
    {
        public int PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string ImagePath { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ForAllUsers { get; set; }
        public string Restaurant { get; set; }
        public int RestaurantID { get; set; }
    }

}
