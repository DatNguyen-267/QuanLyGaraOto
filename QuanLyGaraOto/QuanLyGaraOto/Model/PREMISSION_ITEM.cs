//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyGaraOto.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PREMISSION_ITEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PREMISSION_ITEM()
        {
            this.ROLE_DETAIL = new HashSet<ROLE_DETAIL>();
        }
    
        public int PermissionItem_Id { get; set; }
        public string PermissionItem_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE_DETAIL> ROLE_DETAIL { get; set; }
    }
}
