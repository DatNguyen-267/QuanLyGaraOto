using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyGaraOto.Template
{
    /// <summary>
    /// Interaction logic for ReportSalesTemplate.xaml
    /// </summary>
    public partial class ReportSalesTemplate : Window
    {
     
        SalesReportViewModel salesReportViewModel { get; set; }
        public ReportSalesTemplate(DateTime date)
        {
            InitializeComponent();
            this.DataContext = (salesReportViewModel = new SalesReportViewModel(date));
        }

    }
}
