using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private RECEPTION _Reception { get; set; }
        public RECEPTION Reception { get => _Reception; set { _Reception = value; OnPropertyChanged(); } }
        private REPAIR _Repair { get; set; }
        public REPAIR Repair { get => _Repair; set { _Repair = value; OnPropertyChanged(); } }
        private RECEIPT _Receipt { get; set; }
        public RECEIPT Receipt { get => _Receipt; set { _Receipt = value; OnPropertyChanged(); } }
        private ObservableCollection<ListRepair> _ListRepair { get; set; }
        public ObservableCollection<ListRepair> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public BillViewModel(RECEPTION Reception)
        {
            this.Reception = Reception;
            this.Receipt = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == this.Reception.Reception_Id).SingleOrDefault();
            this.Repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == this.Reception.Reception_Id).SingleOrDefault();
            Command();
            LoadData();
        }
        public void Command()
        {

        }
        public void LoadData()
        {
            var repairDetail = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == Repair.Repair_Id);
            ListRepair = new ObservableCollection<ListRepair>();
            int i = 1;
            foreach (var item in repairDetail)
            {
                ListRepair temp = new ListRepair();
                temp.STT = i++;
                temp.RepairDetail = item;
                ListRepair.Add(temp);
            }
        }
    }
}
