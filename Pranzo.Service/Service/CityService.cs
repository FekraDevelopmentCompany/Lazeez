﻿using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class CityService : BaseService<lkp_City, City>
    {
        public CityService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<lkp_City>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // TODO Custom Code
            // Delete District
            var distrcitService = unitOfWork.Service<DistrictService>();
            distrcitService.Delete(r => r.CityID == id);

            base.Delete(id);
        }

        #endregion

        #region Business Operations

        public string GetName(int id)
        {
            return repository.Table
                             .Where(c => c.ID == id)
                             .Select(c => c.Name)
                             .FirstOrDefault();
        }

        public List<City> GetLocation()
        {
            return repository.Table.Select(ex => new City { Name = ex.lkp_Country.Name + ", " + ex.Name, ID = ex.ID }).ToList();
        }

        /// <summary>
        /// Search in lkp_City By Name
        /// </summary>
        /// <param name="prms"></param>
        /// <returns></returns>
        public JQueryDataTables<MasterDataSearch> Search(MasterDataSearchParams prms)
        {
            // Parameters
            bool ignoreName = string.IsNullOrEmpty(prms.Name);

            // Get Repository of Country
            var countryRepository = unitOfWork.Repository<lkp_Country>();

            var cities = (from city in repository.Table
                          join country in countryRepository.Table on city.CountryID equals country.ID
                          where city.IsDeleted == false
                             && (ignoreName || city.Name.Contains(prms.Name))
                          select new MasterDataSearch
                          {
                              ID = city.ID,
                              Name = city.Name,
                              Description = country.Name
                          })
                          .OrderBy(prms.OrderBy);

            return new JQueryDataTables<MasterDataSearch>
            {
                iTotalRecords = cities.Count(),
                iTotalDisplayRecords = cities.Count(),
                aaData = cities.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}