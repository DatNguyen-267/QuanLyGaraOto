﻿using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AddSupplierViewModel : BaseViewModel
    {
        private string _SupplierName { get; set; }
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }
        private string _SupplierPhone { get; set; }
        public string SupplierPhone { get => _SupplierPhone; set { _SupplierPhone = value; OnPropertyChanged(); } }
        private string _SupplierEmail { get; set; }
        public string SupplierEmail { get => _SupplierEmail; set { _SupplierEmail = value; OnPropertyChanged(); } }
        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CheckSupplierName { get; set; }
        public AddSupplierViewModel()
        {
            VisExistsName = false;
            // Init data
            AddCommand = new RelayCommand<AddSupplierWindow>((p) => {
                if (string.IsNullOrEmpty(p.txtSupplierName.Text) ||
                string.IsNullOrEmpty(p.txbSupplierPhone.Text) ||
                string.IsNullOrEmpty(p.txbSupplierEmail.Text)) return false;
                if (VisExistsName == true) return false;

                return true;
            }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn thêm", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SUPPLIER Supplier = new SUPPLIER();
                    Supplier.Supplier_Name = p.txtSupplierName.Text;
                    Supplier.Supplier_Phone = p.txbSupplierPhone.Text;
                    Supplier.Supplier_Email = p.txbSupplierEmail.Text;
                    DataProvider.Ins.DB.SUPPLIERs.Add(Supplier);
                    DataProvider.Ins.DB.SaveChanges();
                    p.Close();
                }
                    
            });
            CheckSupplierName = new RelayCommand<AddSupplierWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.SUPPLIERs.Where(x => x.Supplier_Name == p.txtSupplierName.Text).Count() == 0)
                {
                    VisExistsName = false;
                }
                else VisExistsName = true;
            });
            CloseCommand = new RelayCommand<AddSupplierWindow>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
            });
        }
    }
}