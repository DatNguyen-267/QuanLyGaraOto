using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
     public class SubBillViewModel : BaseViewModel
    {

        private ObservableCollection<ListRepair> _ListRepair { get; set; }
        public ObservableCollection<ListRepair> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public SubBillViewModel(ObservableCollection<ListRepair> listRepairs)
        {
            ListRepair = listRepairs;
        }
    }
}
