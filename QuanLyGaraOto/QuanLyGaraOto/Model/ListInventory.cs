using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ListInventory : BaseViewModel
    {
        private int _STT { get; set; }
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }

        private int _IdSupplies { get; set; }
        public int IdSupplies { get => _IdSupplies; set { _IdSupplies = value; OnPropertyChanged(); } }

        private string _Supplies_Name { get; set; }
        public string Supplies_Name { get => _Supplies_Name; set { _Supplies_Name = value; OnPropertyChanged(); } }

        private int _TonDau { get; set; }
        public int TonDau { get => _TonDau; set { _TonDau = value; OnPropertyChanged(); } }

        private int _PhatSinh { get; set; }
        public int PhatSinh { get => _PhatSinh; set { _PhatSinh = value; OnPropertyChanged(); } }

        private int _TonCuoi { get; set; }
        public int TonCuoi { get => _TonCuoi; set { _TonCuoi = value; OnPropertyChanged(); } }
    }
}
