using Prism.Services.Dialogs;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace QuanLyGaraOto.ViewModel
{
    public class CarReceptionViewModel :BaseViewModel
    {
        private ObservableCollection<CAR_BRAND> _ListBrand { get; set; }
        public ObservableCollection<CAR_BRAND> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
        private bool _IsSuccess { get; set; }
        public bool IsSuccess { get => _IsSuccess; set { _IsSuccess = value; OnPropertyChanged(); } }
       
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

        private bool _VisOverDate { get; set; }
        public bool VisOverDate { get => _VisOverDate; set { _VisOverDate = value; OnPropertyChanged(); } }
     
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CheckDate { get; set; }

        public CarReceptionViewModel()
        {
            VisOverDate = false;
            ReceptionDate = DateTime.Now;
            IsSuccess = false;
            ListBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
            });
            ConfirmCommand = new RelayCommand<CarReceptionWindow>((p) => {
                Regex regex = new Regex(@"^[0-9]+$");
                if (VisOverDate == true) return false;
                if (string.IsNullOrEmpty(p.txbName.Text)
                    || (string.IsNullOrEmpty(p.txtPhone.Text) || !regex.IsMatch(p.txtPhone.Text.ToString()))
                    || string.IsNullOrEmpty(p.txbAddress.Text)
                    || SelectedBrand == null
                    || ReceptionDate == null
                    || string.IsNullOrEmpty(p.txbLicensePlate.Text)
                    ) return false;

                return true;
            }, (p) =>
            {
                if (ReceptionDate.Date > DateTime.Now.Date) MessageBox.Show("Ngày tiếp nhận không thể vượt quá ngày hiện tại", "Thông báo lỗi nhập liệu",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (MessageBox.Show("Bạn chắc chắn đồng ý tiếp nhận xe này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        CUSTOMER customer = new CUSTOMER() { Customer_Address = Address, Customer_Phone = Phone, Customer_Name = Name };
                        DataProvider.Ins.DB.CUSTOMERs.Add(customer);
                        DataProvider.Ins.DB.SaveChanges();

                        RECEPTION carReception = new RECEPTION()
                        {
                            IdCustomer = customer.Customer_Id,
                            LicensePlate = LicensePlate,
                            ReceptionDate = ReceptionDate.Date,
                            IdCarBrand = SelectedBrand.CarBrand_Id
                        };
                        DataProvider.Ins.DB.RECEPTIONs.Add(carReception);
                        DataProvider.Ins.DB.SaveChanges();
                        IsSuccess = true;
                        IdNew = carReception.Reception_Id;
                        MessageBox.Show("Tiếp nhận xe thành công", "Thông báo", MessageBoxButton.OK);
                        p.Close();
                    }
                }
            });
            CheckDate = new RelayCommand<DatePicker>((p) => {
                return true;
            }, (p) =>
            { 
                VisOverDate = false;
                if (p.SelectedDate > DateTime.Now.Date)
                {
                    VisOverDate = true;
                }
            });
        }
        public void ValidateNumber(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
