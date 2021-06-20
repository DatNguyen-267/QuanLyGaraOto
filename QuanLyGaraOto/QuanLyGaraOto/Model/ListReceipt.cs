using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ListReceipt:BaseViewModel
    {
        

        private string _LicensePlate { get; set; }
        public string LicensePlate { get => _LicensePlate; set { _LicensePlate = value; OnPropertyChanged(); } }

        private string _Customer_Name { get; set; }
        public string Customer_Name { get => _Customer_Name; set { _Customer_Name = value; OnPropertyChanged(); } }

        private RECEIPT _Receipt;
        public RECEIPT Receipt { get => _Receipt; set { _Receipt = value; OnPropertyChanged(); } }
      
    }
}
