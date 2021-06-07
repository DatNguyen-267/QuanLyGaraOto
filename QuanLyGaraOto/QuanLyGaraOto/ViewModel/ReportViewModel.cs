using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    class ReportViewModel : BaseViewModel
    {

        public ICommand ReportCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand YearChangedCommand { get; set; }
        public ICommand MonthChangedCommand { get; set; }
        public ICommand LoadCbCommand { get; set; }

        public ICommand ReportSalesCommand { get; set; }
        public ICommand ReportInventoryCommand { get; set; }

        public ICommand Report_YearChangedCommand { get; set; }
        public ICommand Report_MonthChangedCommand { get; set; }
        

        private string first_item_year { get; set; }
        public string First_item_year { get => first_item_year; set { first_item_year = value; OnPropertyChanged(); } }
        private string first_item_month { get; set; }
        public string First_item_month { get => first_item_month; set { first_item_month = value; OnPropertyChanged(); } }

        private ObservableCollection<string> itemSource_Year = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSource_Year { get => itemSource_Year; set { itemSource_Year = value; OnPropertyChanged(); } }

        private ObservableCollection<string> itemSource_Month = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSource_Month { get => itemSource_Month; set { itemSource_Month = value; OnPropertyChanged(); } }


        private ObservableCollection<RECEIPT> _List;
        public ObservableCollection<RECEIPT> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<ImportTemp> _ListImport;
        public ObservableCollection<ImportTemp> ListImport { get => _ListImport; set { _ListImport = value; OnPropertyChanged(); } }

        private ObservableCollection<ListSales> _ListSales;
        public ObservableCollection<ListSales> ListSales { get => _ListSales; set { _ListSales = value; OnPropertyChanged(); } }

        private int _TotalMoney;

        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        public ReportViewModel(ObservableCollection<string> itemsource_Year, ObservableCollection<string> itemsource_Month)
        {
            ItemSource_Year = itemsource_Year;
            
        }

        public ReportViewModel()
        {
            load_firstItem();

            Report_MonthChangedCommand = new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report_LoadToView(p); });

            Report_YearChangedCommand = new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report_LoadToView(p); });

            LoadCbCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => {  LoadComboBox(); });

            ReportSalesCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ReportMonthWindow wd = new ReportMonthWindow(); wd.ShowDialog(); });

            MonthChangedCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => { LoadToView(p); });

            YearChangedCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => { LoadToView(p); });

            CloseCommand=new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { CloseWd(p); });

            ReportCommand= new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report(p); });
        }

        public void load_firstItem()
        {
            First_item_year = "Năm " + DateTime.Now.Year;
            ItemSource_Year.Add(First_item_year);
            First_item_month = "Tháng " + DateTime.Now.Month;
            ItemSource_Month.Add(First_item_month);


        }
        public void LoadToView(ReportWindow p)
        {

            string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
            string selectedYear = tmp[1];
            string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
            string selectedMonth = tmp1[1];

            Load_KinhDoanh(selectedYear, selectedMonth);
            Load_NhapKho(selectedYear, selectedMonth);
            
        }

        public void Load_KinhDoanh(string selectedYear,string selectedMonth)
        {
            List = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            ObservableCollection<RECEIPT> temp = new ObservableCollection<RECEIPT>();
            foreach (var item in List)
            {

                if (item.ReceiptDate.Year.ToString() == selectedYear && item.ReceiptDate.Month.ToString() == selectedMonth)
                    temp.Add(item);
            }

            List = temp;

        }
        public void Load_NhapKho(string selectedYear, string selectedMonth)
        {
            ListImport = new ObservableCollection<ImportTemp>();



            var ImportInfoList = DataProvider.Ins.DB.IMPORT_GOODS_DETAIL;
            foreach (var item in ImportInfoList)
            {

                ImportTemp importTemp = new ImportTemp();


                var suppliesNameList = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == item.IdSupplies).SingleOrDefault();
                importTemp.Supplies_Name = suppliesNameList.Supplies_Name;
                var importDateList = DataProvider.Ins.DB.IMPORT_GOODS.Where(x => x.ImportGoods_Id == item.IdImportGood).SingleOrDefault();
                importTemp.ImportGoods_Date = importDateList.ImportGoods_Date;
                if (importDateList.ImportGoods_Date.Year.ToString() == selectedYear && importDateList.ImportGoods_Date.Month.ToString() == selectedMonth)
                {
                    importTemp.ImportInfo = item;
                    ListImport.Add(importTemp);
                }
            }
        }

        
        public void LoadComboBox()
        {

            string selectedYear = DateTime.Now.Year.ToString();
            string selectedMonth=DateTime.Now.Month.ToString();
            Load_KinhDoanh(selectedYear,selectedMonth);
            Load_NhapKho(selectedYear, selectedMonth);
            ReportSales_LoadToView(selectedYear, selectedMonth);


            ObservableCollection<RECEIPT> temp = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            foreach (var item in temp)
            {
                if(item.ReceiptDate.Year!=DateTime.Now.Year)
                    ItemSource_Year.Add("Năm "+item.ReceiptDate.Year.ToString());       
            }
            for(int i=1;i<13;i++)
                if (DateTime.Now.Month!=i)
                     ItemSource_Month.Add("Tháng " + i);
           
        }
        
        public void Report_LoadToView(ReportMonthWindow p)
        {
            
            string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
            string selectedYear = tmp[1];
            string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
            string selectedMonth = tmp1[1];
            ReportSales_LoadToView(selectedYear, selectedMonth);
        }
        public void ReportSales_LoadToView(string selectedYear,string selectedMonth)
        {
            ListSales = new ObservableCollection<ListSales>();

            int i = 1;

            TotalMoney = 0;
            var a = DataProvider.Ins.DB.RECEIPTs.Where(x=>x.ReceiptDate.Year.ToString() == selectedYear && x.ReceiptDate.Month.ToString() == selectedMonth);
            foreach(var item in a)
            {
                TotalMoney += item.MoneyReceived;
            }
           

            var CarBrandList = DataProvider.Ins.DB.CAR_BRAND;
            
            foreach (var item in CarBrandList)
            {

                ListSales listSales = new ListSales();
                int amountOfTurn = 0;
                int totalMoney = 0;
                float rate = 0;

                var ReceptionList = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.IdCarBrand == item.CarBrand_Id );
                if(ReceptionList!=null)
                {
                    foreach(var temp in ReceptionList)
                    {
                        
                        var ReceiptList = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == temp.Reception_Id && x.ReceiptDate.Year.ToString() == selectedYear && x.ReceiptDate.Month.ToString() == selectedMonth).SingleOrDefault();
                        if (ReceiptList != null)
                        {
                            
                            amountOfTurn++;
                            totalMoney +=ReceiptList.MoneyReceived;
                            

                        }
                    }
                    
                }
                if (TotalMoney == 0)
                    rate = 0;
                else
                    rate = totalMoney / TotalMoney;
                listSales.STT = i;
                listSales.IdCarBrand = item.CarBrand_Id;
                listSales.CarBrand_Name = item.CarBrand_Name;
                listSales.AmountOfTurn = amountOfTurn;
                listSales.TotalMoney = totalMoney;
                listSales.Rate = rate;
                ListSales.Add(listSales);
                i++;

            }
        }

        public void CloseWd(ReportMonthWindow p)
        {
            p.Close();
        }
       
        public void Report(ReportMonthWindow p)
        {
            
                string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
                int selectedYear = Int32.Parse(tmp[1]);
                string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
                int selectedMonth = Int32.Parse(tmp1[1]);
   
                DateTime date = DateTime.Now;
              
            if (!(date.Year < selectedYear || date.Year == selectedYear && date.Month <= selectedMonth))
             
            {
               
                if(check())
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn lập báo cáo", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {

                        DataProvider.Ins.DB.SALES_REPORT.Add(new SALES_REPORT { SalesReport_Date = DateTime.Now, SalesReport_Revenue = TotalMoney });

                        DataProvider.Ins.DB.SaveChanges();
                        int i = 0;
                        var salesReport = DataProvider.Ins.DB.SALES_REPORT;
                        foreach (var item in salesReport)
                        {
                            if (item.SalesReport_Id > i)
                                i = item.SalesReport_Id;
                        }

                        foreach (var item in ListSales)
                        {
                            SALES_REPORT_DETAIL salesReportDetail = new SALES_REPORT_DETAIL();
                            salesReportDetail.IdSalesReport = i;
                            salesReportDetail.IdCarBrand = item.IdCarBrand;
                            salesReportDetail.AmountOfTurn = item.AmountOfTurn;
                            salesReportDetail.TotalMoney = item.TotalMoney;
                            salesReportDetail.Rate = item.Rate;

                            DataProvider.Ins.DB.SALES_REPORT_DETAIL.Add(salesReportDetail);
                            DataProvider.Ins.DB.SaveChanges();

                        }

                        MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        p.Close();
                    }
                }
               


            }

        }
        public bool check()
        {
            var sales_report = DataProvider.Ins.DB.SALES_REPORT;
            foreach(var item in sales_report)
            {
                if (DateTime.Now.Year == item.SalesReport_Date.Year && DateTime.Now.Month == item.SalesReport_Date.Month)
                    return false;
            }
            return true;
        }

    }
}
