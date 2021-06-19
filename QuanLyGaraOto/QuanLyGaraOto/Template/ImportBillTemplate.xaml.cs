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
using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;

namespace QuanLyGaraOto.Template
{
    /// <summary>
    /// Interaction logic for ImportBillTemplate.xaml
    /// </summary>
    public partial class ImportBillTemplate : Window
    {
        public ImportBillViewModel importBillViewModel { get; set; }
        public ImportBillTemplate()
        {
            InitializeComponent();
            this.DataContext = (importBillViewModel = new ImportBillViewModel());
        }
        public ImportBillTemplate(IMPORT_GOODS import)
        {
            InitializeComponent();
            this.DataContext = (importBillViewModel = new ImportBillViewModel(import));
        }
    }
}
