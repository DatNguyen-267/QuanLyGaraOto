using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ChangeCarInfoViewModel :BaseViewModel
    {
        private ObservableCollection<CAR_BRAND> _ListBrand { get; set; }
        public ObservableCollection<CAR_BRAND> ListBrand { get => _ListBrand; set { _ListBrand = value; OnPropertyChanged(); } }
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
        private DateTime _RepairDate { get; set; }
        public DateTime RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        private CAR_BRAND _SelectedBrand { get; set; }
        public CAR_BRAND SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }


        private RECEPTION _CarReception { get; set; }
        public RECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private REPAIR _RepairForm { get; set; }
        public REPAIR RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }


        private bool _VisOverDate1 { get; set; }
        public bool VisOverDate1 { get => _VisOverDate1; set { _VisOverDate1 = value; OnPropertyChanged(); } }

        private bool _VisOverDate2 { get; set; }
        public bool VisOverDate2 { get => _VisOverDate2; set { _VisOverDate2 = value; OnPropertyChanged(); } }
        private bool _VisErrorDate2 { get; set; }
        public bool VisErrorDate2 { get => _VisErrorDate2; set { _VisErrorDate2 = value; OnPropertyChanged(); } }

        public ICommand CheckDate1 { get; set; }
        public ICommand CheckDate2 { get; set; }

        public ChangeCarInfoViewModel() { }
        public ChangeCarInfoViewModel(RECEPTION carReception)
        {
            VisErrorDate2 = false;
            VisOverDate1 = false;
            VisOverDate2 = false;
            this.CarReception = carReception;
            if (DataProvider.Ins.DB.REPAIRs.Where(x=> x.IdReception == CarReception.Reception_Id).Count() > 0)
            {
                RepairForm = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == CarReception.Reception_Id).SingleOrDefault();
            }
            ListBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);

            InitData();
            Command();
        }
        public void Command()
        {
            ConfirmCommand = new RelayCommand<ChangeCarInfoWindow>(
               (p) => {
                   if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbAddress.Text) || 
                   string.IsNullOrEmpty(p.txbLicensePlate.Text) || string.IsNullOrEmpty(p.txbPhone.Text) ||
                   string.IsNullOrEmpty(p.cbbCarBrand.Text) || string.IsNullOrEmpty(p.dpReceptionDate.Text))
                   return false;

                   Regex regex = new Regex(@"^[0-9]+$");
                   if (!regex.IsMatch(p.txbPhone.Text) && !string.IsNullOrEmpty(p.txbPhone.Text)) return false;
                   if (VisOverDate1 || VisOverDate2 || VisErrorDate2) return false;
                   return true;
               },
               (p) =>
               {
                   if (MessageBox.Show("Bạn chắc chắn đồng ý thay đổi thông tin", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                   {
                       var carReceptionTemp = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == CarReception.Reception_Id).SingleOrDefault();
                       carReceptionTemp.CUSTOMER.Customer_Name = Name;
                       carReceptionTemp.CUSTOMER.Customer_Phone = Phone;
                       carReceptionTemp.CUSTOMER.Customer_Address = Address;
                       carReceptionTemp.CAR_BRAND = SelectedBrand;
                       carReceptionTemp.ReceptionDate = ReceptionDate;
                       carReceptionTemp.LicensePlate = LicensePlate;
                       var repairFormTemp = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == CarReception.Reception_Id).SingleOrDefault();
                       if (repairFormTemp != null) repairFormTemp.RepairDate = RepairDate;

                       DataProvider.Ins.DB.SaveChanges();
                       p.Close();
                   }
               });
            CloseCommand = new RelayCommand<Window>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                   
                        p.Close();
                    
                });
            CheckDate1 = new RelayCommand<ChangeCarInfoWindow>((p) => {
                return true;
            }, (p) =>
            {    
                VisOverDate1 = false;
                if (p.dpReceptionDate.SelectedDate.Value.Date > DateTime.Now.Date)
                {
                    VisOverDate1 = true;
                }
                VisOverDate2 = false;
                VisErrorDate2 = false;
                if (p.dpReceptionDate.SelectedDate != null)
                {
                    if (p.dpReceptionDate.SelectedDate.Value.Date > p.dpRepairDate.SelectedDate.Value.Date)
                    {
                        VisErrorDate2 = true;
                    }
                    else if (p.dpRepairDate.SelectedDate.Value.Date > DateTime.Now.Date) VisOverDate2 = true;
                }

            });
            CheckDate2 = new RelayCommand<ChangeCarInfoWindow>((p) => {
                return true;
            }, (p) =>
            {
                VisOverDate2 = false;
                VisErrorDate2 = false;
                if (p.dpReceptionDate.SelectedDate != null)
                {
                    if (p.dpReceptionDate.SelectedDate.Value.Date > p.dpRepairDate.SelectedDate.Value.Date)
                    {
                        VisErrorDate2 = true;
                    }
                    else if (p.dpRepairDate.SelectedDate.Value.Date > DateTime.Now.Date) VisOverDate2 = true;
                }
               
            });
        }
        public void InitData()
        {
            if (RepairForm!=null)
            {
                RepairDate = RepairForm.RepairDate;
            }
            this.Name = CarReception.CUSTOMER.Customer_Name;
            this.Address = CarReception.CUSTOMER.Customer_Address;
            this.Phone = CarReception.CUSTOMER.Customer_Phone;
            this.LicensePlate = CarReception.LicensePlate;
            this.SelectedBrand = CarReception.CAR_BRAND;
            this.ReceptionDate = CarReception.ReceptionDate;
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }

}
