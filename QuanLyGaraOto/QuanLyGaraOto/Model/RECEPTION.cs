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
    using QuanLyGaraOto.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class RECEPTION : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RECEPTION()
        {
            this.RECEIPTs = new HashSet<RECEIPT>();
            this.REPAIRs = new HashSet<REPAIR>();
        }

        private int _Reception_Id { get; set; }
        public int Reception_Id { get => _Reception_Id; set { _Reception_Id = value; OnPropertyChanged(); } }
        private string _LicensePlate { get; set; }
        public string LicensePlate { get => _LicensePlate; set { _LicensePlate = value; OnPropertyChanged(); } }
        private System.DateTime _ReceptionDate { get; set; }
        public System.DateTime ReceptionDate { get => _ReceptionDate; set { _ReceptionDate = value; OnPropertyChanged(); } }
        private int _Debt { get; set; }
        public int Debt { get => _Debt; set { _Debt = value; OnPropertyChanged(); } }
        private int _IdCarBrand { get; set; }
        public int IdCarBrand { get => _IdCarBrand; set { _IdCarBrand = value; OnPropertyChanged(); } }
        private int _IdCustomer { get; set; }
        public int IdCustomer { get => _IdCustomer; set { _IdCustomer = value; OnPropertyChanged(); } }
        private CAR_BRAND _CAR_BRAND { get; set; }
        public virtual CAR_BRAND CAR_BRAND { get => _CAR_BRAND; set { _CAR_BRAND = value; OnPropertyChanged(); } }
        private CUSTOMER _CUSTOMER { get; set; }
        public virtual CUSTOMER CUSTOMER { get => _CUSTOMER; set { _CUSTOMER = value; OnPropertyChanged(); } }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECEIPT> RECEIPTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REPAIR> REPAIRs { get; set; }
    }
}
