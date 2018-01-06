using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class DistrictService : BaseService<lkp_District, District>
    {
        public DistrictService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<lkp_District>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations


        /// <summary>
        /// Search in Lkp_District By Name
        /// </summary>
        /// <param name="prms"></param>
        /// <returns></returns>
        public JQueryDataTables<MasterDataSearch> Search(MasterDataSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);

            // Get Repository of City
            var cityRepository = unitOfWork.Repository<lkp_City>();

            var districts = (from district in repository.Table
                             join city in cityRepository.Table on district.CityID equals city.ID
                             where city.IsDeleted == false
                                && (ignoreName || district.Name.Contains(prms.Name))
                             select new MasterDataSearch
                             {
                                 ID = district.ID,
                                 Name = district.Name,
                                 Description = city.Name
                             })
                          .OrderBy(prms.OrderBy);

            return new JQueryDataTables<MasterDataSearch>
            {
                iTotalRecords = districts.Count(),
                iTotalDisplayRecords = districts.Count(),
                aaData = districts.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        #endregion
    }
}