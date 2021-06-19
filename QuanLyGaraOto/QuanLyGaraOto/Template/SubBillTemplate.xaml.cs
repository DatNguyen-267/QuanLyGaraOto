using QuanLyGaraOto.Model;
using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyGaraOto.Template
{
    /// <summary>
    /// Interaction logic for SubBillTemplate.xaml
    /// </summary>
    public partial class SubBillTemplate : Window
    {
        public SubBillViewModel subBillViewModel { get; set; }
        public SubBillTemplate(ObservableCollection<ListRepair> listRepairs)
        {
            InitializeComponent();
            this.DataContext = subBillViewModel = new SubBillViewModel (listRepairs);
        }
    }
}
