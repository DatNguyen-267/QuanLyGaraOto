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
    
    public partial class REPAIR_TICKET : BaseViewModel
    {
        private int _ID;
        public int ID
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged(); }
        }
        private int _Receiving_ID;
        public int Receiving_ID
        {
            get => _Receiving_ID;
            set { _Receiving_ID = value; OnPropertyChanged(); }
        }
        private System.DateTime _Repair_Date;
        public System.DateTime Repair_Date
        {
            get => _Repair_Date;
            set { _Repair_Date = value; OnPropertyChanged(); }
        }
        private string _Content;
        public string Content
        {
            get => _Content;
            set { _Content = value; OnPropertyChanged(); }
        }
        private string _Supply_ID;
        public string Supply_ID
        {
            get => _Content;
            set { _Content = value; OnPropertyChanged(); }
        }
        private int _Supplies_Amount;
        public int Supplies_Amount
        {
            get => _Supplies_Amount;
            set { _Supplies_Amount = value; OnPropertyChanged(); }
        }
        private int _Pay_ID;
        public int Pay_ID
        {
            get => _Pay_ID;
            set { _Pay_ID = value; OnPropertyChanged(); }
        }
        private int _Total_Money;
        public int Total_Money
        {
            get => _Total_Money;
            set { _Total_Money = value; OnPropertyChanged(); }
        }

        private PAY _PAY;
        public virtual PAY PAY
        {
            get => _PAY;
            set { _PAY = value; OnPropertyChanged(); }
        }
        private RECEIVING_TICKET _RECEIVING_TICKET;
        public virtual RECEIVING_TICKET RECEIVING_TICKET
        {
            get => _RECEIVING_TICKET;
            set { _RECEIVING_TICKET = value; OnPropertyChanged(); }
        }
        private SUPPLIES _SUPPLY;
        public virtual SUPPLIES SUPPLY
        {
            get => _SUPPLY;
            set { _SUPPLY = value; OnPropertyChanged(); }
        }
    }
}
