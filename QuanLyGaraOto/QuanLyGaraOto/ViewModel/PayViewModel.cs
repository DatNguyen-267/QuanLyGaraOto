using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class PayViewModel : BaseViewModel
    {
        public bool IsPay {get;set;}
        private CarReception _CarReception { get; set; }
        public CarReception CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private RepairForm _RepairForm { get; set; }
        public RepairForm RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private ObservableCollection<RepairInfo> _ListRepair { get; set; }
        public ObservableCollection<RepairInfo> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public PayViewModel()
        {

        }
        public PayViewModel(CarReception carReception)
        {
            InitData();
            this.CarReception = carReception;
        }
        public void InitData()
        {
            IsPay = false;
            RepairForm = DataProvider.Ins.DB.RepairForms.Where(x => x.IdCarReception == CarReception.Id).SingleOrDefault();
            ListRepair = new ObservableCollection<RepairInfo>(DataProvider.Ins.DB.RepairInfoes.Where(
                x => x.IdRepairForm == RepairForm.Id));
        }
    }
}
