using Lazeez.Model.Infrastructure;
using Lazeez.Resource.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class RestaurantPromotionMeal : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantMealID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantPromotionID { get; set; }
    }
}
