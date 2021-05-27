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
        private DateTime? _ReceptionDate { get; set; }
        public DateTime? ReceptionDate { get => _ReceptionDate; set { _ReceptionDate = value; OnPropertyChanged(); } }
        private DateTime? _RepairDate { get; set; }
        public DateTime? RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        private CAR_BRAND _SelectedBrand { get; set; }
        public CAR_BRAND SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }


        private RECEPTION _CarReception { get; set; }
        public RECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private REPAIR _RepairForm { get; set; }
        public REPAIR RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }

        public ChangeCarInfoViewModel() { }
        public ChangeCarInfoViewModel(RECEPTION carReception)
        {
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
            this.Name = CarReception.CUSTOMER.Customer_Name;
            this.Address = CarReception.CUSTOMER.Customer_Address;
            this.Phone = CarReception.CUSTOMER.Customer_Phone;
            this.LicensePlate = CarReception.LicensePlate;
            this.SelectedBrand = CarReception.CAR_BRAND;
            this.ReceptionDate = CarReception.ReceptionDate;
        }
    }
}
