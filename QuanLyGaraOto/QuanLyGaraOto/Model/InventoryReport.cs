using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class InventoryReport:BaseViewModel
    {
        private int _IdReport { get; set; }
        public int IdReport { get => _IdReport; set { _IdReport = value; OnPropertyChanged(); } }
        private string _ReportDate { get; set; }
        public string ReportDate { get => _ReportDate; set { _ReportDate = value; OnPropertyChanged(); } }

        private USER_INFO _uSER_INFO { get; set; }
        public USER_INFO uSER_INFO { get => _uSER_INFO; set { _uSER_INFO = value; OnPropertyChanged(); } }

        private ObservableCollection<ListInventory> _ListInventory;
        public ObservableCollection<ListInventory> ListInventory { get => _ListInventory; set { _ListInventory = value; OnPropertyChanged(); } }
        
    }
}
