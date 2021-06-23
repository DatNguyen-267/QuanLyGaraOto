using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class CarServiceViewModel:BaseViewModel
    {
        public bool IsRecepted { get; set; }
        private bool _VisPay { get; set; }
        public bool VisPay { get=>_VisPay; set { _VisPay = value; OnPropertyChanged(); } }
        private bool _VisOption { get; set; }
        public bool VisOption { get => _VisOption; set { _VisOption = value; OnPropertyChanged(); } }
        private bool _VisChangeCustomerInfo { get; set; }
        public bool VisChangeCustomerInfo { get => _VisChangeCustomerInfo; set { _VisChangeCustomerInfo = value; OnPropertyChanged(); } }
        private bool _VisView { get; set; }
        public bool VisView { get => _VisView; set { _VisView = value; OnPropertyChanged(); } }
        private bool _VisAdd { get; set; }
        public bool VisAdd { get => _VisAdd; set { _VisAdd = value; OnPropertyChanged(); } }
        private bool _VisEdit { get; set; }
        public bool VisEdit { get => _VisEdit; set { _VisEdit = value; OnPropertyChanged(); } }
        private bool _VisDelete { get; set; }
        public bool VisDelete { get => _VisDelete; set { _VisDelete = value; OnPropertyChanged(); } }
        private bool _IsEnabledReceipt { get; set; }
        public bool IsEnabledReceipt { get => _IsEnabledReceipt; set { _IsEnabledReceipt = value; OnPropertyChanged(); } }
        
        #region Visibility
        private bool _VisReceptionCard { get; set; }
        public bool VisReceptionCard { get => _VisReceptionCard; set { _VisReceptionCard = value; OnPropertyChanged(); } }
        private bool _VisInfo { get; set; }
        public bool VisInfo { get => _VisInfo; set { _VisInfo = value; OnPropertyChanged(); } }
        private bool _VisAddRepairCard { get; set; }
        public bool VisAddRepairCard { get => _VisAddRepairCard; set { _VisAddRepairCard = value; OnPropertyChanged(); } }
        private bool _VisRepairServiceCard { get; set; }
        public bool VisRepairServiceCard { get => _VisRepairServiceCard; set { _VisRepairServiceCard = value; OnPropertyChanged(); } }
        
        private bool _IsEnabledAddRepairBtn { get; set; }
        public bool IsEnabledAddRepairBtn { get => _IsEnabledAddRepairBtn; set { _IsEnabledAddRepairBtn = value; OnPropertyChanged(); } }
        private bool _IsEnableReceptionBtn { get; set; }
        public bool IsEnableReceptionBtn { get => _IsEnableReceptionBtn; set { _IsEnableReceptionBtn = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand ReceptionCommand { get; set; }
        public ICommand AddRepairCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand UpdateValue { get; set; }
        public ICommand ChangeCarInfoCommand { get; set; }
        public ICommand ReceiptCommand { get; set; }
        public ICommand ViewCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SelectionChanged { get; set; }
        #endregion

        #region Data Reception
        private ObservableCollection<CAR_BRAND> _ListBrand { get; set; }
        public ObservableCollection<CAR_BRAND> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
        private string _Brand { get; set; }
        public string Brand { get => _Brand; set { _Brand = value; OnPropertyChanged(); } }
        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _LicensePlate { get; set; }
        public string LicensePlate { get => _LicensePlate; set { _LicensePlate = value; OnPropertyChanged(); } }
        private string _Address { get; set; }
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
        private string _Phone { get; set; }
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private DateTime? _ReceptionDate { get; set; }
        public DateTime? ReceptionDate { get => _ReceptionDate; set { _ReceptionDate = value; OnPropertyChanged(); } }
        #endregion

        #region Data Repair

        private ObservableCollection<RepairItem> _ListRepair { get; set; }
        public ObservableCollection<RepairItem> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        private ObservableCollection<SUPPLIES> _ListSupply { get; set; }
        public ObservableCollection<SUPPLIES> ListSupply { get => _ListSupply; set { _ListSupply = value; OnPropertyChanged(); } }
        private ObservableCollection<WAGE> _ListPay { get; set; }
        public ObservableCollection<WAGE> ListPay { get => _ListPay; set { _ListPay = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _ListAmount { get; set; }
        public ObservableCollection<int> ListAmount { get => _ListAmount; set { _ListAmount = value; OnPropertyChanged(); } }
        private ObservableCollection<RepairItem> _Temp { get; set; }
        public ObservableCollection<RepairItem> Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }
        // Danh sach cac List

        private RepairItem _SelectedItem { get; set; }
        public RepairItem SelectedItem { get => _SelectedItem; 
            set { 
                _SelectedItem = value;
                OnPropertyChanged(); } }
        private DateTime? _RepairDate { get; set; }
        public DateTime? RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }

        #endregion

        #region Important Data
        private CarServiceWindow _carServiceWindow { get; set; }
        public CarServiceWindow carServiceWindow { get => _carServiceWindow; set { _carServiceWindow = value; } }
        private REPAIR _RepairForm { get; set; }
        public REPAIR RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private RECEPTION _CarReception { get; set; }
        public RECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private int _Total { get; set; }
        public int Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }
        #endregion
        public CarServiceViewModel()
        {
            GenaralFunction();
            VisPay = false;
        }
        public CarServiceViewModel(RECEPTION carReception)
        {
            GenaralFunction();
            this.CarReception = DataProvider.Ins.DB.RECEPTIONs.Where(x=>x.Reception_Id == carReception.Reception_Id).SingleOrDefault();
            // base data
            
            LoadCarInfo(this.CarReception.Reception_Id);
            // Load thông tin xe 

            VisReption(true);
            // Hiện bảng thông tin và ẩn nút tiếp nhận xe

            if (DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == this.CarReception.Reception_Id).Count() > 0)
            {
                VisRepair(true);
                LoadRepairInfo(this.CarReception.Reception_Id);
                RepairDate = RepairForm.RepairDate;
                if (DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == RepairForm.Repair_Id).Count() == 0)
                {
                    IsEnabledReceipt = false;
                }
            }
            else EnableRepairBtn(true);
            // Kiểm tra bảng thông tin sửa chữa

            UpdateTotal();
            // Tính tổng tiền

            if (DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == CarReception.Reception_Id).Count() > 0)
            {
                VisPay = false;
                VisView = true;
                VisAdd = false;
                VisEdit = false;
                VisDelete = false;
                VisChangeCustomerInfo = false;
                VisOption = false;
            }
            

        }
        public void GenaralFunction()
        {
            InitData();
            InitVis();
            InitBtn();

            ListBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            ListPay = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
            ListSupply = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
            ListRepair = new ObservableCollection<RepairItem>();
            ListAmount = new ObservableCollection<int>();
            for (int i = 1; i < 100; i++)
            {
                ListAmount.Add(i);
            }
            ReceptionCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    CarReceptionWindow carReceptionWindow = new CarReceptionWindow();
                    carReceptionWindow.ShowDialog();
                    CarReceptionViewModel backData = (carReceptionWindow.DataContext as CarReceptionViewModel);
                    if (backData.IsSuccess)
                    {
                        VisReption(true);
                        EnableRepairBtn(true);
                        LoadCarInfo(backData.IdNew);
                        IsRecepted = true;
                    };
                });
            AddRepairCommand = new RelayCommand<Object>(
                (p) => {
                    if (IsEnabledAddRepairBtn) return true;
                    return false;
                },
                (p) =>
                {
                    AddRepairFormWindow addRepairFormWindow = new AddRepairFormWindow(CarReception);
                    addRepairFormWindow.ShowDialog();
                    AddRepairFormViewModel addRepairFormViewModel = (addRepairFormWindow.DataContext as AddRepairFormViewModel);
                    if (addRepairFormViewModel.IsSuccess)
                    {
                        AddRepairForm(addRepairFormViewModel.NewRepairForm);
                        VisRepair(true);
                        RepairDate = addRepairFormViewModel.NewRepairForm.RepairDate;
                    }
                });
            AddCommand = new RelayCommand<Object>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    AddRepairDetailWindow addRepairDetailWindow = new AddRepairDetailWindow(RepairForm);
                    addRepairDetailWindow.ShowDialog();
                    AddRepairDetailViewModel addRepairDetailViewModel = (addRepairDetailWindow.DataContext as AddRepairDetailViewModel);

                    REPAIR_DETAIL returnRepairDetail = addRepairDetailViewModel.ReturnRepairDetail;
                    if (returnRepairDetail!=null)
                    {
                        ListRepair.Add(new RepairItem() { RepairInfo = returnRepairDetail,
                            TotalMoney = returnRepairDetail.TotalMoney ,
                            STT = ListRepair.Count()+1 ,

                        });

                        UpdateTotal();
                        DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == CarReception.Reception_Id).SingleOrDefault().Debt = Total;
                        DataProvider.Ins.DB.SaveChanges();

                        if (VisPay == false) VisPay = true;
                        IsEnabledReceipt = true;
                    }
                });
            EditCommand = new RelayCommand<Object>(
                (p) => {
                    if (SelectedItem == null) return false;
                    return true;
                },
                (p) =>
                {
                    AddRepairDetailWindow addRepairDetailWindow = new AddRepairDetailWindow(SelectedItem.RepairInfo);
                    addRepairDetailWindow.ShowDialog();
                    AddRepairDetailViewModel addRepairDetailViewModel = (addRepairDetailWindow.DataContext as AddRepairDetailViewModel);

                    REPAIR_DETAIL returnRepairDetail = addRepairDetailViewModel.ReturnRepairDetail;
                    if (returnRepairDetail != null)
                    {
                        SelectedItem.RepairInfo = returnRepairDetail;
                        UpdateTotal();
                        DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == CarReception.Reception_Id).SingleOrDefault().Debt = Total;
                        DataProvider.Ins.DB.SaveChanges();

                        if (VisPay == false) VisPay = true;
                        
                    }
                });
            DeleteCommand = new RelayCommand<Object>(
                (p) => {
                    if (SelectedItem == null) return false;
                    if (DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.RepairDetail_Id == SelectedItem.RepairInfo.RepairDetail_Id).SingleOrDefault() == null)
                        return false;
                    return true;
                },
                (p) =>
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn xóa những dữ liệu bạn đã chọn", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        foreach (var item in Temp)
                        {
                            if (item.RepairInfo.IdSupplies != null)
                            {
                                var temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == item.RepairInfo.IdSupplies).SingleOrDefault();
                                temp.Supplies_Amount = temp.Supplies_Amount + item.RepairInfo.SuppliesAmount;
                                DataProvider.Ins.DB.SaveChanges();
                            }

                            DeleteModel deleteModel = new DeleteModel();
                            deleteModel.RepairInfo(item.RepairInfo);

                            LoadRepairInfo();
                            UpdateTotal();
                            DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == CarReception.Reception_Id).SingleOrDefault().Debt = Total;
                            DataProvider.Ins.DB.SaveChanges();

                            if (ListRepair.Count() == 0)
                            {
                                IsEnabledReceipt = false;
                            }
                        }
                    }
                });

            ChangeCarInfoCommand = new RelayCommand<Object>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    ChangeCarInfoWindow changeCarInfoWindow = new ChangeCarInfoWindow(CarReception);
                    changeCarInfoWindow.ShowDialog();

                    ChangeCarInfoViewModel changeCarInfoViewModel = (changeCarInfoWindow.DataContext as ChangeCarInfoViewModel);
                    CarReception = changeCarInfoViewModel.CarReception;
                    // RepairDate

                });
            ReceiptCommand = new RelayCommand<Object>(
                (p) => {
                    if (RepairForm == null || CarReception == null) return false;
                    if (DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == RepairForm.Repair_Id).Count() > 0) return true;
                        return true;
                },
                (p) =>
                {
                    PayWindow payWindow = new PayWindow(CarReception);
                    payWindow.ShowDialog();

                    PayViewModel payViewModel = (payWindow.DataContext as PayViewModel);
                    VisPay = !payViewModel.IsPay;
                    VisView = payViewModel.IsPay;
                    VisAdd = !payViewModel.IsPay;
                    VisEdit = !payViewModel.IsPay;
                    VisDelete = !payViewModel.IsPay;
                    VisChangeCustomerInfo = !payViewModel.IsPay;
                    VisOption = !payViewModel.IsPay;
                });
            ViewCommand = new RelayCommand<Object>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    PayWindow payWindow = new PayWindow(CarReception);
                    payWindow.ShowDialog();
                });
            CloseCommand = new RelayCommand<Window>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        p.Close();
                    
                });
            SelectionChanged = new RelayCommand<DataGrid>
                ((p) =>
                {
                    return true;
                }, (p) =>
                {
                    Temp = new ObservableCollection<RepairItem>(p.SelectedItems.Cast<RepairItem>().ToList());
                }
            );
        }

        public void UpdateTotal()
        {
            Total = 0;
            foreach (var item in ListRepair)
            {
                Total = Total + (int)item.RepairInfo.TotalMoney;
            }
        }
        public void InitData()
        {
            IsRecepted = false;
            Total = 0;
        }
        public void InitBtn()
        {
            IsEnabledAddRepairBtn = false;
            IsEnableReceptionBtn = true;
        }
        public void InitVis()
        {
            IsEnabledReceipt = true;
            VisReceptionCard = true;
            VisAddRepairCard = true;
            VisInfo = false;
            VisRepairServiceCard = false;
            VisPay = true;
            VisView = false;
            VisAdd = true;
            VisEdit = true;
            VisDelete = true;
            VisChangeCustomerInfo = true;
            VisOption = true;
        }
        public void EnableReceptionBtn(bool temp)
        {
            if (temp) IsEnableReceptionBtn = true;
            else IsEnableReceptionBtn = false;
        }
        public void EnableRepairBtn (bool temp)
        {
            if (temp) IsEnabledAddRepairBtn = true;
            else IsEnabledAddRepairBtn = false;
        } 
        public void VisReption(bool Vis)
        {
            if (Vis)
            {
                VisReceptionCard = false;
                VisInfo = true;
            }
        }
        public void VisRepair(bool Vis)
        {
            if (Vis)
            {
                VisAddRepairCard = false;
                VisRepairServiceCard = true;
            }
        }
        public void LoadCarInfo(int IdNewCar)
        {
            CarReception = new RECEPTION();
            CarReception = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == IdNewCar).SingleOrDefault();
            Name = CarReception.CUSTOMER.Customer_Name;
            Address = CarReception.CUSTOMER.Customer_Address;
            Phone = CarReception.CUSTOMER.Customer_Phone;
            LicensePlate = CarReception.LicensePlate;
            ReceptionDate = CarReception.ReceptionDate;
            Brand = CarReception.CAR_BRAND.CarBrand_Name;
        }
        public void LoadRepairInfo(int ID)
        {
            RepairForm = DataProvider.Ins.DB.REPAIRs.Where(x=> x.IdReception == ID).SingleOrDefault();
            var repairInfo = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == RepairForm.Repair_Id);
            int i = 1;
            foreach (var item in repairInfo)
            {
               
                RepairItem repairItem = new RepairItem();
                repairItem.STT = i++;
                repairItem.RepairInfo = item;
                repairItem.TotalMoney = item.TotalMoney;
                ListRepair.Add(repairItem);
            }
        }
        public void LoadRepairInfo()
        {
            ListRepair = new ObservableCollection<RepairItem>();
            var repairInfo = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == RepairForm.Repair_Id);
            int i = 1;
            foreach (var item in repairInfo)
            {

                RepairItem repairItem = new RepairItem();
                repairItem.STT = i++;
                repairItem.RepairInfo = item;
                repairItem.TotalMoney = item.TotalMoney;
                ListRepair.Add(repairItem);
            }
        }
        public void AddRepairForm(REPAIR newRepairForm)
        {
            newRepairForm.IdReception = CarReception.Reception_Id;
            DataProvider.Ins.DB.REPAIRs.Add(newRepairForm);
            DataProvider.Ins.DB.SaveChanges();
            RepairForm = newRepairForm;
        }
    }
}
