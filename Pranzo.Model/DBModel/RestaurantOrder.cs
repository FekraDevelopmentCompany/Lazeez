using System;
using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Lazeez.Resource.Shared;

namespace Lazeez.Model.DBModel
{
    public class RestaurantOrder : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int RestaurantBranchID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [RegularExpression(@"((?=.*[0-9])\d{0,16}(\.\d{1,2})?)", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_InvalidFormat")]
        public decimal TotalCost { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [RegularExpression(@"((?=.*[0-9])\d{0,16}(\.\d{1,2})?)", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_InvalidFormat")]
        public decimal DeliveryCost { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [RegularExpression(@"((?=.*[0-9])\d{0,16}(\.\d{1,2})?)", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_InvalidFormat")]
        public decimal Tax { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public DateTime OrderTime { get; set; }

        public DateTime? DeliveryTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public int Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        public string PaymentType { get; set; }
    }
}