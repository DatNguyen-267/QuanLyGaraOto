using System;
using QuanLyGaraOto.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    class ImportItem : BaseViewModel
    {
        private IMPORT_GOODS_DETAIL _ImportInfo { get; set; }

        public IMPORT_GOODS_DETAIL ImportInfo { get => _ImportInfo; set { _ImportInfo = value; OnPropertyChanged(); } }

        private string _Supplies_Name { get; set; }
        public string Supplies_Name { get => _Supplies_Name; set { _Supplies_Name = value; OnPropertyChanged(); } }

        private int _TotalMoney { get; set; }

        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
    }
}
