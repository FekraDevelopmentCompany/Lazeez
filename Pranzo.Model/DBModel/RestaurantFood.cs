using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Lazeez.Resource.Shared;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class RestaurantFood : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [Remote("IsUnique", "RestaurantFood", AdditionalFields = "ID , RestaurantID", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_AlreadyExists")]
        public int FoodTypeID { get; set; }
    }
}
