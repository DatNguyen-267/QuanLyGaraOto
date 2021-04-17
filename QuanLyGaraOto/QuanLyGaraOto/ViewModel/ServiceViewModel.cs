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
        public ICommand CarReceptionCommand { get; set; }

        public ServiceViewModel()
        {
            
            CarReceptionCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p)=> {
                CarReceptionWindow carReceptionWindow = new CarReceptionWindow();
                carReceptionWindow.ShowDialog();
            });
            LoadListData();
        }
        public void LoadListData()
        {
            ListCar = new ObservableCollection<ListCar>();
            var ListLicensePlate = DataProvider.Ins.DB.LICENSE_PLATE;

        }
    }
}
