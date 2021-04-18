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
    
    public partial class User : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserInfoes = new HashSet<UserInfo>();
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; OnPropertyChanged(); }
        }
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set { _UserName = value; OnPropertyChanged(); }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set { _Password = value; OnPropertyChanged(); }
        }
        private int _IdRole;
        public int IdRole
        {
            get => _IdRole;
            set { _IdRole = value; OnPropertyChanged(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
