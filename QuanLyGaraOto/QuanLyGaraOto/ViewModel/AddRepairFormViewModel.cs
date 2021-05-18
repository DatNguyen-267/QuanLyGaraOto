using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AddRepairFormViewModel : BaseViewModel
    {
        public bool IsSuccess { get; set; }
        private DateTime? _RepairDate { get; set; }
        public DateTime? RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        private REPAIRFORM _NewRepairForm { get; set; }
        public REPAIRFORM NewRepairForm { get => _NewRepairForm; set { _NewRepairForm = value; OnPropertyChanged(); } }
        public AddRepairFormViewModel()
        {
            IsSuccess = false;
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) => {
                if (RepairDate == null) return false;
                return true;
            }, (p) =>
            {
                NewRepairForm = new REPAIRFORM();
                NewRepairForm.RepairDate = RepairDate;
                IsSuccess = true;
                p.Close();
            });
        }
    }
}
