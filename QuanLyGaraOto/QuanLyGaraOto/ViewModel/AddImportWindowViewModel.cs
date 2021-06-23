using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.Model;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace QuanLyGaraOto.ViewModel
{
    class AddImportWindowViewModel : BaseViewModel
    {
        public bool IsSuccess { get; set; }

        private DateTime _ImportDate { get; set; }

        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private IMPORT_GOODS _NewImport { get; set; }
        public IMPORT_GOODS NewImport { get => _NewImport; set{ _NewImport = value; OnPropertyChanged(); } }

        private USER _user { get; set; }

        public USER user { get => _user; set { _user = value; OnPropertyChanged(); } }

        private USER_INFO _userinfo { get; set; }

        public USER_INFO userinfo { get => _userinfo; set { _userinfo = value; OnPropertyChanged(); } }

        private ObservableCollection<SUPPLIER> _ListSupplier { get; set; }
        public ObservableCollection<SUPPLIER> ListSupplier { get => _ListSupplier; set { _ListSupplier = value; OnPropertyChanged(); } }

        private SUPPLIER _SelectedSupplier { get; set; }
        public SUPPLIER SelectedSupplier { get => _SelectedSupplier; set { _SelectedSupplier = value; OnPropertyChanged(); } }


        public AddImportWindowViewModel(USER user)
        {
            this.user = user;
            userinfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == user.Users_Id).FirstOrDefault();
            IsSuccess = false;
            ImportDate = DateTime.Now;
            ListSupplier = new ObservableCollection<SUPPLIER>(DataProvider.Ins.DB.SUPPLIERs);
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
               
            });
            ConfirmCommand = new RelayCommand<AddImportWindow>((p) => {
                if (ImportDate == null) return false;
                if (SelectedSupplier == null) return false;
                return true;
            }, (p) =>
            {
                NewImport = new IMPORT_GOODS();
                NewImport.ImportGoods_Date = ImportDate;
                NewImport.IdUser = user.Users_Id;
                NewImport.ImportGoods_UserName = userinfo.UserInfo_Name;
                NewImport.IdSupplier = SelectedSupplier.Supplier_Id;
                NewImport.ImportGoods_Supplier = SelectedSupplier.Supplier_Name;
                NewImport.ImportGoods_TotalMoney = 0;

                DataProvider.Ins.DB.IMPORT_GOODS.Add(NewImport);
                DataProvider.Ins.DB.SaveChanges();
                p.Close();
                ImportWindow importWindow = new ImportWindow(NewImport);
                importWindow.ShowDialog();               
            });

        }

    }
}
