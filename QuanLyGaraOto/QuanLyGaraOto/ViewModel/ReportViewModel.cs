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
       
        private ObservableCollection<ListInventory> _ListInventory;
        public ObservableCollection<ListInventory> ListInventory { get => _ListInventory; set { _ListInventory = value; OnPropertyChanged(); } }
        private bool _VisView { get; set; }
        public bool VisView { get => _VisView; set { _VisView = value; OnPropertyChanged(); } }

        private bool _VisListView_Sales { get; set; }
        public bool VisListView_Sales { get => _VisListView_Sales; set { _VisListView_Sales = value; OnPropertyChanged(); } }

        private bool _VisListView_Inventory { get; set; }
        public bool VisListView_Inventory { get => _VisListView_Inventory; set { _VisListView_Inventory = value; OnPropertyChanged(); } }

        private int _TotalMoney;

        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private string _ReportName { get; set; }
        public string ReportName { get => _ReportName; set { _ReportName = value; OnPropertyChanged(); } }

        bool _isReport = false;
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }

        public ReportViewModel(bool role) : this()
        {
            isReport = role;
        }


        public ReportViewModel(ObservableCollection<string> itemsource_Year, ObservableCollection<string> itemsource_Month)
        {
            ItemSource_Year = itemsource_Year;
            
        }

        public ReportViewModel()
        {
            load_firstItem();

            Report_MonthChangedCommand = new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report_LoadToView(p);VisButtonReport(p); });

            Report_YearChangedCommand = new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report_LoadToView(p); VisButtonReport(p); });

            LoadCbCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => {  LoadComboBox(); });

            ReportSalesCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ReportMonthWindow wd = new ReportMonthWindow(); VisButtonReport(wd);VisReport(true) ; wd.ShowDialog(); });

            ReportInventoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ReportMonthWindow wd = new ReportMonthWindow(); VisButtonReport(wd); VisReport(false); wd.ShowDialog(); });

            MonthChangedCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => { LoadToView(p); });

            YearChangedCommand = new RelayCommand<ReportWindow>((p) => { return true; }, (p) => { LoadToView(p); });

            CloseCommand=new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { CloseWd(p); });

            //Phân biệt báo cáo kinh doanh hay tồn
            //ReportCommand= new RelayCommand<ReportMonthWindow>((p) => { return true; }, (p) => { Report_Sales(p); });
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
            if(VisListView_Sales)
                ReportSales_LoadToView(selectedYear, selectedMonth);
            if (VisListView_Inventory)
                ReportInventory_LoadToView(selectedYear, selectedMonth);
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
                int amountOfTurn =0;
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

        public void ReportInventory_LoadToView(string selectedYear, string selectedMonth)
        {
            ListInventory = new ObservableCollection<ListInventory>();

            int i = 1;

            var list_Supplies = DataProvider.Ins.DB.SUPPLIES;
            if(list_Supplies!=null)
            {
                foreach (var item in list_Supplies)
                {

                    ListInventory listInventory = new ListInventory();
                    int report_Year = int.Parse(selectedYear);
                    int report_Month = int.Parse(selectedMonth);
                    int Tondau = 0;
                    int TonCuoi = 0;
                    int PhatSinh = 0;
                    int Sudung = 0;
                    int temp = (int)item.Supplies_Amount;

                    for (int a = DateTime.Now.Year; a >= report_Year; a--)
                    {
                        for (int b = DateTime.Now.Month; b >= report_Month; b--)
                        {
                            TonCuoi = temp;
                            var import_Good = DataProvider.Ins.DB.IMPORT_GOODS.Where(x => x.ImportGoods_Date.Year == a && x.ImportGoods_Date.Month == b);
                            if(import_Good!=null)
                            {
                                foreach (var item1 in import_Good)
                                {
                                    var import_Good_Detail = DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Where(x => x.IdImportGood == item1.ImportGoods_Id && x.IdSupplies == item.Supplies_Id);
                                    if (import_Good_Detail != null)
                                        PhatSinh += (int)import_Good_Detail.Sum(x => x.Amount);
                                }
                            }
                           
                            var repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.RepairDate.Year == a && x.RepairDate.Month == b);
                            if(repair!=null)
                            {
                                foreach (var item2 in repair)
                                {
                                    var repair_Detail = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == item2.Repair_Id && x.IdSupplies == item.Supplies_Id);
                                    if (repair_Detail != null)
                                        Sudung += (int)repair_Detail.Sum(x => (int)x.SuppliesAmount);
                                }
                            }
                            Tondau = TonCuoi - PhatSinh + Sudung;
                            temp = Tondau;
                        }
                    }
                    listInventory.STT = i;
                    listInventory.Supplies_Name = item.Supplies_Name;
                    listInventory.TonDau = Tondau;
                    listInventory.TonCuoi = TonCuoi;
                    listInventory.PhatSinh = PhatSinh;
                    ListInventory.Add(listInventory);
                    i++;

                }
            }
           
        }

        public void CloseWd(ReportMonthWindow p)
        {
            p.Close();
        }
       
        public void Report_Sales(ReportMonthWindow p)
        {

                    if (MessageBox.Show("Bạn có chắc chắn muốn lập báo cáo", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
                        int selectedYear = Int32.Parse(tmp[1]);
                        string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
                        int selectedMonth = Int32.Parse(tmp1[1]);

                        DateTime date=new DateTime(selectedYear,selectedMonth,1);
                       

                        DataProvider.Ins.DB.SALES_REPORT.Add(new SALES_REPORT { SalesReport_Date = date, SalesReport_Revenue = TotalMoney });

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
        public bool check(ReportMonthWindow p)
        {
            string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
            int selectedYear = Int32.Parse(tmp[1]);
            string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
            int selectedMonth = Int32.Parse(tmp1[1]);

            var a = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year== selectedYear&& x.SalesReport_Date.Month == selectedMonth);
           
            if (DateTime.Now.Year > selectedYear || DateTime.Now.Year == selectedYear && DateTime.Now.Month > selectedMonth)
            {
                if(a.Count()==0)
                    return true;
            }
                
            return false;
        }
        public void VisButtonReport(ReportMonthWindow p)
        {
            if(check(p))
            {
                VisView = true;
            }
            else
            {
                VisView = false;
            }
        }
        public void VisReport(bool check)
        {
            if(check)
            {
                ReportName = "Báo cáo kinh doanh";
                VisListView_Sales = true;
                VisListView_Inventory = false;
            }
            else
            {
                ReportName = "Báo cáo tồn";
                VisListView_Sales = false;
                VisListView_Inventory = true;
            }
        }

    }
}
