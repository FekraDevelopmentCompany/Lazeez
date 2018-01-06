using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.DBModel
{
    public class RestaurantMenu : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int FoodTypeID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [Remote("IsUnique", "RestaurantMenu", AdditionalFields = "ID , RestaurantID", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_AlreadyExists")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}