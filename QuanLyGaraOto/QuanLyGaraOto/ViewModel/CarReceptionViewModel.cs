using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace QuanLyGaraOto.ViewModel
{
    public class CarReceptionViewModel :BaseViewModel
    {
        private ObservableCollection<CarBrand> _ListBrand { get; set; }
        public ObservableCollection<CarBrand> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
        private bool _IsSuccess { get; set; }
        public bool IsSuccess { get => _IsSuccess; set { _IsSuccess = value; OnPropertyChanged(); } }
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
        private CarBrand _SelectedBrand { get; set; }
        public CarBrand SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }
        private int _IdNew { get; set; }
        public int IdNew { get => _IdNew; set { _IdNew = value; OnPropertyChanged(); } }
        public CarReceptionViewModel()
        {
            IsSuccess = false;
            ListBrand = new ObservableCollection<CarBrand>(DataProvider.Ins.DB.CarBrands);
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) => {
                if (Name == null
                    || Phone == null
                    || Address == null
                    || SelectedBrand == null
                    || ReceptionDate == null
                    || LicensePlate == null
                    ) return false;
                return true;
            }, (p) =>
            {
                Customer customer = new Customer() { Address = Address, Telephone = Phone, Name = Name };
                DataProvider.Ins.DB.Customers.Add(customer);
                DataProvider.Ins.DB.SaveChanges();
                  
                CarReception carReception = new CarReception() { 
                    IdCustomer = customer.Id,
                    IdStatus = 1,
                    LicensePlate = LicensePlate,    
                    ReceptionDate = ReceptionDate,
                    IdBrand = SelectedBrand.Id
                };
                DataProvider.Ins.DB.CarReceptions.Add(carReception);
                DataProvider.Ins.DB.SaveChanges();
                IsSuccess = true;
                IdNew = carReception.Id;
                MessageBox.Show("Tiếp nhận xe thành công","Thông báo",MessageBoxButton.OK);
                p.Close();
            });
        }
    }
}
