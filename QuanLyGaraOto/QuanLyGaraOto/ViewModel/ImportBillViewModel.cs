using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto.ViewModel
{
    public class ImportBillViewModel : BaseViewModel
    {

        private IMPORT_GOODS _Import { get; set; }

        public IMPORT_GOODS Import { get => _Import; set { _Import = value; OnPropertyChanged(); } }

        private ObservableCollection<IMPORT_GOODS_DETAIL> _ListImport { get; set; }
        public ObservableCollection<IMPORT_GOODS_DETAIL> ListImport { get => _ListImport; set { _ListImport = value; OnPropertyChanged(); } }

        private ObservableCollection<ListImport> _List { get; set; }
        public ObservableCollection<ListImport> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        public ImportBillViewModel()
        {

        }
        public ImportBillViewModel(IMPORT_GOODS import)
        {
            Import = import;
            int i = 1;
            ListImport = new ObservableCollection<IMPORT_GOODS_DETAIL>(DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Where(x=> x.IdImportGood == import.ImportGoods_Id));
            List = new ObservableCollection<ListImport>();
            foreach(var item in ListImport)
            {
                ListImport temp = new ListImport();
                temp.STT = i++;
                temp.ImportDetail = item;
                List.Add(temp);
            }    

        }
    }
}
