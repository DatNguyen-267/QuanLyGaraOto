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
    
    public partial class SALES_REPORT_DETAIL
    {
        public int IdSalesReport { get; set; }
        public int IdCarBrand { get; set; }
        public Nullable<int> AmountOfTurn { get; set; }
        public Nullable<int> TotalMoney { get; set; }
        public Nullable<double> Rate { get; set; }
    
        public virtual CAR_BRAND CAR_BRAND { get; set; }
        public virtual SALES_REPORT SALES_REPORT { get; set; }
    }
}
