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
    /// Interaction logic for AddRepairDetailWindow.xaml
    /// </summary>
    public partial class AddRepairDetailWindow : Window
    {
        public AddRepairDetailViewModel addRepairDetailViewModel { get; set; }
        public AddRepairDetailWindow(REPAIR_DETAIL repair_Detail)
        {
            InitializeComponent();
            this.DataContext = (addRepairDetailViewModel = new AddRepairDetailViewModel(repair_Detail));
        }
        public AddRepairDetailWindow(REPAIR repair)
        {
            InitializeComponent();
            this.DataContext = (addRepairDetailViewModel = new AddRepairDetailViewModel(repair));
        }
    }
}
