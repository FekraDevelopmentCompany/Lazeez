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
    
    public partial class tbl_RestaurantPromotionMeal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RestaurantPromotionMeal()
        {
            this.tbl_RestaurantOrderMeal = new HashSet<tbl_RestaurantOrderMeal>();
        }
    
        public int ID { get; set; }
        public int RestaurantPromotionID { get; set; }
        public int RestaurantMealID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual tbl_RestaurantMeal tbl_RestaurantMeal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantOrderMeal> tbl_RestaurantOrderMeal { get; set; }
        public virtual tbl_RestaurantPromotion tbl_RestaurantPromotion { get; set; }
    }
}