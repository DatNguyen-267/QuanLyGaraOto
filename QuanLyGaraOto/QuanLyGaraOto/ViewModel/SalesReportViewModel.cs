using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class SalesReportViewModel : BaseViewModel
    {
        private SalesReport _SalesReport { get; set; }
        public SalesReport SalesReport { get => _SalesReport; set { _SalesReport = value; OnPropertyChanged(); } }

        private string _ReportDate { get; set; }
        public string ReportDate { get => _ReportDate; set { _ReportDate = value; OnPropertyChanged(); } }

        private USER_INFO _uSER_INFO { get; set; }
        public USER_INFO uSER_INFO { get => _uSER_INFO; set { _uSER_INFO = value; OnPropertyChanged(); } }

        private ObservableCollection<ListSales> _ListSales;
        public ObservableCollection<ListSales> ListSales { get => _ListSales; set { _ListSales = value; OnPropertyChanged(); } }

        public SalesReportViewModel(SalesReport salesReport )
        {
            SalesReport = salesReport;
            ReportDate = salesReport.ReportDate;
            uSER_INFO = salesReport.uSER_INFO;
            ListSales = salesReport.ListSales;
        }
        public void Command()
        {

        }
    }
}
