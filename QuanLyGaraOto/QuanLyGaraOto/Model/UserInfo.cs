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
    
    public partial class UserInfo
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Telephone { get; set; }
        public string CMND { get; set; }
    
        public virtual User User { get; set; }
    }
}
