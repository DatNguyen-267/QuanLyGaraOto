using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for AddNewGoodWindow.xaml
    /// </summary>
    public partial class AddNewGoodWindow : Window
    {
        public AddNewGoodViewModel addNewGoodViewModel { get; set; }
        public AddNewGoodWindow()
        {
            InitializeComponent();
            this.DataContext = (addNewGoodViewModel = new AddNewGoodViewModel());
        }
        public AddNewGoodWindow(SUPPLIES supplies)
        {
            InitializeComponent();
            this.DataContext = (addNewGoodViewModel = new AddNewGoodViewModel(supplies));
        }
    }
}
