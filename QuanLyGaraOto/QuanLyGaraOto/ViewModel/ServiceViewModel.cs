using QuanLyGaraOto.Convert;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {
        #region View Data
        private ObservableCollection<ListCar> _ListCar { get; set; }
        public ObservableCollection<ListCar> ListCar { get => _ListCar; set { _ListCar = value; OnPropertyChanged(); } }
        private ObservableCollection<ListCar> _Temp { get; set; }
        public ObservableCollection<ListCar> Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }

        private ListCar _SelectedItem { get; set; }
        public ListCar SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
       
        private string _ReceptionOfDay { get; set; }
        public string ReceptionOfDay { get => _ReceptionOfDay; set { _ReceptionOfDay = value; OnPropertyChanged(); } }
        private GARA_INFO _GaraInfo { get; set; }
        public GARA_INFO GaraInfo { get => _GaraInfo; set { _GaraInfo = value; OnPropertyChanged(); } }
        private int _ReceptionAmount { get; set; }
        public int ReceptionAmount
        {
            get => _ReceptionAmount; set
            {
                _ReceptionAmount = value;
                ReceptionOfDay = "Số xe đã tiếp nhận hôm nay: " + ReceptionAmount.ToString() + "/" + GaraInfo.MaxCarReception.ToString();
                OnPropertyChanged(); } }
        #endregion

        #region Search Data
        private string _CustomerName { get; set; }
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }
        private string _LicensePlate { get; set; }
        public string LicensePlate { get => _LicensePlate; set { _LicensePlate = value; OnPropertyChanged(); } }
        private string _Debt { get; set; }
        public string Debt { get => _Debt; set { _Debt = value; OnPropertyChanged(); } }
        private string _ID { get; set; }
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }
        private CAR_BRAND _SelectedBrand { get; set; }
        public CAR_BRAND SelectedBrand { get => _SelectedBrand; set { _SelectedBrand = value; OnPropertyChanged(); } }
        private ObservableCollection<CAR_BRAND> _ListCarBrand { get; set; }
        public ObservableCollection<CAR_BRAND> ListCarBrand { get => _ListCarBrand; set { _ListCarBrand = value; OnPropertyChanged(); } }

        private ObservableCollection<ListCar> _TempListCar { get; set; }
        public ObservableCollection<ListCar> TempListCar { get => _TempListCar; set { _TempListCar = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand CarReceptionCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand SelectionChanged { get; set; }
        #endregion
        public ServiceViewModel()
        {
            InitData();
            CarReceptionCommand = new RelayCommand<object>((p) => {
                LoadReceptionAmount();
               
                return true;
            }, (p)=> {
                if (ReceptionAmount >= GaraInfo.MaxCarReception)
                {
                    MessageBox.Show("Số xe tiếp nhận ngày hôm nay đã đạt tối đa "+ ReceptionAmount.ToString() + "/" +
                        GaraInfo.MaxCarReception.ToString() + " không thể tiếp nhận thêm",
                        "Thông báo số xe đã tiếp nhận tối đa",
                        MessageBoxButton.OK,MessageBoxImage.Warning);
                }
                else
                {
                    CarServiceWindow carServiceWindow = new CarServiceWindow();
                    carServiceWindow.ShowDialog();

                    CarServiceViewModel carServiceViewModel = (carServiceWindow.DataContext as CarServiceViewModel);
                    if (carServiceViewModel.IsRecepted)
                    {
                        ListCar tempListCar = new ListCar();
                        tempListCar.CarReception = carServiceViewModel.CarReception;
                        tempListCar.Debt = carServiceViewModel.Total;
                        ListCar.Add(tempListCar);
                        LoadReceptionAmount();
                    }
                }
            });
            DeleteCommand = new RelayCommand<object>
                ((p) =>
                {
                    if (SelectedItem == null) return false;
                    return true;
                }, (p) =>
                {
                    foreach (var item in Temp)
                    {
                        DeleteModel deleteModel = new DeleteModel();
                        deleteModel.CarReception(item.CarReception);
                        ListCar.Remove(item);
                    }
                    LoadReceptionAmount();
                }
            );
            OpenCommand = new RelayCommand<object>
                ((p) =>
                {
                    if (SelectedItem == null) return false;
                    return true;
                }, (p) =>
                {
                    if (Temp.Count()>1)
                    {
                        MessageBox.Show("Vui lòng chỉ chọn 1 dòng để xem thông tin", "Lỗi chọn nhiều dữ liệu cùng lúc", MessageBoxButton.OK);
                    }
                    else
                    {
                        CarServiceWindow carServiceWindow = new CarServiceWindow(SelectedItem.CarReception);
                        carServiceWindow.ShowDialog();

                        CarServiceViewModel carServiceViewModel = (carServiceWindow.DataContext as CarServiceViewModel);
                        SelectedItem.CarReception = carServiceViewModel.CarReception;
                        SelectedItem.Debt = carServiceViewModel.Total;
                    }
                   
                }
            );
            SearchCommand = new RelayCommand<ServiceWindow>
                ((p) =>
                {
                    if (string.IsNullOrEmpty(p.txbID.Text) 
                    && string.IsNullOrEmpty(p.txbDebt.Text) 
                    && string.IsNullOrEmpty(p.txbLicensePlate.Text) 
                    && (SelectedBrand == null ||SelectedBrand.CarBrand_Name == null ) 
                    && string.IsNullOrEmpty(CustomerName)
                    ) return false;
                    return true;
                }, (p) =>
                {
                    LoadListData();
                    UnicodeConvert uni = new UnicodeConvert();
                    TempListCar = new ObservableCollection<ListCar>();


                    foreach (var item in ListCar)
                    {
                        if (((!string.IsNullOrEmpty(p.txbID.Text) && item.CarReception.Reception_Id.ToString().Contains(ID.ToString()))
                        || (string.IsNullOrEmpty(p.txbID.Text)))

                        && ((!string.IsNullOrEmpty(p.txbLicensePlate.Text) && uni.RemoveUnicode(item.CarReception.LicensePlate).ToLower().Contains(uni.RemoveUnicode(LicensePlate).ToLower()))
                        || (string.IsNullOrEmpty(p.txbLicensePlate.Text)))

                        && ((SelectedBrand == null || SelectedBrand.CarBrand_Name == null) || ((SelectedBrand != null ) && item.CarReception.CAR_BRAND.CarBrand_Name == SelectedBrand.CarBrand_Name)
                         )

                        && ((!string.IsNullOrEmpty(p.txbCustomerName.Text) && uni.RemoveUnicode(item.CarReception.CUSTOMER.Customer_Name).ToLower().Contains(uni.RemoveUnicode(CustomerName).ToLower()))
                        ||(string.IsNullOrEmpty(p.txbCustomerName.Text))) 

                        && ( (!string.IsNullOrEmpty(p.txbDebt.Text) && (item.CarReception.Debt.ToString() == Debt.ToString()))
                        ||(string.IsNullOrEmpty(p.txbDebt.Text)))) TempListCar.Add(item);
                    }
                    ListCar = TempListCar;
                }
            );
            RefeshCommand = new RelayCommand<ServiceWindow>
                ((p) =>
                {
                    return true;
                }, (p) =>
                {
                    ID = "";
                    CustomerName = "";
                    Debt = "";
                    SelectedBrand = null;
                    LicensePlate = "";
                    LoadListData();
                }
            );
            LoadListData();
            SelectionChanged = new RelayCommand<DataGrid>
                ((p) =>
                {
                    return true;
                }, (p) =>
                {
                    Temp = new ObservableCollection<ListCar>(p.SelectedItems.Cast<ListCar>().ToList()) ;
                }
            );
        }

        public void LoadReceptionAmount()
        {
            ReceptionAmount = 0;
            ObservableCollection<RECEPTION> list = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
            foreach (var item in list)
            {
                if (item.ReceptionDate.Date.Day == DateTime.Now.Date.Day &&
                    item.ReceptionDate.Date.Month == DateTime.Now.Date.Month &&
                    item.ReceptionDate.Date.Year == DateTime.Now.Date.Year)
                {
                    ReceptionAmount++;
                }
            }
        }
        public void InitData()
        {
            GaraInfo = DataProvider.Ins.DB.GARA_INFO.FirstOrDefault();
            ListCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            LoadReceptionAmount();
        }
        public int GetDebt(int ID)
        {
            int Debt = 0;
            if (DataProvider.Ins.DB.REPAIRs.Where(x=>x.IdReception == ID).Count() > 0)
            {
                REPAIR repairForm = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == ID).SingleOrDefault();
                
                if (DataProvider.Ins.DB.REPAIR_DETAIL.Where(x=> x.IdRepair == repairForm.Repair_Id).Count() > 0)
                {
                    ObservableCollection<REPAIR_DETAIL> listRepairInfo = new ObservableCollection<REPAIR_DETAIL>
                        (DataProvider.Ins.DB.REPAIR_DETAIL.Where(x=>x.IdRepair == repairForm.Repair_Id));
                    foreach (var item in listRepairInfo)
                    {
                        Debt = Debt + item.TotalMoney;
                    }
                }
            }
            return Debt;
        }
        public void LoadListData()
        {
            var ListReception = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
            ListCar = new ObservableCollection<ListCar>();
            foreach (var item in ListReception)
            {
                ListCar tempListCar = new ListCar();
                tempListCar.CarReception = item;
                tempListCar.Debt = GetDebt(item.Reception_Id);
                ListCar.Add(tempListCar);
            }
        }
        public void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Temp = new ObservableCollection<ListCar>();
            
            foreach (var item in e.AddedItems)
                
            {
                Temp.Add(item as ListCar);
            }
        }
    }
}
