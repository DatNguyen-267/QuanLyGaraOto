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
    
    public partial class USER_INFO:BaseViewModel
    {
        public int UserInfo_Id { get; set; }
        public Nullable<int> IdUser { get; set; }

        private string _UserInfo_Name { get; set; }
        public string UserInfo_Name { get => _UserInfo_Name; set { _UserInfo_Name = value; OnPropertyChanged(); } }

        private string _UserInfo_Address { get; set; }
        public string UserInfo_Address { get => _UserInfo_Address; set { _UserInfo_Address = value; OnPropertyChanged(); } }

        private Nullable<System.DateTime> _UserInfo_BirthDate { get; set; }
        public Nullable<System.DateTime> UserInfo_BirthDate { get => _UserInfo_BirthDate; set { _UserInfo_BirthDate = value; OnPropertyChanged(); } }

        private string _UserInfo_Telephone { get; set; }
        public string UserInfo_Telephone { get => _UserInfo_Telephone; set { _UserInfo_Telephone = value; OnPropertyChanged(); } }

        private string _UserInfo_CMND { get; set; }
        public string UserInfo_CMND { get => _UserInfo_CMND; set { _UserInfo_CMND = value; OnPropertyChanged(); } }

        private USER _USER { get; set; }
        public virtual USER USER { get => _USER; set { _USER = value; OnPropertyChanged(); } }
    }
}
