using System;
using System.Collections.Generic;
using QuanLyGaraOto.ViewModel;
using QuanLyGaraOto.Model;
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
    /// Interaction logic for AddImportWindow.xaml
    /// </summary>
    public partial class AddImportWindow : Window
    {
        private AddImportWindowViewModel addImportWindowViewModel { get; set; }
        public AddImportWindow(USER user)
        {
            InitializeComponent();
            this.DataContext = (addImportWindowViewModel = new AddImportWindowViewModel(user));
        }
    }
}
