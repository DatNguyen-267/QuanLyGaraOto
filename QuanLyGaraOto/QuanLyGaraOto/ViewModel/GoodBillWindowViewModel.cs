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
        private ObservableCollection<SUPPLIER> _ListSupplier { get; set; }
        public ObservableCollection<SUPPLIER> ListSupplier { get => _ListSupplier; set { _ListSupplier = value; OnPropertyChanged(); } }
        private IMPORT_GOODS _SelectedItem { get; set; }
        public IMPORT_GOODS SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public GoodBillWindowViewModel()
        {
            IsClose = true;
            ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
            ListSupplier = new ObservableCollection<SUPPLIER>(DataProvider.Ins.DB.SUPPLIERs);
            LoadCommand = new RelayCommand<GoodBillWindow>(
                (p)=>
                {
                    if (SelectedItem == null) return false;
                    return true;
                },
                (p)=>
                {
                    ImportWindow wd = new ImportWindow(SelectedItem);
                    if(SelectedItem.ImportGoods_TotalMoney != 0)
                    {
                        wd.btnPay.Visibility = Visibility.Hidden;
                        wd.btnView.Visibility = Visibility.Visible;
                        
                    }    
                    wd.ShowDialog();
                });
            SearchCommand = new RelayCommand<GoodBillWindow>((p) => {
                if (p == null) return false;
                if (string.IsNullOrEmpty(p.txbID.Text) && string.IsNullOrEmpty(p.dpImportDate.Text)
                && string.IsNullOrEmpty(p.cbSupplier.Text))
                    return false;
                return true;
            }, (p) =>
            {
                ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
                UnicodeConvert uni = new UnicodeConvert();
                TempListImport = new ObservableCollection<IMPORT_GOODS>();
                foreach (var item in ListImport)
                {
                    if ((string.IsNullOrEmpty(p.cbSupplier.Text) || (!string.IsNullOrEmpty(p.cbSupplier.Text) && (item.SUPPLIER.Supplier_Name == p.cbSupplier.Text)))
                    &&
                    (string.IsNullOrEmpty(p.txbID.Text) ||
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
                p.cbSupplier.Text = "";
                ListImport = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
            });
            CloseCommand = new RelayCommand<GoodBillWindow>((p) => { return true; }, (p) =>
            {
               
                    p.Close();
            });
            ExportCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PrintViewModel printViewModel = new PrintViewModel();
                printViewModel.XuatDanhSachDonNhapHang(ListImport);

            });
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsClose)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else e.Cancel = true;
            }
            else e.Cancel = false;
        }
    }
}
