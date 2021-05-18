using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ServiceViewModel()
        {
            
            CarReceptionCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p)=> {
                CarServiceWindow carServiceWindow = new CarServiceWindow();
                carServiceWindow.ShowDialog();

                CarServiceViewModel carServiceViewModel = (carServiceWindow.DataContext as CarServiceViewModel);
                if (carServiceViewModel.IsRecepted)
                {
                    ListCar tempListCar = new ListCar();
                    tempListCar.CarReception = carServiceViewModel.CarReception;
                    tempListCar.Debt = carServiceViewModel.Total;
                    ListCar.Add(tempListCar);
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
        public int GetDebt(int ID)
        {
            int Debt = 0;
            if (DataProvider.Ins.DB.REPAIRFORMs.Where(x=>x.IdCarReception == ID).Count() > 0)
            {
                REPAIRFORM repairForm = DataProvider.Ins.DB.REPAIRFORMs.Where(x => x.IdCarReception == ID).SingleOrDefault();
                
                if (DataProvider.Ins.DB.REPAIRINFOes.Where(x=> x.IdRepairForm == repairForm.Id).Count() > 0)
                {
                    ObservableCollection<REPAIRINFO> listRepairInfo = new ObservableCollection<REPAIRINFO>
                        (DataProvider.Ins.DB.REPAIRINFOes.Where(x=>x.IdRepairForm == repairForm.Id));
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
            var ListReception = new ObservableCollection<CARRECEPTION>(DataProvider.Ins.DB.CARRECEPTIONs);
            ListCar = new ObservableCollection<ListCar>();
            foreach (var item in ListReception)
            {
                ListCar tempListCar = new ListCar();
                tempListCar.CarReception = item;
                tempListCar.Debt = GetDebt(item.Id);
                ListCar.Add(tempListCar);
            }
        }
    }
}
