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
    
    public partial class REPAIRINFO
    {
        public int Id { get; set; }
        public int IdRepairForm { get; set; }
        public string Content { get; set; }
        public Nullable<int> IdSupply { get; set; }
        public Nullable<int> SuppliesAmount { get; set; }
        public int IdPay { get; set; }
        public int TotalMoney { get; set; }
    
        public virtual PAY PAY { get; set; }
        public virtual REPAIRFORM REPAIRFORM { get; set; }
        public virtual SUPPLIES SUPPLIES { get; set; }
    }
}
