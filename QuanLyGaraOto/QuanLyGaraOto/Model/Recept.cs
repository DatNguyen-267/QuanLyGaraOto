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
    
    public partial class Recept : BaseViewModel
    {
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
        private int _IdGaraInfo;
        public int IdGaraInfo
        {
            get => _IdGaraInfo;
            set { _IdGaraInfo = value; OnPropertyChanged(); }
        }
        private System.DateTime _ReceptDate;
        public System.DateTime ReceptDate
        {
            get => _ReceptDate;
            set { _ReceptDate = value; OnPropertyChanged(); }
        }
        private int _TotalMoney;
        public int TotalMoney
        {
            get => _TotalMoney;
            set { _TotalMoney = value; OnPropertyChanged(); }
        }

        public virtual CarReception CarReception { get; set; }
        public virtual GaraInfo GaraInfo { get; set; }
    }
}
