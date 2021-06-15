using QuanLyGaraOto.ViewModel;
using QuanLyGaraOto.Model;
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
    /// Interaction logic for ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private ImportGoodViewModel importGoodViewModel { get; set; }
        public ImportWindow()
        {
            InitializeComponent();
            this.DataContext = (importGoodViewModel = new ImportGoodViewModel());
        }
        public ImportWindow(IMPORT_GOODS import)
        {
            InitializeComponent();
            this.DataContext = (importGoodViewModel = new ImportGoodViewModel(import));
        }

    }
}
