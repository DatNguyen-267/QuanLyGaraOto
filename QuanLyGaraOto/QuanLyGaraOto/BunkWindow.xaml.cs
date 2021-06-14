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
using System.Text.RegularExpressions;
using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for BunkWindow.xaml
    /// </summary>
    public partial class BunkWindow : UserControl
    {
        private BunkViewModel bunkviewmodel { get; set; }
        public BunkWindow()
        {
            InitializeComponent();
            this.DataContext = (bunkviewmodel = new BunkViewModel());
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
    }
}
