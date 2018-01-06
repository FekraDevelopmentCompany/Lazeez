using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.DBModel
{
    public class RestaurantOrderMeal : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantOrderID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantMealID { get; set; }

        public int? RestaurantPromotionMealID { get; set; }
    }
}