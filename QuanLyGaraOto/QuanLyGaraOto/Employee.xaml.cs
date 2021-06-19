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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        EmployeeViewModel Viewmodel { get; set; }
        public Employee()
        {
            InitializeComponent();
            DataContext = Viewmodel = new EmployeeViewModel();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
