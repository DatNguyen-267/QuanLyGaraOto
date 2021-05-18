using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class CarServiceViewModel:BaseViewModel
    {
        public bool IsRecepted { get; set; }
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
        #endregion

        #region Data Reception
        private ObservableCollection<CARBRAND> _ListBrand { get; set; }
        public ObservableCollection<CARBRAND> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
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
        private ObservableCollection<PAY> _ListPay { get; set; }
        public ObservableCollection<PAY> ListPay { get => _ListPay; set { _ListPay = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _ListAmount { get; set; }
        public ObservableCollection<int> ListAmount { get => _ListAmount; set { _ListAmount = value; OnPropertyChanged(); } }
        // Danh sach cac List

        private RepairItem _SelectedItem { get; set; }
        public RepairItem SelectedItem { get => _SelectedItem; 
            set { 
                _SelectedItem = value;
                if (SelectedItem != null)
                {
                    Content = SelectedItem.RepairInfo.Content;
                    SelectedSupply = SelectedItem.RepairInfo.SUPPLIES;
                    SelectedPay = SelectedItem.RepairInfo.PAY;
                    SelectedAmount = SelectedItem.RepairInfo.SuppliesAmount;
                    Price = SelectedItem.Price;
                    TotalMoney = SelectedItem.TotalMoney;
                } else
                {
                    Content = null;
                    SelectedSupply = null;
                    SelectedPay = null;
                    SelectedAmount = 1;
                    Price = 0;
                    TotalMoney = 0;
                }
                OnPropertyChanged(); } }
        private string _Content { get; set; }
        public string Content { get => _Content; set { _Content = value; OnPropertyChanged(); } }
        private SUPPLIES _SelectedSupply { get; set; }
        public SUPPLIES SelectedSupply { get => _SelectedSupply; set { _SelectedSupply = value; OnPropertyChanged(); } }
        private PAY _SelectedPay { get; set; }
        public PAY SelectedPay { get => _SelectedPay; set { _SelectedPay = value; OnPropertyChanged(); } }
        private Nullable<int> _SelectedAmount { get; set; }
        public Nullable<int> SelectedAmount { get => _SelectedAmount; set { _SelectedAmount = value; OnPropertyChanged(); } }
        private int _Price { get; set; }
        public int Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
        private DateTime? _RepairDate { get; set; }
        public DateTime? RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }

        #endregion

        #region Important Data
        private CarServiceWindow _carServiceWindow { get; set; }
        public CarServiceWindow carServiceWindow { get => _carServiceWindow; set { _carServiceWindow = value; } }
        private REPAIRFORM _RepairForm { get; set; }
        public REPAIRFORM RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private CARRECEPTION _CarReception { get; set; }
        public CARRECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private int _Total { get; set; }
        public int Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }
        #endregion
        public CarServiceViewModel()
        {
            GenaralFunction();
        }
        public CarServiceViewModel(CARRECEPTION carReception)
        {
            GenaralFunction();
            this.CarReception = DataProvider.Ins.DB.CARRECEPTIONs.Where(x=>x.Id == carReception.Id).SingleOrDefault();
            // base data

            LoadCarInfo(this.CarReception.Id);
            // Load thông tin xe 

            VisReption(true);
            // Hiện bảng thông tin và ẩn nút tiếp nhận xe

            if (DataProvider.Ins.DB.REPAIRFORMs.Where(x => x.IdCarReception == this.CarReception.Id).Count() > 0)
            {
                VisRepair(true);
                LoadRepairInfo(this.CarReception.Id);
                RepairDate = RepairForm.RepairDate;
            }
            else EnableRepairBtn(true);
            // Kiểm tra bảng thông tin sửa chữa

            UpdateTotal();
            // Tính tổng tiền
        }
        public void GenaralFunction()
        {
            InitData();
            InitVis();
            InitBtn();

            ListBrand = new ObservableCollection<CARBRAND>(DataProvider.Ins.DB.CARBRANDs);
            ListPay = new ObservableCollection<PAY>(DataProvider.Ins.DB.PAYs);
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
                    AddRepairFormWindow addRepairFormWindow = new AddRepairFormWindow();
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
                    if (Content == null
                    || SelectedSupply == null
                    || SelectedPay == null
                    || SelectedAmount == null
                    ) return false;
                    return true;
                },
                (p) =>
                {
                    REPAIRINFO repairInfo = new REPAIRINFO()
                    {
                        Content = Content,
                        IdPay = SelectedPay.Id,
                        IdRepairForm = RepairForm.Id
                    ,
                        IdSupply = SelectedSupply.Id,
                        SuppliesAmount = SelectedAmount,
                        TotalMoney = TotalMoney
                    };
                    ListRepair.Add(new RepairItem() { RepairInfo = repairInfo, Price = Price, TotalMoney = TotalMoney });
                    DataProvider.Ins.DB.REPAIRINFOes.Add(repairInfo);
                    DataProvider.Ins.DB.SaveChanges();
                    UpdateTotal();
                });
            EditCommand = new RelayCommand<Object>(
                (p) => {
                    if (SelectedItem == null) return false;
                    if (Content == null || SelectedSupply == null || SelectedPay == null || SelectedAmount == null) return false;
                    return true;
                },
                (p) =>
                {
                    REPAIRINFO temp = DataProvider.Ins.DB.REPAIRINFOes.Where(x => x.Id == SelectedItem.RepairInfo.Id).SingleOrDefault();
                    temp.Content = Content;
                    temp.IdSupply = SelectedSupply.Id;
                    temp.IdPay = SelectedPay.Id;
                    temp.SuppliesAmount = SelectedAmount;
                    temp.TotalMoney = TotalMoney;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.Price = Price;
                    SelectedItem.TotalMoney = TotalMoney;
                    UpdateTotal();
                });
            DeleteCommand = new RelayCommand<Object>(
                (p) => {
                    if (SelectedItem == null) return false;
                    if (DataProvider.Ins.DB.REPAIRINFOes.Where(x => x.Id == SelectedItem.RepairInfo.Id).SingleOrDefault() == null)
                        return false;
                    return true;
                },
                (p) =>
                {
                    DeleteModel deleteModel = new DeleteModel();
                    deleteModel.RepairInfo(SelectedItem.RepairInfo);
                    ListRepair.Remove(SelectedItem);
                    UpdateTotal();
                });
            UpdateValue = new RelayCommand<Object>(
                (p) => {
                    if (SelectedSupply == null || SelectedAmount == null || SelectedPay == null
                    ) return false;
                    return true;
                },
                (p) =>
                {
                    Calculate();
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
                    return true;
                },
                (p) =>
                {
                PayWindow payWindow = new PayWindow(CarReception);
                payWindow.ShowDialog();

                PayViewModel payViewModel = (payWindow.DataContext as PayViewModel);

                });
        }
        public void Calculate()
        {
            Price = (int)SelectedSupply.Supplies_Price * (int)SelectedAmount;
            TotalMoney = (int)Price + (int)SelectedPay.Pay_Price;
        }
        public void UpdateTotal()
        {
            Total = 0;
            foreach (var item in ListRepair)
            {
                Total = Total + (int)item.TotalMoney;
            }
        }
        public void InitData()
        {
            IsRecepted = false;
            SelectedAmount = 1;
            Price = 0;
            TotalMoney = 0;
            Total = 0;
        }
        public void InitBtn()
        {
            IsEnabledAddRepairBtn = false;
            IsEnableReceptionBtn = true;
        }
        public void InitVis()
        {
            VisReceptionCard = true;
            VisAddRepairCard = true;
            VisInfo = false;
            VisRepairServiceCard = false;  
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
            CarReception = new CARRECEPTION();
            CarReception = DataProvider.Ins.DB.CARRECEPTIONs.Where(x => x.Id == IdNewCar).SingleOrDefault();
            Name = CarReception.CUSTOMER.Customer_Name;
            Address = CarReception.CUSTOMER.Customer_Address;
            Phone = CarReception.CUSTOMER.Customer_Phone;
            LicensePlate = CarReception.LicensePlate;
            ReceptionDate = CarReception.ReceptionDate;
            Brand = CarReception.CARBRAND.CarBrand_Name;
        }
        public void LoadRepairInfo(int ID)
        {
            RepairForm = DataProvider.Ins.DB.REPAIRFORMs.Where(x=> x.IdCarReception == ID).SingleOrDefault();
            var repairInfo = DataProvider.Ins.DB.REPAIRINFOes.Where(x => x.IdRepairForm == RepairForm.Id);
            foreach (var item in repairInfo)
            {
                RepairItem repairItem = new RepairItem();
                repairItem.RepairInfo = item;
                repairItem.TotalMoney = item.TotalMoney;
                repairItem.Price = (int)item.SUPPLIES.Supplies_Price * (int)item.SuppliesAmount;
                ListRepair.Add(repairItem);
            }
        }
        public void AddRepairForm(REPAIRFORM newRepairForm)
        {
            newRepairForm.IdCarReception = CarReception.Id;
            DataProvider.Ins.DB.REPAIRFORMs.Add(newRepairForm);
            DataProvider.Ins.DB.SaveChanges();
            RepairForm = newRepairForm;
        }
    }
}
