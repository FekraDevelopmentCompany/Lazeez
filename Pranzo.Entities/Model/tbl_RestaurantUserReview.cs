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
    
    public partial class tbl_RestaurantUserReview
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public short Rate { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual tbl_Restaurant tbl_Restaurant { get; set; }
        public virtual tbl_User tbl_User { get; set; }
    }
}
