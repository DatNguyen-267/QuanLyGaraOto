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
    
    public partial class CAR_IN_DEBT
    {
        public string License_Number { get; set; }
        public Nullable<int> Debt { get; set; }
    
        public virtual LICENSE_PLATE LICENSE_PLATE { get; set; }
    }
}
