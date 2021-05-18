using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ListCar : BaseViewModel
    {
        private CARRECEPTION _CarReception { get; set; }
        public CARRECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private int _Debt { get; set; }
        public int Debt { get => _Debt; set { _Debt = value; OnPropertyChanged(); } }
    }
}
