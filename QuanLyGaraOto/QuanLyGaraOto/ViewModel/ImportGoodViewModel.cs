using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyGaraOto.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace QuanLyGaraOto.ViewModel
{
    class ImportGoodViewModel : BaseViewModel
    {
        public ICommand AddImportCommand { get; set; }

        public ICommand CalculateTotal { get; set; }
        public ICommand ImportCommand { get; set; }

        public ICommand AddGoodCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private ObservableCollection<SUPPLIES> _ListSupplies { get; set; }
        public ObservableCollection<SUPPLIES> ListSupplies { get => _ListSupplies; set { _ListSupplies = value; OnPropertyChanged(); } }

        private int _Total { get; set; }
        public int Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }

        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private SUPPLIES _SelectedSupply { get; set; }
        public SUPPLIES SelectedSupply { get => _SelectedSupply; set { _SelectedSupply = value;OnPropertyChanged();
                
            } }


       
        public ImportGoodViewModel()
            
        {
            ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);

            //Command
            AddImportCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) => {
                    AddImportWindow addImportWindow = new AddImportWindow();
                    addImportWindow.ShowDialog();
                    AddImportWindowViewModel addRepairFormViewModel = (addImportWindow.DataContext as AddImportWindowViewModel);
                    if (addRepairFormViewModel.IsSuccess)
                    {
                        
                    }
                }
                );
            CalculateTotal = new RelayCommand<ImportWindow>(
                (p) => {
                    if (string.IsNullOrEmpty(p.txbPrice.Text) || string.IsNullOrEmpty(p.txbAmount.Text)) return false;
                    return true; },
                (p) =>
                {
                    p.txbCost.Text = (Int32.Parse(p.txbPrice.Text) * Int32.Parse(p.txbAmount.Text)).ToString();
                }
                );
            AddGoodCommand = new RelayCommand<ImportWindow>(
                (p) =>
                {
                    if (SelectedSupply != null)
                    {
                        if (string.IsNullOrEmpty(p.txbPrice.Text) || string.IsNullOrEmpty(p.txbAmount.Text)) return false;
                        return true;
                    }
                    return false;
                },
                (p) =>
                {
                    var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == SelectedSupply.Supplies_Id).SingleOrDefault();
                     List.Supplies_Amount += Int32.Parse(p.txbAmount.Text);
                    DataProvider.Ins.DB.SaveChanges();
                }
                );

        }

        

    }
}
