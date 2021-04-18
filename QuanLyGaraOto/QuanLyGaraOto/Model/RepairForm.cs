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
    
    public partial class RepairForm : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RepairForm()
        {
            this.RepairInfoes = new HashSet<RepairInfo>();
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; OnPropertyChanged(); }
        }
        private int _IdCarReception;
        public int IdCarReception
        {
            get => _IdCarReception;
            set { _IdCarReception = value; OnPropertyChanged(); }
        }
        private System.DateTime _RepairDate;
        public System.DateTime RepairDate
        {
            get => _RepairDate;
            set { _RepairDate = value; OnPropertyChanged(); }
        }


        public virtual CarReception CarReception { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RepairInfo> RepairInfoes { get; set; }
    }
}
