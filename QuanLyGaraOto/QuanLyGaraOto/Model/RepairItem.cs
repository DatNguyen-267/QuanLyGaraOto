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
        private int _STT { get; set; }
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }
        private REPAIR_DETAIL _RepairInfo { get; set; }
        public REPAIR_DETAIL RepairInfo { get => _RepairInfo; set { _RepairInfo = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
    }
}
