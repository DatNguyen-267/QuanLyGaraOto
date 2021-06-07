using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ListSales:BaseViewModel
    {
        private int _IdCarBrand { get; set; }
        public int IdCarBrand { get => _IdCarBrand; set { _IdCarBrand = value; OnPropertyChanged(); } }
        private int _STT { get; set; }
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }
        private string _CarBrand_Name { get; set; }
        public string CarBrand_Name { get => _CarBrand_Name; set { _CarBrand_Name = value; OnPropertyChanged(); } }

        private int _AmountOfTurn { get; set; }
        public int AmountOfTurn { get => _AmountOfTurn; set { _AmountOfTurn = value; OnPropertyChanged(); } }

        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private float _Rate { get; set; }
        public float Rate { get => _Rate; set { _Rate = value; OnPropertyChanged(); } }
    }
}
