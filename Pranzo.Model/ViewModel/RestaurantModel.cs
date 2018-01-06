﻿using System;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Lazeez.Model.ViewModel
{
    public class RestaurantModel
    {
        public string Header { get; set; }

        private Restaurant _restaurantVm;
        public Restaurant RestaurantVM
        {
            get { return _restaurantVm; }
            set
            {
                _restaurantVm = value;
                Header = _restaurantVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }
    }

    public class RestaurantSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public int NumberOfTables { get; set; }
        public int NumberOfStaff { get; set; }
        public string TimeToDelivery { get; set; }
    }

    public class RestaurantSearchParams : JQueryDataTablesModel
    {
        public string Name { get; set; }
        public int? Type { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string CanReserveTables { get; set; }
        public string AllowSmoking { get; set; }
    }

    public class RestaurantForAPI
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
        public int OrderCount { get; set; }
        public DateTime CreationDate { get; set; }

        public List<string> LstFoodType { get; set; }
    }

    public class ResturantFilter
    {
        [Required]
        public int Type { get; set; }
        [Required]
        public int FilterType { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public int Count { get; set; }

        public string Keyword { get; set; }
    }

}