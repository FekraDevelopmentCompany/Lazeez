﻿using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Lazeez.Resource.Shared;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class FoodType : BaseEntity
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_MaximumLength")]
        [Remote("IsFoodTypeUnique", "RestaurantFood", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_AlreadyExists")]
        public string Name { get; set; }
    }
}