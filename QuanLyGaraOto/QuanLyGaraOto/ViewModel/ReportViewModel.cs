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
    class ReportViewModel : BaseViewModel
    {
        public ICommand YearChangedCommand { get; set; }
        public ICommand MonthChangedCommand { get; set; }
        public ICommand LoadCbCommand { get; set; }
        public ICommand ReportSalesCommand { get; set; }
        public ICommand ReportInventoryCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        private string first_item_year { get; set; }
        public string First_item_year { get => first_item_year; set { first_item_year = value; OnPropertyChanged(); } }
        private string first_item_month { get; set; }
        public string First_item_month { get => first_item_month; set { first_item_month = value; OnPropertyChanged(); } }

        private ObservableCollection<string> itemSource_Year = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSource_Year { get => itemSource_Year; set { itemSource_Year = value; OnPropertyChanged(); } }

        private ObservableCollection<string> itemSource_Month = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSource_Month { get => itemSource_Month; set { itemSource_Month = value; OnPropertyChanged(); } }
     
        private ObservableCollection<ListReceipt> _List;
        public ObservableCollection<ListReceipt> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<ImportTemp> _ListImport;
        public ObservableCollection<ImportTemp> ListImport { get => _ListImport; set { _ListImport = value; OnPropertyChanged(); } }

        bool _isReport = false;
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }


        private string _LicensePlate { get; set; }
        public string LicensePlate { get => _LicensePlate; set { _LicensePlate = value; OnPropertyChanged(); } }

        private string _Customer_Name { get; set; }
        public string Customer_Name { get => _Customer_Name; set { _Customer_Name = value; OnPropertyChanged(); } }

        private USER _User { get; set; }
        public USER User { get => _User; set { _User = value; OnPropertyChanged(); } }

        private bool _IsSelectedTabNhapHang { get; set; }
        public bool IsSelectedTabNhapHang { get => _IsSelectedTabNhapHang; set { _IsSelectedTabNhapHang = value; OnPropertyChanged(); } }

        private  bool _IsSelectedTabKinhDoanh { get; set; }
        public bool IsSelectedTabKinhDoanh { get => _IsSelectedTabKinhDoanh; set { _IsSelectedTabKinhDoanh = value; OnPropertyChanged(); } }

        public ReportViewModel(bool role, USER u) : this()
        {
            isReport = role;
            User = u;
        }
        public ReportViewModel()
        {
            load_firstItem();
            LoadComboBox();
            LoadCbCommand = new RelayCommand<ReportWindow>(
                (p) => { return true; },
                (p) =>
                {
                    LoadComboBox();
                });

            ReportSalesCommand = new RelayCommand<object>(
                (p) => { return true; }, 
                (p) => 
                {
                    ReportMonthWindow wd = new ReportMonthWindow(true,User);
                    wd.ShowDialog();
                });

            ReportInventoryCommand = new RelayCommand<object>(
                (p) => { return true; }, 
                (p) => 
                { 
                    ReportMonthWindow wd = new ReportMonthWindow(false,User);
                    wd.ShowDialog();
                });

            MonthChangedCommand = new RelayCommand<ReportWindow>(
                (p) => { return true; },
                (p) => 
                { 
                    LoadToView(p);
                });

            YearChangedCommand = new RelayCommand<ReportWindow>(
                (p) => { return true; },
                (p) => 
                { 
                    LoadToView(p); 
                });
            ExportCommand = new RelayCommand<ReportWindow>(
                (p) => { return true; },
                (p) =>
                {
                    PrintViewModel printViewModel = new PrintViewModel();
                    if (IsSelectedTabKinhDoanh)
                    {
                        printViewModel.XuatLichSuKinhDoanh(List);
                    }else if (IsSelectedTabNhapHang)
                    {
                        printViewModel.XuatLichSuNhapHang(ListImport);
                    }
                });
        }

        //Load combobox-selected item khi khởi tạo
        public void load_firstItem()
        {
            First_item_year = "Năm " + DateTime.Now.Year;
            ItemSource_Year.Add(First_item_year);
            First_item_month = "Tháng " + DateTime.Now.Month;
            ItemSource_Month.Add(First_item_month);
            string selectedYear = DateTime.Now.Year.ToString();
            string selectedMonth = DateTime.Now.Month.ToString();
            Load_KinhDoanh(selectedYear, selectedMonth);
            Load_NhapKho(selectedYear, selectedMonth);
        }
        //Load ListView doanh thu-nhập kho
        public void LoadToView(ReportWindow p)
        {
            string[] tmp = p.cb_SelectYear.SelectedValue.ToString().Split(' ');
            string selectedYear = tmp[1];
            string[] tmp1 = p.cb_SelectMonth.SelectedValue.ToString().Split(' ');
            string selectedMonth = tmp1[1];
            Load_KinhDoanh(selectedYear, selectedMonth);
            Load_NhapKho(selectedYear, selectedMonth);           
        }

        //Thêm dữ liệu vào list kinh doanh
        public void Load_KinhDoanh(string selectedYear,string selectedMonth)
        {
            List = new ObservableCollection<ListReceipt>();
            
            var receipt = DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Year.ToString() == selectedYear && x.ReceiptDate.Month.ToString() == selectedMonth);
            foreach (var item in receipt)
            {
                ListReceipt temp = new ListReceipt();
                var reception = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == item.IdReception).SingleOrDefault();

                var customer = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.Customer_Id == reception.IdCustomer).SingleOrDefault();

                temp.Customer_Name = customer.Customer_Name;
                temp.LicensePlate = reception.LicensePlate;
                temp.Receipt = item;

                List.Add(temp);
            }

            
        }

        //Thêm dữ liệu vào list nhập kho
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

        //Load combobox
        public void LoadComboBox()
        {
            ObservableCollection<RECEIPT> temp = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            foreach (var item in temp)
            {
                if(item.ReceiptDate.Year!=DateTime.Now.Year)
                {
                    ItemSource_Year.Add("Năm " + item.ReceiptDate.Year.ToString());                    
                }          
            }
            for(int i=1;i<13;i++)
                if (DateTime.Now.Month!=i)
                {
                    ItemSource_Month.Add("Tháng " + i);                  
                }
        }   
    }
}
