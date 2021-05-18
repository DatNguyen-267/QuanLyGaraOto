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
        private CARRECEPTION _CarReception { get; set; }
        public CARRECEPTION CarReception { get => _CarReception; set { _CarReception = value; OnPropertyChanged(); } }
        private REPAIRFORM _RepairForm { get; set; }
        public REPAIRFORM RepairForm { get => _RepairForm; set { _RepairForm = value; OnPropertyChanged(); } }
        private ObservableCollection<REPAIRINFO> _ListRepair { get; set; }
        public ObservableCollection<REPAIRINFO> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public PayViewModel()
        {

        }
        public PayViewModel(CARRECEPTION carReception)
        {
            InitData();
            this.CarReception = carReception;
        }
        public void InitData()
        {
            IsPay = false;
            RepairForm = DataProvider.Ins.DB.REPAIRFORMs.Where(x => x.IdCarReception == CarReception.Id).SingleOrDefault();
            ListRepair = new ObservableCollection<REPAIRINFO>(DataProvider.Ins.DB.REPAIRINFOes.Where(
                x => x.IdRepairForm == RepairForm.Id));
        }
    }
}
