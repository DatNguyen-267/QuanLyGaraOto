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
    
    public partial class IMPORT_GOODS_DETAIL :BaseViewModel
    {
        private int _IdImportGood { get; set; }

        public int IdImportGood { get => _IdImportGood; set { _IdImportGood = value; OnPropertyChanged(); } }
        private int _IdSupplies { get; set; }

        public int IdSupplies { get => _IdSupplies; set { _IdSupplies = value; OnPropertyChanged(); } }
        private int _Amount { get; set; }
        public int Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        private int _Price { get; set; }
        public int Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        public virtual IMPORT_GOODS IMPORT_GOODS { get; set; }
        public virtual SUPPLIES SUPPLIES { get; set; }
    }
}
