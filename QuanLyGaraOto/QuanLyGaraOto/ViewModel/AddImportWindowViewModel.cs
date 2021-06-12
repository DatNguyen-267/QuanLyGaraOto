using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.Model;
using System.Windows;
using System.Windows.Input;


namespace QuanLyGaraOto.ViewModel
{
    class AddImportWindowViewModel : BaseViewModel
    {
        public bool IsSuccess { get; set; }

        private DateTime _ImportDate { get; set; }

        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private IMPORT_GOODS _NewImport { get; set; }
        public IMPORT_GOODS NewImport { get => _NewImport; set{ _NewImport = value; OnPropertyChanged(); } }

        public AddImportWindowViewModel()
        {
            IsSuccess = false;
            ImportDate = DateTime.Now;
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) => {
                if (ImportDate == null) return false;
                return true;
            }, (p) =>
            {
                NewImport = new IMPORT_GOODS();
                NewImport.ImportGoods_Date = ImportDate;
                
                IsSuccess = true;
                p.Close();
                ImportWindow importWD = new ImportWindow();
                importWD.dtpImportDate.SelectedDate = ImportDate;
                importWD.ShowDialog();
            });

        }

    }
}
