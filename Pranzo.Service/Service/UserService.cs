using Lazeez.Entities.Model;
using Lazeez.Repository.UnitOfWork;
using Lazeez.Service.Infrastructure;
using Lazeez.Model.DBModel;
using Lazeez.Helper;
using System.Linq;
using Lazeez.Model.ViewModel;

namespace Lazeez.Service.Service
{
    public class UserService : BaseService<tbl_User, User>
    {
        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<tbl_User>();
        }

        #region BaseService Operations


        #endregion

        #region Business Operations

        public JQueryDataTables<UserSearch> Search(UserSearchParams prms)
        {
            // Parameters
            var ignoreName = string.IsNullOrEmpty(prms.Name);
            var ignoreUserType = !prms.UserType.HasValue;
            var ignorePhone = string.IsNullOrEmpty(prms.Phone);

            var userRepositry = unitOfWork.Repository<lkp_UserType>();
            var branchRepositry = unitOfWork.Repository<tbl_RestaurantBranch>();

            var users = (from user in repository.Table
                         join userType in userRepositry.Table on user.UserTypeID equals userType.ID
                         join restaurantBranch in branchRepositry.Table on user.RestaurantBranchID equals restaurantBranch.ID into branchs
                         from restaurantBranch in branchs.DefaultIfEmpty()
                         where user.IsDeleted == false && user.CreatorUserID == GlobalSettings.CurrentUserID
                              && (ignoreName || user.Name.Contains(prms.Name))
                              && (ignorePhone || user.Phone.Contains(prms.Phone))
                              && (ignoreUserType || user.UserTypeID == prms.UserType.Value)
                         select new UserSearch
                         {
                             ID = user.ID,
                             Name = user.Name,
                             UserType = userType.Name,
                             NumberOfRestaurant = user.NumberOfRestaurant,
                             BranchName = restaurantBranch.Name,
                             Phone = user.Phone,
                             Email = user.Email
                         })
                          .OrderBy(prms.OrderBy);

            return new JQueryDataTables<UserSearch>
            {
                iTotalRecords = users.Count(),
                iTotalDisplayRecords = users.Count(),
                aaData = users.Skip(prms.iDisplayStart).Take(prms.iDisplayLength).ToList()
            };
        }

        public bool IsValidUser(string name, string password)
        {
            password = StringCipher.Hash(password);
            var user = repository.Table
                             .Where(u => u.IsDeleted == false && u.Name.ToLower() == name.ToLower() && u.Password == password && u.UserTypeID != (int)Enums.UserType.Normal)
                             .Select(u => new User
                             {
                                 ID = u.ID,
                                 Name = u.Name,
                                 //UserTypeID = u.UserTypeID,
                                 //RestaurantBranchID = u.RestaurantBranchID
                             })
                            .FirstOrDefault();

            if (user == null) return false;

            GlobalSettings.CurrentUserID = user.ID;
            GlobalSettings.CurrentUserName = user.Name;
            //GlobalSettings.CurrentUserTypeID = (Enums.UserType)user.UserTypeID;
            //GlobalSettings.RestaurantBranchID = user.RestaurantBranchID;

            return true;
        }

        public int GetNumberOfRestaurant(int id)
        {
            return repository.Table
                             .Where(u => u.IsDeleted == false && u.ID == id)
                             .Select(u => u.NumberOfRestaurant)
                             .FirstOrDefault();
        }

        public bool SignUp(User model, out tbl_User user, out string message)
        {
            var isSaved = false;
            user = null;
            message = "";

            if (base.Exists(r => r.ID != model.ID && r.Name == model.Name))
            {
                message = "User name should be uniqe.";
                return isSaved;
            }

            if (base.Exists(r => r.ID != model.ID && r.Email == model.Email))
            {
                message = "Email should be uniqe.";
                return isSaved;
            }

            //Normal User Type
            //model.UserTypeID = (short)Enums.UserType.Normal;
            Mapper.Instance.CreateMap<User, tbl_User>();
            tbl_User table = Mapper.Instance.Map<User, tbl_User>(model);

            //Make Password Hashed
            table.Password = StringCipher.Hash(model.Password);

            //if (!string.IsNullOrEmpty(model.DeviceToken))
            //    table.tbl_UserDeviceToken.Add(new tbl_UserDeviceToken { tbl_User = table, DeviceToken = model.DeviceToken });

            repository.Insert(table);
            user = table;
            isSaved = true;
            return isSaved;
        }
        public bool Update(User model, out tbl_User user, out string message)
        {
            var isSaved = false;
            user = null;
            message = "";

            if (base.Exists(r => r.ID != model.ID && r.Name == model.Name))
            {
                message = "User name should be uniqe.";
                return isSaved;
            }

            if (base.Exists(r => r.ID != model.ID && r.Email == model.Email))
            {
                message = "Email should be uniqe.";
                return isSaved;
            }

            //Normal User Type
            //model.UserTypeID = (short)Enums.UserType.Normal;
            Mapper.Instance.CreateMap<User, tbl_User>();
            tbl_User table = Mapper.Instance.Map<User, tbl_User>(model);

            //Make Password Hashed
            table.Password = StringCipher.Hash(model.Password);

            //if (!string.IsNullOrEmpty(model.DeviceToken))
            //    table.tbl_UserDeviceToken.Add(new tbl_UserDeviceToken { tbl_User = table, DeviceToken = model.DeviceToken });

            repository.Update(table);
            user = table;
            isSaved = true;
            return isSaved;
        }
        #endregion
    }
}