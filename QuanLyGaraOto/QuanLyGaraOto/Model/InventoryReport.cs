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

        private string _UserName { get; set; }
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private ObservableCollection<ListInventory> _ListInventory;
        public ObservableCollection<ListInventory> ListInventory { get => _ListInventory; set { _ListInventory = value; OnPropertyChanged(); } }
        
    }
}
