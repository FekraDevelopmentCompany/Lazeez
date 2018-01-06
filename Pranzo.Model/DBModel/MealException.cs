using Lazeez.Model.Infrastructure;
using Lazeez.Resource.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class MealException : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [Remote("IsMealExceptionUnique", "RestaurantMeal", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_AlreadyExists")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
