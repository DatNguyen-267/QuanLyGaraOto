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
    /// Interaction logic for EditSupplierWindow.xaml
    /// </summary>
    public partial class EditSupplierWindow : Window
    {
        public EditSupplierViewModel editSupplierViewModel { get; set; }
        public EditSupplierWindow(SUPPLIER supplier )
        {
            InitializeComponent();
            this.DataContext = editSupplierViewModel = new EditSupplierViewModel(supplier);
        }
    }
}
