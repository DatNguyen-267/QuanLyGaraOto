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

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for ReportMonthWindow.xaml
    /// </summary>
    public partial class ReportMonthWindow : Window
    {
        public ReportMonthViewModel reportMonthViewModel { get; set; }
        public ReportMonthWindow()
        {
            InitializeComponent();           
        }
        public ReportMonthWindow(bool check,USER User)
        {
            InitializeComponent();
            this.DataContext = (reportMonthViewModel = new ReportMonthViewModel(check,User));
        }
    }
}
