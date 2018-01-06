using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class CountryService : BaseService<lkp_Country, Country>
    {
        public CountryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<lkp_Country>();
        }

        #region BaseService Operations

        public override void Delete(int id)
        {
            // TODO Custom Code
            // Delete City
            var cityService = unitOfWork.Service<CityService>();
            cityService.Delete(r => r.CountryID == id);

            base.Delete(id);
        }

        #endregion

        #region Business Operations


        /// <summary>
        /// Search in Lkp_Country By Name
        /// </summary>
        /// <param name="prms"></param>
        /// <returns></returns>
        public JQueryDataTables<MasterDataSearch> Search(MasterDataSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);

            var countries = (from country in repository.Table
                             where country.IsDeleted == false
                                && (ignoreName || country.Name.Contains(prms.Name))
                             select new MasterDataSearch
                             {
                                 ID = country.ID,
                                 Name = country.Name,
                                 Description = string.Empty
                             })
                          .OrderBy(prms.OrderBy);

            return new JQueryDataTables<MasterDataSearch>
            {
                iTotalRecords = countries.Count(),
                iTotalDisplayRecords = countries.Count(),
                aaData = countries.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}