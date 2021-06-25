using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class EditSupplierViewModel :BaseViewModel
    {
        private SUPPLIER _Supplier { get; set; }
        public SUPPLIER Supplier { get => _Supplier; set { _Supplier = value; OnPropertyChanged(); } }
        private string _SupplierName { get; set; }
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }
        private string _SupplierPhone { get; set; }
        public string SupplierPhone { get => _SupplierPhone; set { _SupplierPhone = value; OnPropertyChanged(); } }
        private string _SupplierEmail { get; set; }
        public string SupplierEmail { get => _SupplierEmail; set { _SupplierEmail = value; OnPropertyChanged(); } }
        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CheckSupplierName { get; set; }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public EditSupplierViewModel(SUPPLIER supplier)
        {
            IsClose = true;
            VisExistsName = false;
            this.Supplier = supplier;
            this.SupplierName = this.Supplier.Supplier_Name;
            this.SupplierPhone = this.Supplier.Supplier_Phone;
            this.SupplierEmail = this.Supplier.Supplier_Email;
            // Init data
            EditCommand = new RelayCommand<EditSupplierWindow>((p) => {
                if (string.IsNullOrEmpty(p.txtSupplierName.Text) ||
                string.IsNullOrEmpty(p.txbSupplierPhone.Text) ||
                string.IsNullOrEmpty(p.txbSupplierEmail.Text)) return false;
                if (VisExistsName) return false;
                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch((p.txbSupplierPhone.Text)) && !string.IsNullOrEmpty((p.txbSupplierPhone.Text))) return false;

                return true; }, (p) =>
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn sửa", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var tempSupplier = DataProvider.Ins.DB.SUPPLIERs.Where(x => x.Supplier_Id == this.Supplier.Supplier_Id).SingleOrDefault();
                        tempSupplier.Supplier_Name = p.txtSupplierName.Text;
                        tempSupplier.Supplier_Phone = p.txbSupplierPhone.Text;
                        tempSupplier.Supplier_Email = p.txbSupplierEmail.Text;
                        DataProvider.Ins.DB.SaveChanges();
                        IsClose = false;
                        p.Close();
                    }
            });
            CheckSupplierName = new RelayCommand<EditSupplierWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.SUPPLIERs.Where(x => x.Supplier_Name == p.txtSupplierName.Text && x.Supplier_Id != Supplier.Supplier_Id).Count() > 0)
                {
                    VisExistsName = true;
                }
            });
            CloseCommand = new RelayCommand<EditSupplierWindow>((p) => { return true; }, (p) =>
            {
              
                    p.Close();
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
