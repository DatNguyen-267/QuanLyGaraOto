using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class SalesReport : BaseViewModel
    {
        private int _IdReport { get; set; }
        public int IdReport { get => _IdReport; set { _IdReport = value; OnPropertyChanged(); } }
        private string _ReportDate { get; set; }
        public string ReportDate { get => _ReportDate; set { _ReportDate = value; OnPropertyChanged(); } }

        private USER_INFO _uSER_INFO { get; set; }
        public USER_INFO uSER_INFO { get => _uSER_INFO; set { _uSER_INFO = value; OnPropertyChanged(); } }

        private ObservableCollection<ListSales> _ListSales;
        public ObservableCollection<ListSales> ListSales { get => _ListSales; set { _ListSales = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

    }
}
