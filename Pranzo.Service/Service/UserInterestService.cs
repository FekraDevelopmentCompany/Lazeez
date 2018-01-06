using Lazeez.Entities.Model;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Model.ViewModel;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Lazeez.Service.Service
{
    public class UserInterestService : BaseService<tbl_UserInterest, UserInterest>
    {
        public UserInterestService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_UserInterest>();
        }

        #region BaseService Operations

        #endregion

        #region Business Operations


        #endregion
    }
}
