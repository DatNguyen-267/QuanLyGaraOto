using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public ICommand CheckDate { get; set; }
        private REPAIR _NewRepairForm { get; set; }
        public REPAIR NewRepairForm { get => _NewRepairForm; set { _NewRepairForm = value; OnPropertyChanged(); } }
        private RECEPTION _Reception { get; set; }
        public RECEPTION Reception { get => _Reception; set { _Reception = value; OnPropertyChanged(); } }

        private bool _VisOverDate { get; set; }
        public bool VisOverDate { get => _VisOverDate; set { _VisOverDate = value; OnPropertyChanged(); } }
        private bool _VisErrorDate { get; set; }
        public bool VisErrorDate { get => _VisErrorDate; set { _VisErrorDate = value; OnPropertyChanged(); } }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public AddRepairFormViewModel(RECEPTION reception)
        {
            IsClose = true;
            this.Reception = reception;
            IsSuccess = false;
            VisErrorDate = false;
            VisOverDate = false;
            RepairDate = DateTime.Now;
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
             
                    p.Close();
                
            });
            ConfirmCommand = new RelayCommand<Window>((p) => {
                if (VisOverDate || VisErrorDate) return false;
                if (RepairDate == null) return false;
                return true;
            }, (p) =>
            {   
                    if (MessageBox.Show("Bạn chắc chắn đồng ý thêm phiếu sửa chữa", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        NewRepairForm = new REPAIR();
                        NewRepairForm.RepairDate = RepairDate;
                        IsSuccess = true; 
                        IsClose = false;
                        p.Close();
                    }
                
            });
            CheckDate = new RelayCommand<DatePicker>((p) => {
                return true;
            }, (p) =>
            {
                VisErrorDate = false;
                VisOverDate = false;

                if (p.SelectedDate.Value.Year <= Reception.ReceptionDate.Year &&
                    p.SelectedDate.Value.Month <= Reception.ReceptionDate.Month &&
                    p.SelectedDate.Value.Day < Reception.ReceptionDate.Day )
                {
                    VisErrorDate = true;
                }
                else if (p.SelectedDate > DateTime.Now.Date)
                {
                    VisOverDate = true;
                }
            });
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsClose)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else e.Cancel = true;
            }
            else e.Cancel = false;
        }
    }

}
