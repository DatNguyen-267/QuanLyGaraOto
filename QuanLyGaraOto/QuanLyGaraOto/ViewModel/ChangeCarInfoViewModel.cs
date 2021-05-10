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
    public class ChangeCarInfoViewModel :BaseViewModel
    {
        private ObservableCollection<CarBrand> _ListBrand { get; set; }
        public ObservableCollection<CarBrand> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
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
        private DateTime? _RepairDate { get; set; }
        public DateTime? RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        private CarBrand _SelectedBrand { get; set; }
        public CarBrand SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }


        private CarReception _CarReception { get; set; }
        public CarReception CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private RepairForm _RepairForm { get; set; }
        public RepairForm RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }

        public ChangeCarInfoViewModel() { }
        public ChangeCarInfoViewModel(CarReception carReception)
        {
            this.CarReception = carReception;
            if (DataProvider.Ins.DB.RepairForms.Where(x=> x.IdCarReception == CarReception.Id).Count() > 0)
            {
                RepairForm = DataProvider.Ins.DB.RepairForms.Where(x => x.IdCarReception == CarReception.Id).SingleOrDefault();
            }
            ListBrand = new ObservableCollection<CarBrand>(DataProvider.Ins.DB.CarBrands);

            InitData();
            Command();
        }
        public void Command()
        {
            ConfirmCommand = new RelayCommand<Window>(
               (p) => {
                   if (Name == null
                    || Phone == null
                    || Address == null
                    || SelectedBrand == null
                    || ReceptionDate == null
                    || LicensePlate == null
                    || RepairDate == null
                    ) return false;
                   return true;
               },
               (p) =>
               {
                   
               });
            CloseCommand = new RelayCommand<Window>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    p.Close();
                });
        }
        public void InitData()
        {
            if (RepairForm!=null)
            {
                RepairDate = RepairForm.RepairDate;
            }
            this.Name = CarReception.Customer.Name;
            this.Address = CarReception.Customer.Address;
            this.Phone = CarReception.Customer.Telephone;
            this.LicensePlate = CarReception.LicensePlate;
            this.SelectedBrand = CarReception.CarBrand;
            this.ReceptionDate = CarReception.ReceptionDate;
        }
    }
}
