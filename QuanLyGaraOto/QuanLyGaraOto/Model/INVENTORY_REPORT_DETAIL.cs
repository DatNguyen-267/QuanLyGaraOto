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
    
    public partial class INVENTORY_REPORT_DETAIL
    {
        public int InventoryReportDetail_Id { get; set; }
        public int IdInventoryReport { get; set; }
        public int IdSupplies { get; set; }
        public int TonDau { get; set; }
        public int PhatSinh { get; set; }
        public int TonCuoi { get; set; }
    
        public virtual INVENTORY_REPORT INVENTORY_REPORT { get; set; }
        public virtual SUPPLIES SUPPLIES { get; set; }
    }
}
