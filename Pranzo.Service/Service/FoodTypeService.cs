﻿using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class FoodTypeService : BaseService<lkp_FoodType, FoodType>
    {
        public FoodTypeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<lkp_FoodType>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // TODO Custom Code
            // Delete RestaurantFood
            var restaurantFood = unitOfWork.Service<RestaurantFoodService>();
            restaurantFood.Delete(r => r.FoodTypeID == id);

            // Delete UserInterset
            var userInterestService = unitOfWork.Service<UserInterestService>();
            userInterestService.Delete(r => r.FoodTypeID == id);


            base.Delete(id);
        }

        #endregion

        #region Business Operations

        /// <summary>
        /// Search in lkp_FoodType Name
        /// </summary>
        /// <param name="prms"></param>
        /// <returns></returns>
        public JQueryDataTables<MasterDataSearch> Search(MasterDataSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);

            var foodTypes = (from foodType in repository.Table
                             where foodType.IsDeleted == false
                                && (ignoreName || foodType.Name.Contains(prms.Name))
                             select new MasterDataSearch
                             {
                                 ID = foodType.ID,
                                 Name = foodType.Name,
                                 Description = string.Empty
                             })
                            .OrderBy(prms.OrderBy);

            return new JQueryDataTables<MasterDataSearch>
            {
                iTotalRecords = foodTypes.Count(),
                iTotalDisplayRecords = foodTypes.Count(),
                aaData = foodTypes.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}