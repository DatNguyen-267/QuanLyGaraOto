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
    
    public partial class REPAIR_TICKET
    {
        public int ID { get; set; }
        public int Receiving_ID { get; set; }
        public System.DateTime Repair_Date { get; set; }
        public string Content { get; set; }
        public string Supply_ID { get; set; }
        public int Supplies_Amount { get; set; }
        public int Pay { get; set; }
        public int Total_Money { get; set; }
    
        public virtual RECEIVING_TICKET RECEIVING_TICKET { get; set; }
        public virtual SUPPLIES SUPPLY { get; set; }
    }
}
