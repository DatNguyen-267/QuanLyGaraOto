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
    public class ServiceViewModel : BaseViewModel
    {
        private ObservableCollection<ListCar> _ListCar { get; set; }
        public ObservableCollection<ListCar> ListCar { get => _ListCar; set { _ListCar = value; OnPropertyChanged(); } }
        private ListCar _SelectedItem { get; set; }
        public ListCar SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }
        public ICommand CarReceptionCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand OpenCommand { get; set; }
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
                OnPropertyChanged();
            }
        }
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
                    DeleteModel deleteModel = new DeleteModel();
                    deleteModel.CarReception(SelectedItem.CarReception);
                    ListCar.Remove(SelectedItem);
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
                    CarServiceWindow carServiceWindow = new CarServiceWindow(SelectedItem.CarReception);
                    carServiceWindow.ShowDialog();

                    CarServiceViewModel carServiceViewModel = (carServiceWindow.DataContext as CarServiceViewModel);
                    SelectedItem.CarReception = carServiceViewModel.CarReception;
                    SelectedItem.Debt = carServiceViewModel.Total;
                }
            );
            LoadListData();
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
    }
}
