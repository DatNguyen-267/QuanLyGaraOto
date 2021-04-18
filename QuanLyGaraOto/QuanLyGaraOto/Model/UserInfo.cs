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
    
    public partial class UserInfo : BaseViewModel
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; OnPropertyChanged(); }
        }
        private int _IdUser;
        public int IdUser
        {
            get => _IdUser;
            set { _IdUser = value; OnPropertyChanged(); }
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; OnPropertyChanged(); }
        }
        private string _Address;
        public string Address
        {
            get => _Address;
            set { _Address = value; OnPropertyChanged(); }
        }
        private Nullable<System.DateTime> _BirthDate;
        public Nullable<System.DateTime> BirthDate
        {
            get => _BirthDate;
            set { _BirthDate = value; OnPropertyChanged(); }
        }
        private string _Telephone;
        public string Telephone
        {
            get => _Telephone;
            set { _Telephone = value; OnPropertyChanged(); }
        }
        private string _CMND;
        public string CMND
        {
            get => _CMND;
            set { _CMND = value; OnPropertyChanged(); }
        }

        private User _User;
        public virtual User User
        {
            get => _User;
            set { _User = value; OnPropertyChanged(); }
        }
    }
}