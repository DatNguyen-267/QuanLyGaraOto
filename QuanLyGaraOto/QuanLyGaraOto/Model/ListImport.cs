using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.ViewModel;

namespace QuanLyGaraOto.Model
{
    public class ListImport : BaseViewModel
    {
        private int _STT { get; set; }
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }
        private IMPORT_GOODS_DETAIL _ImportDetail { get; set; }
        public IMPORT_GOODS_DETAIL ImportDetail { get => _ImportDetail; set { _ImportDetail = value; OnPropertyChanged(); } }
    }
}
