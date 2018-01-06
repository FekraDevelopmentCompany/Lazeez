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
    
    public partial class tbl_RestaurantOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RestaurantOrder()
        {
            this.tbl_RestaurantOrderMeal = new HashSet<tbl_RestaurantOrderMeal>();
            this.tbl_UserOrder = new HashSet<tbl_UserOrder>();
        }
    
        public int ID { get; set; }
        public int RestaurantID { get; set; }
        public Nullable<int> RestaurantBranchID { get; set; }
        public decimal TotalCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal Tax { get; set; }
        public System.DateTime OrderTime { get; set; }
        public Nullable<System.DateTime> DeliveryTime { get; set; }
        public int Status { get; set; }
        public string PaymentType { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual tbl_Restaurant tbl_Restaurant { get; set; }
        public virtual tbl_RestaurantBranch tbl_RestaurantBranch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RestaurantOrderMeal> tbl_RestaurantOrderMeal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UserOrder> tbl_UserOrder { get; set; }
    }
}
