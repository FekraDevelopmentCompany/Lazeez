//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lazeez.Entities.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_RestaurantMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RestaurantMenu()
        {
            this.tbl_RestaurantMeal = new HashSet<tbl_RestaurantMeal>();
        }
    
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public int FoodTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual lkp_FoodType lkp_FoodType { get; set; }
        public virtual tbl_Restaurant tbl_Restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantMeal> tbl_RestaurantMeal { get; set; }
    }
}
