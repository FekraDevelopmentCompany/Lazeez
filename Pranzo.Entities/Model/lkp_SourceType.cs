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
    
    public partial class lkp_SourceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lkp_SourceType()
        {
            this.lkp_SourceImage = new HashSet<lkp_SourceImage>();
        }
    
        public short ID { get; set; }
        public string Name { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int CreatorUserID { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedUserID { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lkp_SourceImage> lkp_SourceImage { get; set; }
    }
}
