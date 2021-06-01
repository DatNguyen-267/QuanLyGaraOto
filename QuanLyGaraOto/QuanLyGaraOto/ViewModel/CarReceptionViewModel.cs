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
        private ObservableCollection<CAR_BRAND> _ListBrand { get; set; }
        public ObservableCollection<CAR_BRAND> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
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
        private DateTime _ReceptionDate { get; set; }
        public DateTime ReceptionDate { get => _ReceptionDate; set { _ReceptionDate = value; OnPropertyChanged(); } }
        private CAR_BRAND _SelectedBrand { get; set; }
        public CAR_BRAND SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }
        private int _IdNew { get; set; }
        public int IdNew { get => _IdNew; set { _IdNew = value; OnPropertyChanged(); } }
        public CarReceptionViewModel()
        {
            IsSuccess = false;
            ListBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
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
                CUSTOMER customer = new CUSTOMER() {Customer_Address = Address, Customer_Phone = Phone, Customer_Name = Name };
                DataProvider.Ins.DB.CUSTOMERs.Add(customer);
                DataProvider.Ins.DB.SaveChanges();
                  
                RECEPTION carReception = new RECEPTION() { 
                    IdCustomer = customer.Customer_Id,
                    LicensePlate = LicensePlate,    
                    ReceptionDate = ReceptionDate,
                    IdCarBrand = SelectedBrand.CarBrand_Id
                };
                DataProvider.Ins.DB.RECEPTIONs.Add(carReception);
                DataProvider.Ins.DB.SaveChanges();
                IsSuccess = true;
                IdNew = carReception.Reception_Id;
                MessageBox.Show("Tiếp nhận xe thành công","Thông báo",MessageBoxButton.OK);
                p.Close();
            });
        }
    }
}
