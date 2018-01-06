using System.Linq;
using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;

namespace Lazeez.Service.Service
{
    public class DeviceTokenService : BaseService<tbl_UserDeviceToken, UserDeviceToken>
    {
        public DeviceTokenService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_UserDeviceToken>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations

       
        #endregion
    }
}
