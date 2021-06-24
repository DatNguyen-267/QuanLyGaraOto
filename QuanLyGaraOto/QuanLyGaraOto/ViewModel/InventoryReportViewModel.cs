﻿using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyGaraOto.ViewModel
{
    public class InventoryReportViewModel : BaseViewModel
    {
        private string _Date;
        public string Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }
        private string _UserName { get; set; }
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private int _IdReport;
        public int IdReport { get => _IdReport; set { _IdReport = value; OnPropertyChanged(); } }

        private ObservableCollection<ListInventory> _ListInventory;
        public ObservableCollection<ListInventory> ListInventory { get => _ListInventory; set { _ListInventory = value; OnPropertyChanged(); } }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public InventoryReportViewModel(DateTime date)
        {
            ListInventory = new ObservableCollection<ListInventory>();

            Date = date.Month.ToString() + "/" + date.Year.ToString();
            var InventoryReport = DataProvider.Ins.DB.INVENTORY_REPORT.Where(x => x.InventoryReport_Date.Year == date.Year && x.InventoryReport_Date.Month == date.Month).SingleOrDefault();
            var InventoryReportDetail = DataProvider.Ins.DB.INVENTORY_REPORT_DETAIL.Where(x => x.IdInventoryReport == InventoryReport.InventoryReport_Id);
            UserName = InventoryReport.InventoryReport_UserName;
            IdReport = InventoryReport.InventoryReport_Id;
            int i = 1;
            foreach (var item in InventoryReportDetail)
            {
                ListInventory listInventory = new ListInventory();
                listInventory.TonCuoi = item.TonCuoi;
                listInventory.TonDau = item.TonDau;
                listInventory.PhatSinh = item.PhatSinh;
                var Supplies = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == item.IdSupplies).SingleOrDefault();
                listInventory.Supplies_Name = Supplies.Supplies_Name;
                listInventory.STT = i;
                ListInventory.Add(listInventory);
                i++;

            }
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
