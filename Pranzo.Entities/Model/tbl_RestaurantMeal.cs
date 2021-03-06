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
    
    public partial class tbl_RestaurantMeal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RestaurantMeal()
        {
            this.tbl_RestaurantMealException = new HashSet<tbl_RestaurantMealException>();
            this.tbl_RestaurantOrderMeal = new HashSet<tbl_RestaurantOrderMeal>();
            this.tbl_RestaurantPromotionMeal = new HashSet<tbl_RestaurantPromotionMeal>();
        }
    
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public int RestaurantMenuID { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public Nullable<int> Calories { get; set; }
        public decimal Cost { get; set; }
        public int NumberOfPersons { get; set; }
        public string Temperature { get; set; }
        public string TimeOfMeal { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual tbl_Restaurant tbl_Restaurant { get; set; }
        public virtual tbl_RestaurantMenu tbl_RestaurantMenu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantMealException> tbl_RestaurantMealException { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantOrderMeal> tbl_RestaurantOrderMeal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantPromotionMeal> tbl_RestaurantPromotionMeal { get; set; }
    }
}
