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

        #endregion

        #region Data Reception
        private ObservableCollection<CarBrand> _ListBrand { get; set; }
        public ObservableCollection<CarBrand> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
        private CarBrand _SelectedBrand { get; set; }
        public CarBrand SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }
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
        private ObservableCollection<Supply> _ListSupply { get; set; }
        public ObservableCollection<Supply> ListSupply { get => _ListSupply; set { _ListSupply = value; OnPropertyChanged(); } }
        private ObservableCollection<Pay> _ListPay { get; set; }
        public ObservableCollection<Pay> ListPay { get => _ListPay; set { _ListPay = value; OnPropertyChanged(); } }
        private CarBrand _SelectedItem { get; set; }
        public CarBrand SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        private string _Content { get; set; }
        public string Content { get => _Content; set { _Content = value; OnPropertyChanged(); } }
        private Supply _SelectedSupply { get; set; }
        public Supply SelectedSupply { get => _SelectedSupply; set { _SelectedSupply = value; OnPropertyChanged(); } }
        private Pay _SelectedPay { get; set; }
        public Pay SelectedPay { get => _SelectedPay; set { _SelectedPay = value; OnPropertyChanged(); } }
        private int? _SelectedAmount { get; set; }
        public int? SelectedAmount { get => _SelectedAmount; set { _SelectedAmount = value; OnPropertyChanged(); } }
        private int? _Price { get; set; }
        public int? Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private int? _TotalMoney { get; set; }
        public int? TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
        #endregion

        #region Important Data
        private CarServiceWindow _carServiceWindow { get; set; }
        public CarServiceWindow carServiceWindow { get => _carServiceWindow; set { _carServiceWindow = value; } }
        private RepairForm _RepairForm { get; set; }
        public RepairForm RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private CarReception _CarReception { get; set; }
        public CarReception CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        #endregion
        public CarServiceViewModel()
        {
            InitData();
            InitVis();
            InitBtn();
            ListBrand = new ObservableCollection<CarBrand>(DataProvider.Ins.DB.CarBrands);
            ReceptionCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    CarReceptionWindow carReceptionWindow = new CarReceptionWindow();
                    carReceptionWindow.ShowDialog();
                    CarReceptionViewModel backData = (carReceptionWindow.DataContext as CarReceptionViewModel);
                    if (backData.IsSuccess) {
                        VisReption(true);
                        EnableRepairBtn(true);
                        LoadCarInfo(backData.IdNew);
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
                    }
                });
            AddCommand = new RelayCommand<Object>(
                (p) => {
                    if (Content == null
                    || SelectedSupply == null
                    || (SelectedAmount == null || SelectedAmount.GetType() != typeof(int))
                    || SelectedPay == null
                    || (Price == null || Price.GetType() != typeof(int))
                    || (TotalMoney == null || TotalMoney.GetType() != typeof(int))
                    ) return false;
                    return true;
                },
                (p) =>
                {
                    
                });
        }
        public void InitData()
        {
            SelectedAmount = 0;
            Price = 0;
            TotalMoney = 0;
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
            CarReception = new CarReception();
            CarReception = DataProvider.Ins.DB.CarReceptions.Where(x => x.Id == IdNewCar).Single();
            Name = CarReception.Customer.Name;
            Address = CarReception.Customer.Address;
            Phone = CarReception.Customer.Telephone;
            LicensePlate = CarReception.LicensePlate;
            ReceptionDate = CarReception.ReceptionDate;
            SelectedBrand = CarReception.CarBrand;
        }
        public void LoadRepairInfo()
        {

        }
        public void AddRepairForm(RepairForm newRepairForm)
        {
            newRepairForm.IdCarReception = CarReception.Id;
            DataProvider.Ins.DB.RepairForms.Add(newRepairForm);
            DataProvider.Ins.DB.SaveChanges();
            RepairForm = newRepairForm;
        }
    }
}
