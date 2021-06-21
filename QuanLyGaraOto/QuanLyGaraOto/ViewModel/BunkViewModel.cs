using QuanLyGaraOto.Convert;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class BunkViewModel : BaseViewModel
    {
        public ICommand OpenAddCommand { get; set; }
        public ICommand OpenEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CloseAddCommand { get; set; }
        public ICommand OpenImportCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand SuppliesBillCommand { get; set; }
        public ICommand SearchSuppliesCommand { get; set; }
        public ICommand RefeshSuppliesCommand { get; set; }
        public ICommand SelectionChanged { get; set; }

        public ICommand ExportCommand { get; set; }

        private BunkWindow _MainWindow;

        public BunkWindow MainWindow { get => _MainWindow; set { _MainWindow = value; } }

        private ObservableCollection<SUPPLIES> _Temp { get; set; }
        public ObservableCollection<SUPPLIES> Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }

        private ObservableCollection<SUPPLIES> _ListSupplies { get; set; }
        public ObservableCollection<SUPPLIES> ListSupplies { get => _ListSupplies; set { _ListSupplies = value; OnPropertyChanged(); } }
        private ObservableCollection<SUPPLIES> _TempListSupplies { get; set; }
        public ObservableCollection<SUPPLIES> TempListSupplies { get => _TempListSupplies; set { _TempListSupplies = value; OnPropertyChanged(); } }
        private string _Name { get; set; }

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Price { get; set; }

        public string Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private string _Amount { get; set; }

        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }

        private USER _user { get; set; }

        public USER user { get => _user; set { _user = value; OnPropertyChanged(); } }


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

        bool _isImportBunk = false;
        public bool isImportBunk { get => _isImportBunk; set { _isImportBunk = value; OnPropertyChanged(); } }
        private string _SuppliesName { get; set; }
        public string SuppliesName { get => _SuppliesName; set { _SuppliesName = value; OnPropertyChanged(); } }

        private string _SuppliesPrice { get; set; }
        public string SuppliesPrice { get => _SuppliesPrice; set { _SuppliesPrice = value; OnPropertyChanged(); } }

        private string _SuppliesAmount { get; set; }
        public string SuppliesAmount { get => _SuppliesAmount; set { _SuppliesAmount = value; OnPropertyChanged(); } }
        public BunkViewModel(bool role, USER user) : this()
        {
            isImportBunk = role;
            this.user = user;
        }


        public BunkViewModel()
        {
            LoadSupplies();
            OpenAddCommand = new RelayCommand<MainWindow>((p) => true, (p) => 
            {
                AddNewGoodWindow wdAddGoods = new AddNewGoodWindow();

                wdAddGoods.txbName.Text = "";
                wdAddGoods.btnAdd.Visibility = System.Windows.Visibility.Visible;
                wdAddGoods.txbPrice.Text = "";
                wdAddGoods.ShowDialog();
                AddNewGoodViewModel add = wdAddGoods.DataContext as AddNewGoodViewModel;
                ListSupplies.Add(add.Supplies);
            });

            OpenEditCommand = new RelayCommand<MainWindow>((p) => {
                if (SelectedItem == null) return false;
                if (Temp.Count > 1) return false;
                return true;
            }, (p) =>
            {
                AddNewGoodWindow wdAddGoods = new AddNewGoodWindow(SelectedItem);

                wdAddGoods.txbName.Text = SelectedItem.Supplies_Name;
                wdAddGoods.btnAdd.Visibility = System.Windows.Visibility.Hidden;
                wdAddGoods.btnEdit.Visibility = System.Windows.Visibility.Visible;
                wdAddGoods.txbPrice.Text = SelectedItem.Supplies_Price.ToString();
                wdAddGoods.ShowDialog();
            });

            CloseAddCommand = new RelayCommand<AddNewGoodWindow>(
                (p)=>
                { return true; },
                (p)=>
                {
                    p.Close();
                }
                );
            OpenImportCommand = new RelayCommand<MainWindow>((p) => true, (p) => OpenImportWd(p));

            DeleteCommand = new RelayCommand<MainWindow>((p) => {
                if (SelectedItem == null) return false;
                return true; }, (p) => 
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại phụ tùng đã chọn", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
                    foreach(var item in Temp)
                    {
                        DeleteModel delete = new DeleteModel();
                        delete.SUPPLIES(item);
                        ListSupplies.Remove(item);
                    }
                    LoadSupplies();
                });

            SuppliesBillCommand = new RelayCommand<object>((p) => { return true; }, p => { GoodBillWindow wd = new GoodBillWindow(); wd.ShowDialog(); });

            SelectionChanged = new RelayCommand<DataGrid>(
                (p)=>
                {
                    return true;
                },
                (p)=>
                {
                    Temp = new ObservableCollection<SUPPLIES>(p.SelectedItems.Cast<SUPPLIES>().ToList());
                }
                );
            SearchSuppliesCommand = new RelayCommand<BunkWindow>((p) => {
                if (p == null) return false;
                Regex regex = new Regex(@"^[0-9]+$");

                if (!regex.IsMatch(p.txbSuppliesPrice.Text) && !string.IsNullOrEmpty(p.txbSuppliesPrice.Text)) return false;
                if (!regex.IsMatch(p.txbSuppliesAmount.Text) && !string.IsNullOrEmpty(p.txbSuppliesAmount.Text)) return false;

                if (string.IsNullOrEmpty(p.txbSuppliesName.Text) && string.IsNullOrEmpty(p.txbSuppliesPrice.Text)
                && string.IsNullOrEmpty(p.txbSuppliesAmount.Text))
                    return false;
                return true;
            }, (p) =>
            {
                ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
                UnicodeConvert uni = new UnicodeConvert();
                TempListSupplies = new ObservableCollection<SUPPLIES>();

                foreach (var item in ListSupplies)
                {
                    if ((string.IsNullOrEmpty(p.txbSuppliesName.Text) ||
                    (!string.IsNullOrEmpty(p.txbSuppliesName.Text) && uni.RemoveUnicode(item.Supplies_Name).ToLower().Contains(uni.RemoveUnicode(p.txbSuppliesName.Text).ToLower())))
                    && ((string.IsNullOrEmpty(p.txbSuppliesAmount.Text) ||
                    (!string.IsNullOrEmpty(p.txbSuppliesAmount.Text) && uni.RemoveUnicode(item.Supplies_Amount.ToString()).ToLower().Contains(uni.RemoveUnicode(p.txbSuppliesAmount.Text).ToLower()))))
                    && ((string.IsNullOrEmpty(p.txbSuppliesPrice.Text) ||
                    (!string.IsNullOrEmpty(p.txbSuppliesPrice.Text) && uni.RemoveUnicode(item.Supplies_Price.ToString()).ToLower().Contains(uni.RemoveUnicode(p.txbSuppliesPrice.Text).ToLower()))))
                    )
                        TempListSupplies.Add(item);
                }
                ListSupplies = TempListSupplies;
            });
            RefeshSuppliesCommand = new RelayCommand<BunkWindow>((p) => { return true; }, (p) =>
            {
                p.txbSuppliesName.Text = "";
                p.txbSuppliesAmount.Text = "";
                p.txbSuppliesPrice.Text = "";
                ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
            });
            ExportCommand = new RelayCommand<BunkWindow>((p) => { return true; }, (p) =>
              {
                  PrintViewModel printViewModel = new PrintViewModel();
                  printViewModel.Print_DanhSachVatTu(ListSupplies);
              });
        }

        public void OpenImportWd(MainWindow wd)
        {
            AddImportWindow addImportWindow = new AddImportWindow(user);
            addImportWindow.ShowDialog();
        }
        void LoadSupplies()
        {
            ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
        }

    }
}