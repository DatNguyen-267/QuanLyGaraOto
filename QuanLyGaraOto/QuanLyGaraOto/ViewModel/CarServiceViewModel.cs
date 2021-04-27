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

        private CarServiceWindow _carServiceWindow { get; set; }
        public CarServiceWindow carServiceWindow { get => _carServiceWindow; set { _carServiceWindow = value; } }

        #region Command
        public ICommand ReceptionCommand { get; set; }
        public ICommand AddRepairCommand { get; set; }
        #endregion

        #region Data
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
        public CarServiceViewModel()
        {
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
                (p) => { return true; },
                (p) =>
                {
                    //
                });
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
            CarReception carReception = new CarReception();
            carReception = DataProvider.Ins.DB.CarReceptions.Where(x => x.Id == IdNewCar).Single();
            Name = carReception.Customer.Name;
            Address = carReception.Customer.Address;
            Phone = carReception.Customer.Telephone;
            LicensePlate = carReception.LicensePlate;
            ReceptionDate = carReception.ReceptionDate;
            SelectedBrand = carReception.CarBrand;
        }
    }
}
