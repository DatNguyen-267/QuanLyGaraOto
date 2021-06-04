using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ListRepair :BaseViewModel
    {
        private int _STT { get; set; }
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }
        private REPAIR_DETAIL _RepairDetail { get; set; }
        public REPAIR_DETAIL RepairDetail { get => _RepairDetail; set { _RepairDetail = value; OnPropertyChanged(); } }
    }
}
