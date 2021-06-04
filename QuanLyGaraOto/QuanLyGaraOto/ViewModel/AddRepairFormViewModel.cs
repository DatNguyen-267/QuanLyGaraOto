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
        private DateTime _RepairDate { get; set; }
        public DateTime RepairDate { get => _RepairDate; set { _RepairDate = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        private REPAIR _NewRepairForm { get; set; }
        public REPAIR NewRepairForm { get => _NewRepairForm; set { _NewRepairForm = value; OnPropertyChanged(); } }
        public AddRepairFormViewModel()
        {
            IsSuccess = false;
            RepairDate = DateTime.Now;
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) => {
                if (RepairDate == null) return false;
                return true;
            }, (p) =>
            {
                NewRepairForm = new REPAIR();
                NewRepairForm.RepairDate = RepairDate;
                IsSuccess = true;
                p.Close();
            });
        }
    }
}
