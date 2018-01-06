using System.ComponentModel.DataAnnotations;
using Lazeez.Model.Infrastructure;
using Lazeez.Resource.Shared;
//using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class User : BaseEntity
    {
        public int ID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        //public int UserTypeID { get; set; }

        //public int? UserParentID { get; set; }
        //public int? RestaurantBranchID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [System.Web.Mvc.Remote("IsUnique", "User", AdditionalFields = "ID ", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_AlreadyExists")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(250, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        //public string ConfirmPassword { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        public string FacebookAccount { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        public string InstgramAccount { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [EmailAddress(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_Email")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public int CityId { get; set; }
        //public string DeviceToken { get; set; }
    }
}