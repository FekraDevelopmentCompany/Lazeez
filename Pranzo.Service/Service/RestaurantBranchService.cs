﻿using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;
using Lazeez.Model.ViewModel;

namespace Lazeez.Service.Service
{
    public class RestaurantBranchService : BaseService<tbl_RestaurantBranch, RestaurantBranch>
    {
        public RestaurantBranchService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_RestaurantBranch>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations

        public JQueryDataTables<RestaurantBranchSearch> Search(RestaurantBranchSearchParams prms)
        {
            // Parameters
            bool ignoreName = string.IsNullOrEmpty(prms.Name);
            bool ignoreDistrict = !prms.DistrictID.HasValue;
            bool ignoreAddress = string.IsNullOrEmpty(prms.Address);

            // Get Repository of District
            var districtRepositry = unitOfWork.Repository<lkp_District>();

            var restaurantBranchs = (from restBranch in repository.Table
                                     join district in districtRepositry.Table on restBranch.DistrictID equals district.ID
                                     where restBranch.IsDeleted == false
                                        && restBranch.RestaurantID == prms.RestaurantID
                                        && (ignoreName || restBranch.Name.Contains(prms.Name))
                                        && (ignoreDistrict || restBranch.DistrictID == prms.DistrictID)
                                        && (ignoreAddress || restBranch.Name.Contains(prms.Address))
                                     select new RestaurantBranchSearch
                                     {
                                         ID = restBranch.ID,
                                         Name = restBranch.Name,
                                         District = district.Name,
                                         Phone = restBranch.Phone,
                                         Address = restBranch.Address,
                                         IsMain = restBranch.IsMain,
                                     })
                                    .OrderBy(prms.OrderBy);

            return new JQueryDataTables<RestaurantBranchSearch>
            {
                iTotalRecords = restaurantBranchs.Count(),
                iTotalDisplayRecords = restaurantBranchs.Count(),
                aaData = restaurantBranchs.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        public string GetName(int id)
        {
            return repository.Table
                             .Where(c => c.ID == id)
                             .Select(c => c.Name)
                             .FirstOrDefault();
        }

        public int GetRestaurantID(int id)
        {
            return repository.Table
                             .Where(c => c.ID == id)
                             .Select(c => c.RestaurantID)
                             .FirstOrDefault();
        }

        #endregion
    }
}