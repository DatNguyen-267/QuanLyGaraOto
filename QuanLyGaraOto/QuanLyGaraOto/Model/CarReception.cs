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
    
    public partial class CarReception
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarReception()
        {
            this.Receipts = new HashSet<Receipt>();
            this.RepairForms = new HashSet<RepairForm>();
        }
    
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public string LicensePlate { get; set; }
        public int IdBrand { get; set; }
        public System.DateTime ReceptionDate { get; set; }
        public int IdStatus { get; set; }
    
        public virtual CarBrand CarBrand { get; set; }
        public virtual CarStatu CarStatu { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt> Receipts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RepairForm> RepairForms { get; set; }
    }
}
