﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private SUPPLIER _Supplier { get; set; }
        public SUPPLIER Supplier { get; set; }

        private USER_INFO _userinfo { get; set; }
        public USER_INFO userinfo { get => _userinfo; set { _userinfo = value; OnPropertyChanged(); } }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public ImportBillViewModel()
        {

        }
        public ImportBillViewModel(IMPORT_GOODS import)
        {
            IsClose = true;
            Import = import;
            Supplier = DataProvider.Ins.DB.SUPPLIERs.Where(x => x.Supplier_Id == Import.IdSupplier).FirstOrDefault();
            userinfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == Import.IdUser).FirstOrDefault();
            
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
