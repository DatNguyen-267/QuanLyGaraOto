using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyGaraOto.Convert;
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto.ViewModel
{
    public class GoodBillWindowViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; set; }
        private ObservableCollection<IMPORT_GOODS> _ListImport { get; set; }
        public ObservableCollection<IMPORT_GOODS> ListImport { get => _ListImport;set { _ListImport = value; OnPropertyChanged(); } }
        private ObservableCollection<IMPORT_GOODS> _TempListImport { get; set; }
        public ObservableCollection<IMPORT_GOODS> TempListImport { get => _TempListImport; set { _TempListImport = value; OnPropertyChanged(); } }

        private IMPORT_GOODS _SelectedItem { get; set; }
        public IMPORT_GOODS SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand CloseCommand { get; set; }
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
                });
            SearchCommand = new RelayCommand<GoodBillWindow>((p) => {
                if (p == null) return false;
                if (string.IsNullOrEmpty(p.txbID.Text) && string.IsNullOrEmpty(p.dpImportDate.Text))
                    return false;
                return true;
            }, (p) =>
            {
                ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
                UnicodeConvert uni = new UnicodeConvert();
                TempListImport = new ObservableCollection<IMPORT_GOODS>();
                foreach (var item in ListImport)
                {
                    if ((string.IsNullOrEmpty(p.txbID.Text) ||
                    (!string.IsNullOrEmpty(p.txbID.Text) && uni.RemoveUnicode(item.ImportGoods_Id.ToString()).ToLower().Contains(uni.RemoveUnicode(p.txbID.Text).ToLower())))
                    && ((string.IsNullOrEmpty(p.dpImportDate.Text) ||
                    (!string.IsNullOrEmpty(p.dpImportDate.Text) && item.ImportGoods_Date.Date.Day == p.dpImportDate.SelectedDate.Value.Date.Day
                    && item.ImportGoods_Date.Date.Month == p.dpImportDate.SelectedDate.Value.Date.Month
                    && item.ImportGoods_Date.Date.Year == p.dpImportDate.SelectedDate.Value.Date.Year)))
                    )
                        TempListImport.Add(item);
                }
                ListImport = TempListImport;
            });
            RefeshCommand = new RelayCommand<GoodBillWindow>((p) => { return true; }, (p) =>
            {
                p.txbID.Text = "";
                p.dpImportDate.Text = "";
                ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
            });
            CloseCommand = new RelayCommand<GoodBillWindow>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
            });
        }
    }
}
