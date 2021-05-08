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
                    tempListCar.Debt = 10000;
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
            LoadListData();
        }
        public void LoadListData()
        {
            var ListReception = new ObservableCollection<CarReception>(DataProvider.Ins.DB.CarReceptions);
            ListCar = new ObservableCollection<ListCar>();
            foreach (var item in ListReception)
            {
                ListCar tempListCar = new ListCar();
                tempListCar.CarReception = item;
                tempListCar.Debt = 10000;
                ListCar.Add(tempListCar);
            }
        }
    }
}
