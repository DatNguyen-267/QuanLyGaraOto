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

    public partial class AddRoleWindow : Window
    {
        EmployeeViewModel Viewmodel { get; set; }
        public AddRoleWindow()
        {
            InitializeComponent();
            DataContext = Viewmodel = new EmployeeViewModel();
        }
    }
}
