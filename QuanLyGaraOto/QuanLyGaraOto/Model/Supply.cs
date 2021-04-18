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
    
    public partial class Supply : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supply()
        {
            this.RepairInfoes = new HashSet<RepairInfo>();
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; OnPropertyChanged(); }
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; OnPropertyChanged(); }
        }
        private Nullable<int> _Price;
        public Nullable<int> Price
        {
            get => _Price;  
            set { _Price = value; OnPropertyChanged(); }
        }
        private Nullable<int> _Amount;
        public Nullable<int> Amount
        {
            get => _Amount;
            set { _Amount = value; OnPropertyChanged(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private ICollection<RepairInfo> _RepairInfoes;
        public virtual ICollection<RepairInfo> RepairInfoes
        {
            get => _RepairInfoes;
            set { _RepairInfoes = value; OnPropertyChanged(); }
        }
    }
}
