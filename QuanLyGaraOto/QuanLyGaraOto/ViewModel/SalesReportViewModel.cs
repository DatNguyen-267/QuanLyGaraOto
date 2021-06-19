﻿using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class SalesReportViewModel : BaseViewModel
    {
        private string _Date;
        public string Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }

        private int _IdReport;
        public int IdReport { get => _IdReport; set { _IdReport = value; OnPropertyChanged(); } }

        private int _TotalMoney;
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private ObservableCollection<ListSales> _ListSales;
        public ObservableCollection<ListSales> ListSales { get => _ListSales; set { _ListSales = value; OnPropertyChanged(); } }

        public SalesReportViewModel(DateTime date)
        {
            ListSales = new ObservableCollection<ListSales>();

            Date = date.Month.ToString() + "/" + date.Year.ToString();
            var SalesReport = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year == date.Year && x.SalesReport_Date.Month == date.Month).SingleOrDefault();
            var SalesReportDetail = DataProvider.Ins.DB.SALES_REPORT_DETAIL.Where(x => x.IdSalesReport == SalesReport.SalesReport_Id);

            IdReport = SalesReport.SalesReport_Id;
            TotalMoney = SalesReport.SalesReport_Revenue;
            int i = 1;
            foreach (var item in SalesReportDetail)
            {
                ListSales listSales = new ListSales();
                listSales.TotalMoney = (int)item.TotalMoney;
                listSales.Rate = (float)item.Rate;
                listSales.STT = i;
                var CarBrand = DataProvider.Ins.DB.CAR_BRAND.Where(x => x.CarBrand_Id == item.IdCarBrand).SingleOrDefault();
                listSales.CarBrand_Name = CarBrand.CarBrand_Name;
                listSales.AmountOfTurn = (int)item.AmountOfTurn;
                ListSales.Add(listSales);
                i++;
            }

        }
    }
}
