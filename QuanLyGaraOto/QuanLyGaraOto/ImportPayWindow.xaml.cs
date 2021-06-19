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
using QuanLyGaraOto.ViewModel;
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for ImportPayWindow.xaml
    /// </summary>
    public partial class ImportPayWindow : Window
    {
        public ImportPayViewModel importPayViewModel { get; set; }

        public ImportPayWindow()
        {
            InitializeComponent();
        }
        public ImportPayWindow(IMPORT_GOODS import)
        {
            InitializeComponent();
            this.DataContext = (importPayViewModel = new ImportPayViewModel(import));
        }
    }
}
