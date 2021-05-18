using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class RepairItem : BaseViewModel
    {
        private RepairInfo _RepairInfo { get; set; }
        public RepairInfo RepairInfo { get => _RepairInfo; set { _RepairInfo = value; OnPropertyChanged(); } }
        private int _Price { get; set; }
        public int Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
    }
}
