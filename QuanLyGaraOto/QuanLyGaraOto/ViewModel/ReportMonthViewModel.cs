using QuanLyGaraOto.Model;
using QuanLyGaraOto.Template;
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
    public class ReportMonthViewModel:BaseViewModel
    {
        public ICommand ReportCommand { get; set; }

        public ICommand Report_YearChangedCommand { get; set; }
        public ICommand Report_MonthChangedCommand { get; set; }

        public ICommand ViewReportCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private ObservableCollection<ListSales> _ListSales;
        public ObservableCollection<ListSales> ListSales { get => _ListSales; set { _ListSales = value; OnPropertyChanged(); } }

        private ObservableCollection<ListInventory> _ListInventory;
        public ObservableCollection<ListInventory> ListInventory { get => _ListInventory; set { _ListInventory = value; OnPropertyChanged(); } }

        private SalesReport _SalesReport;
        public SalesReport SalesReport { get => _SalesReport; set { _SalesReport = value; OnPropertyChanged(); } }

        private InventoryReport _InventoryReport;
        public InventoryReport InventoryReport { get => _InventoryReport; set { _InventoryReport = value; OnPropertyChanged(); } }

        private string first_item_year { get; set; }
        public string First_item_year { get => first_item_year; set { first_item_year = value; OnPropertyChanged(); } }
        private string first_item_month { get; set; }
        public string First_item_month { get => first_item_month; set { first_item_month = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _RpitemSource_Year = new ObservableCollection<string>();
        public ObservableCollection<string> RpItemSource_Year { get => _RpitemSource_Year; set { _RpitemSource_Year = value; OnPropertyChanged(); } }

        private ObservableCollection<string> RpitemSource_Month = new ObservableCollection<string>();
        public ObservableCollection<string> RpItemSource_Month { get => RpitemSource_Month; set { RpitemSource_Month = value; OnPropertyChanged(); } }


        private int _TotalMoney;

        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private string _ReportName { get; set; }
        public string ReportName { get => _ReportName; set { _ReportName = value; OnPropertyChanged(); } }
        private bool _VisView { get; set; }
        public bool VisView { get => _VisView; set { _VisView = value; OnPropertyChanged(); } }

        private bool _VisViewReport { get; set; }
        public bool VisViewReport { get => _VisViewReport; set { _VisViewReport = value; OnPropertyChanged(); } }
        private bool _VisPrint { get; set; }
        public bool VisPrint { get => _VisPrint; set { _VisPrint = value; OnPropertyChanged(); } }
        private bool _VisListView_Sales { get; set; }
        public bool VisListView_Sales { get => _VisListView_Sales; set { _VisListView_Sales = value; OnPropertyChanged(); } }

        private bool _VisListView_Inventory { get; set; }
        public bool VisListView_Inventory { get => _VisListView_Inventory; set { _VisListView_Inventory = value; OnPropertyChanged(); } }

        public ReportMonthViewModel() { }
        public ReportMonthViewModel(bool check,USER User)
        {
            load_firstItem(check);
            LoadComboBox();
            VisReport(check);
            ReportCommand = new RelayCommand<ReportMonthWindow>(
                (p) => { return true; },
                (p) =>
                {
                    if (!check)
                        Report_Inventory(p);
                    if (check)
                        Report_Sales(p);
                });

            Report_MonthChangedCommand = new RelayCommand<ReportMonthWindow>(
              (p) => { return true; },
              (p) =>
              {
                  VisButtonPrint(p);
                  VisButtonReport(p);
                  Report_LoadToView(p);

              });

            Report_YearChangedCommand = new RelayCommand<ReportMonthWindow>(
                (p) => { return true; },
                (p) =>
                {
                    VisButtonPrint(p);
                    VisButtonReport(p);
                    Report_LoadToView(p);
                });
            CloseCommand = new RelayCommand<ReportMonthWindow>(
               (p) => { return true; },
               (p) =>
               {
                   CloseWd(p);
               });
            ViewReportCommand = new RelayCommand<ReportMonthWindow>(
                (p) => { return true; },
                (p) =>
                {
                    
                    string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
                    string selectedYear = tmp[1];
                    string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
                    string selectedMonth = tmp1[1];
                    var uInfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == User.Users_Id).SingleOrDefault();
       
                    if (VisListView_Inventory)
                    {
                        InventoryReport = new InventoryReport();
                        var inventoryReport = DataProvider.Ins.DB.INVENTORY_REPORT.Where(x => x.InventoryReport_Date.Year.ToString() == selectedYear && x.InventoryReport_Date.Month.ToString() == selectedMonth).SingleOrDefault();
                        InventoryReport.uSER_INFO = uInfo;
                        InventoryReport.ReportDate = selectedMonth + "/" + selectedYear;
                        InventoryReport.IdReport = inventoryReport.InventoryReport_Id;
                        InventoryReport.ListInventory = ListInventory;
                        InventoryReportTemplate wd = new InventoryReportTemplate(InventoryReport);
                        wd.ShowDialog();
                    }
                    if (VisListView_Sales)
                    {
                        SalesReport = new SalesReport();
                        SalesReport.uSER_INFO = uInfo;
                        SalesReport.ReportDate = selectedMonth + "/" + selectedYear;
                        var salesReport = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year.ToString() == selectedYear && x.SalesReport_Date.Month.ToString() == selectedMonth).SingleOrDefault();
                        SalesReport.IdReport = salesReport.SalesReport_Id;
                        SalesReport.ListSales = ListSales;
                        SalesReport.TotalMoney = TotalMoney;
                        ReportSalesTemplate wd = new ReportSalesTemplate(SalesReport);
                        wd.ShowDialog();

                    }
                });
            PrintReportCommand=new RelayCommand<ReportMonthWindow>(
                (p) => { return true; },                  
                (p) =>                
                {
                    string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
                    string selectedYear = tmp[1];
                    string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
                    string selectedMonth = tmp1[1];
                    var uInfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == User.Users_Id).SingleOrDefault();

                    if (VisListView_Inventory)
                    {
                        InventoryReport = new InventoryReport();
                        var inventoryReport = DataProvider.Ins.DB.INVENTORY_REPORT.Where(x => x.InventoryReport_Date.Year.ToString() == selectedYear && x.InventoryReport_Date.Month.ToString() == selectedMonth).SingleOrDefault();
                        InventoryReport.uSER_INFO = uInfo;
                        InventoryReport.ReportDate = selectedMonth + "/" + selectedYear;
                        InventoryReport.IdReport = inventoryReport.InventoryReport_Id;
                        InventoryReport.ListInventory = ListInventory;

                        PrintViewModel printViewModel = new PrintViewModel();
                        printViewModel.PrintInventoryReport(InventoryReport);
                    }
                    if (VisListView_Sales)
                    {
                        SalesReport = new SalesReport();
                        SalesReport.uSER_INFO = uInfo;
                        SalesReport.ReportDate = selectedMonth + "/" + selectedYear;
                        var salesReport = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year.ToString() == selectedYear && x.SalesReport_Date.Month.ToString() == selectedMonth).SingleOrDefault();
                        SalesReport.IdReport = salesReport.SalesReport_Id;
                        SalesReport.ListSales = ListSales;
                        SalesReport.TotalMoney = TotalMoney;

                        ReportSalesTemplate reportSalesTemplate = new ReportSalesTemplate(SalesReport);
                        reportSalesTemplate.Show();
                        if (SalesReport.ListSales.Count() > 9)
                        {
                            reportSalesTemplate.Height = reportSalesTemplate.Height + 35 * ((SalesReport.ListSales.Count() - 9));
                        }
                        PrintViewModel printViewModel = new PrintViewModel();
                        printViewModel.PrintSalesReport(reportSalesTemplate);
                       
                    }
                });

        }

        public void LoadComboBox()
        {
            ObservableCollection<RECEIPT> temp = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            foreach (var item in temp)
            {
                if (item.ReceiptDate.Year != DateTime.Now.Year)
                {
                   
                    RpItemSource_Year.Add("Năm " + item.ReceiptDate.Year.ToString());
                }

            }
            for (int i = 1; i < 13; i++)
                if (DateTime.Now.Month != i)
                {
                    
                    RpItemSource_Month.Add("Tháng " + i);
                }
        }
        public void load_firstItem(bool check)
        {
            First_item_year = "Năm " + DateTime.Now.Year;
            RpItemSource_Year.Add(First_item_year);
            First_item_month = "Tháng " + DateTime.Now.Month;
            RpitemSource_Month.Add(First_item_month);
            if (check)
            {
                ReportSales_LoadToView(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            }
            else
            {
                ReportInventory_LoadToView(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            }
        }
        //Load listview Doanh số-Báo cáo tồn
        public void Report_LoadToView(ReportMonthWindow p)
        {

            string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
            string selectedYear = tmp[1];
            string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
            string selectedMonth = tmp1[1];
            if (VisListView_Sales)
                ReportSales_LoadToView(selectedYear, selectedMonth);
            if (VisListView_Inventory)
                ReportInventory_LoadToView(selectedYear, selectedMonth);
        }

        //Thêm dữ liệu vào list Doanh số
        public void ReportSales_LoadToView(string selectedYear, string selectedMonth)
        {
            ListSales = new ObservableCollection<ListSales>();

            int i = 1;

            TotalMoney = 0;
            var a = DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Year.ToString() == selectedYear && x.ReceiptDate.Month.ToString() == selectedMonth);
            foreach (var item in a)
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

                var ReceptionList = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.IdCarBrand == item.CarBrand_Id);
                if (ReceptionList != null)
                {
                    foreach (var temp in ReceptionList)
                    {

                        var ReceiptList = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == temp.Reception_Id && x.ReceiptDate.Year.ToString() == selectedYear && x.ReceiptDate.Month.ToString() == selectedMonth).SingleOrDefault();
                        if (ReceiptList != null)
                        {

                            amountOfTurn++;
                            totalMoney += ReceiptList.MoneyReceived;


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
            if (list_Supplies != null)
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
                            if (import_Good != null)
                            {
                                foreach (var item1 in import_Good)
                                {
                                    var import_Good_Detail = DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Where(x => x.IdImportGood == item1.ImportGoods_Id && x.IdSupplies == item.Supplies_Id).SingleOrDefault();
                                    if (import_Good_Detail != null)
                                        PhatSinh += (int)import_Good_Detail.Amount;
                                }
                            }

                            var repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.RepairDate.Year == a && x.RepairDate.Month == b);
                            if (repair != null)
                            {
                                foreach (var item2 in repair)
                                {
                                    foreach (var item3 in DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == item2.Repair_Id && x.IdSupplies == item.Supplies_Id))
                                    {
                                         if (item3 != null)
                                            Sudung += (int)item3.SuppliesAmount;
                                    } 
                                    
                                }
                            }
                            Tondau = TonCuoi - PhatSinh + Sudung;
                            temp = Tondau;
                        }
                    }
                    listInventory.STT = i;
                    listInventory.Supplies_Name = item.Supplies_Name;
                    listInventory.IdSupplies = item.Supplies_Id;
                    listInventory.TonDau = Tondau;
                    listInventory.TonCuoi = TonCuoi;
                    listInventory.PhatSinh = PhatSinh;
                    ListInventory.Add(listInventory);
                    i++;

                }
            }

        }


        //Đóng wd
        public void CloseWd(ReportMonthWindow p)
        {
            p.Close();
        }

        //Lập báo cáo Doanh số
        public void Report_Sales(ReportMonthWindow p)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn lập báo cáo Doanh số", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
                int selectedYear = Int32.Parse(tmp[1]);
                string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
                int selectedMonth = Int32.Parse(tmp1[1]);

                DateTime date = new DateTime(selectedYear, selectedMonth, 1);


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

                MessageBox.Show("Lập báo cáo thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            }


        }
        //Lập báo cáo Tồn
        public void Report_Inventory(ReportMonthWindow p)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn lập báo cáo Tồn", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
                int selectedYear = Int32.Parse(tmp[1]);
                string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
                int selectedMonth = Int32.Parse(tmp1[1]);

                DateTime date = new DateTime(selectedYear, selectedMonth, 1);


                DataProvider.Ins.DB.INVENTORY_REPORT.Add(new INVENTORY_REPORT { InventoryReport_Date = date });
                DataProvider.Ins.DB.SaveChanges();
                int i = 0;
                var inventoryReport = DataProvider.Ins.DB.INVENTORY_REPORT;
                foreach (var item in inventoryReport)
                {
                    if (item.InventoryReport_Id > i)
                        i = item.InventoryReport_Id;
                }

                foreach (var item in ListInventory)
                {
                    INVENTORY_REPORT_DETAIL inventoryReportDetail = new INVENTORY_REPORT_DETAIL();
                    inventoryReportDetail.IdInventoryReport = i;
                    inventoryReportDetail.IdSupplies = item.IdSupplies;
                    inventoryReportDetail.TonDau = item.TonDau;
                    inventoryReportDetail.TonCuoi = item.TonCuoi;
                    inventoryReportDetail.PhatSinh = item.PhatSinh;
                    DataProvider.Ins.DB.INVENTORY_REPORT_DETAIL.Add(inventoryReportDetail);
                    DataProvider.Ins.DB.SaveChanges();

                }

                MessageBox.Show("Lập báo cáo thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                p.Close();
            }


        }
        public bool check_Report(ReportMonthWindow p)
        {
            string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
            int selectedYear = Int32.Parse(tmp[1]);
            string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
            int selectedMonth = Int32.Parse(tmp1[1]);

            var a = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year == selectedYear && x.SalesReport_Date.Month == selectedMonth);
            var b = DataProvider.Ins.DB.INVENTORY_REPORT.Where(x => x.InventoryReport_Date.Year == selectedYear && x.InventoryReport_Date.Month == selectedMonth);

            if (VisListView_Inventory)
            {
                if (DateTime.Now.Year > selectedYear || DateTime.Now.Year == selectedYear && DateTime.Now.Month > selectedMonth)
                {
                    if (b.Count() == 0)
                        return true;
                }
            }
            if (VisListView_Sales)
            {
                if (DateTime.Now.Year > selectedYear || DateTime.Now.Year == selectedYear && DateTime.Now.Month > selectedMonth)
                {
                    if (a.Count() == 0)
                        return true;
                }
            }


            return false;
        }
        public void VisButtonReport(ReportMonthWindow p)
        {
            if (check_Report(p))
            {
                VisView = true;
            }
            else
            {
                VisView = false;
            }
        }

        public bool check_Print(ReportMonthWindow p)
        {
            string[] tmp = p.Rpcb_SelectYear.SelectedValue.ToString().Split(' ');
            int selectedYear = Int32.Parse(tmp[1]);
            string[] tmp1 = p.Rpcb_SelectMonth.SelectedValue.ToString().Split(' ');
            int selectedMonth = Int32.Parse(tmp1[1]);

            var a = DataProvider.Ins.DB.SALES_REPORT.Where(x => x.SalesReport_Date.Year == selectedYear && x.SalesReport_Date.Month == selectedMonth);
            var b = DataProvider.Ins.DB.INVENTORY_REPORT.Where(x => x.InventoryReport_Date.Year == selectedYear && x.InventoryReport_Date.Month == selectedMonth);

            if (VisListView_Inventory)
            {
                if (b.Count() != 0)
                    return true;
            }
            if (VisListView_Sales)
            {
                if (a.Count() != 0)
                    return true;
            }

            return false;
        }

        public void VisButtonPrint(ReportMonthWindow p)
        {
            if (check_Print(p))
            {
                VisPrint = true;
                VisViewReport = true;
            }
            else
            {
                VisPrint = false;
                VisViewReport = false;
            }
        }

        public void VisReport(bool check)
        {
            if (check)
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
