using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto.ViewModel
{
    public class GoodBillWindowViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; set; }
        private ObservableCollection<IMPORT_GOODS> _ListImport { get; set; }

        public ObservableCollection<IMPORT_GOODS> ListImport { get => _ListImport;set { _ListImport = value; OnPropertyChanged(); } }

        private IMPORT_GOODS _SelectedItem { get; set; }
        public IMPORT_GOODS SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        public GoodBillWindowViewModel()
        {
            ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
            LoadCommand = new RelayCommand<GoodBillWindow>(
                (p)=>
                {
                    if (SelectedItem == null) return false;
                    return true;
                },
                (p)=>
                {
                    ImportPayWindow wd = new ImportPayWindow(SelectedItem);
                    if(SelectedItem.ImportGoods_TotalMoney != 0)
                    {
                        wd.btnPay.Visibility = Visibility.Hidden;
                    }    
                    wd.ShowDialog();
                }

                );
        }
    }
}
