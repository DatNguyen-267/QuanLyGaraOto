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
    
    public partial class REPAIR :BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REPAIR()
        {
            this.REPAIR_DETAIL = new HashSet<REPAIR_DETAIL>();
        }

        public int Repair_Id { get; set; }
        private int _IdReception { get; set; }
        public int IdReception { get => _IdReception; set { _IdReception = value; OnPropertyChanged(); } }
        private System.DateTime _RepairDate { get; set; }
        public System.DateTime RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        private int _Repair_TotalMoney { get; set; }
        public int Repair_TotalMoney { get => _Repair_TotalMoney; set { _Repair_TotalMoney = value; OnPropertyChanged(); } }
        private RECEPTION _RECEPTION { get; set; }
        public virtual RECEPTION RECEPTION { get => _RECEPTION; set { _RECEPTION = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REPAIR_DETAIL> REPAIR_DETAIL { get; set; }
    }
}
