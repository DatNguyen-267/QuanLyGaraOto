using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
   public class ImportTemp:BaseViewModel
    {
        private IMPORT_GOODS_DETAIL _ImportInfo { get; set; }
        public IMPORT_GOODS_DETAIL ImportInfo { get => _ImportInfo; set { _ImportInfo = value; OnPropertyChanged(); } }
        private string _Supplies_Name { get; set; }
        public string Supplies_Name { get => _Supplies_Name; set { _Supplies_Name = value; OnPropertyChanged(); } }
        private DateTime _ImportGoods_Date { get; set; }
        public DateTime ImportGoods_Date { get => _ImportGoods_Date; set { _ImportGoods_Date = value; OnPropertyChanged(); } }
    }
}
