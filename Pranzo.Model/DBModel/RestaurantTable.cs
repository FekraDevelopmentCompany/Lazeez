using Lazeez.Model.Infrastructure;
using Lazeez.Resource.Shared;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class RestaurantTable : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantID { get; set; }
      
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int GuestCount { get; set; }
        
        public string Description { get; set; }
    }
}
