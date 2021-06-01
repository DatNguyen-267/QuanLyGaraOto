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
        private RECEPTION _CarReception { get; set; }
        public RECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private REPAIR _RepairForm { get; set; }
        public REPAIR RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private ObservableCollection<REPAIR_DETAIL> _ListRepair { get; set; }
        public ObservableCollection<REPAIR_DETAIL> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public PayViewModel()
        {

        }
        public PayViewModel(RECEPTION carReception)
        {
            InitData();
            this.CarReception = carReception;
        }
        public void InitData()
        {
            IsPay = false;
            RepairForm = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == CarReception.Reception_Id).SingleOrDefault();
            ListRepair = new ObservableCollection<REPAIR_DETAIL>(DataProvider.Ins.DB.REPAIR_DETAIL.Where(
                x => x.IdRepair == RepairForm.Repair_Id));
        }
    }
}
