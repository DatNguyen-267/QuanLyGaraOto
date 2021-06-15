using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class BunkViewModel : BaseViewModel
    {
        public ICommand OpenAddCommand { get; set; }

        public ICommand OpendEditCommand { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ICommand OpenImportCommand { get; set; }
        public ICommand ImportCommand { get; set; }

        public ICommand SuppliesBillCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        private BunkWindow _MainWindow;

        public BunkWindow MainWindow { get => _MainWindow; set { _MainWindow = value; } }

        private ObservableCollection<SUPPLIES> _ListSupplies { get; set; }
        public ObservableCollection<SUPPLIES> ListSupplies { get => _ListSupplies; set { _ListSupplies = value; OnPropertyChanged(); } }

        private string _Name { get; set; }

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Price { get; set; }

        public string Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private string _Amount { get; set; }

        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        private SUPPLIES _SelectedItem { get; set; }
        public SUPPLIES SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                if (_SelectedItem != null)
                {
                    Name = SelectedItem.Supplies_Name;
                    Price = SelectedItem.Supplies_Price.ToString();
                    Amount = SelectedItem.Supplies_Amount.ToString();
                }
                OnPropertyChanged();
            }
        }


        public BunkViewModel()
        {
            ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);


            OpenAddCommand = new RelayCommand<MainWindow>((p) => true, (p) => OpenAddWd(p));
            AddCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                {
                    var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                    if (List == null || List.Count() != 0) return false;
                    if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbPrice.Text))
                        return false;
                    return true;

                },
                (p) =>
                {
                    var supplier = new SUPPLIES() { Supplies_Name = p.txbName.Text, Supplies_Price = Int32.Parse(p.txbPrice.Text), Supplies_Amount = 0 };
                    DataProvider.Ins.DB.SUPPLIES.Add(supplier);
                    ListSupplies.Add(supplier);
                    DataProvider.Ins.DB.SaveChanges();
                    p.Close();
                });
            EditCommand = new RelayCommand<BunkWindow>(
                (p) =>
                {
                    if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price))
                        return false;
                    if (SelectedItem == null) return false;
                    return true;

                },
                (p) =>
                {
                    var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == SelectedItem.Supplies_Id).SingleOrDefault();
                    List.Supplies_Name = Name;
                    List.Supplies_Price = Int32.Parse(Price);
                    DataProvider.Ins.DB.SaveChanges();
                    OnPropertyChanged("List");

                }
                );
            OpenImportCommand = new RelayCommand<MainWindow>((p) => true, (p) => OpenImportWd(p));

            SuppliesBillCommand = new RelayCommand<object>((p) => { return true; }, p => { GoodBillWindow wd = new GoodBillWindow(); wd.ShowDialog(); });
        }

        public void OpenImportWd(MainWindow wd)
        {
            AddImportWindow addImportWindow = new AddImportWindow();
            addImportWindow.ShowDialog();
        }
        public void OpenAddWd(MainWindow wd)
        {
            AddNewGoodWindow wdAddGoods = new AddNewGoodWindow();

            wdAddGoods.txbName.Text = "";
            wdAddGoods.btnAdd.Visibility = System.Windows.Visibility.Visible;
            wdAddGoods.txbPrice.Text = "";
            wdAddGoods.ShowDialog();
        }
        //public void AddNewGood(AddNewGoodWindow parameter)
        //{
        //    {
        //        if (string.IsNullOrEmpty(parameter.txbName.Text) || string.IsNullOrEmpty(parameter.txbPrice.Text))
        //            return;
        //        if (SelectedItem == null) return;
        //        var supplier = new Supply() { Name = parameter.txbName.Text, Price = Int32.Parse(parameter.txbPrice.Text), Amount = 0 };
        //        DataProvider.Ins.DB.Supplies.Add(supplier);
        //        DataProvider.Ins.DB.SaveChanges();
        //        ListSupplies.Add(supplier);
        //    }
        //}
    }
}