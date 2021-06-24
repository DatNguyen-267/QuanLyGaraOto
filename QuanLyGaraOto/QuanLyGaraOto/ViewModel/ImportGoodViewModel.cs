using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyGaraOto.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace QuanLyGaraOto.ViewModel
{
    class ImportGoodViewModel : BaseViewModel
    {

        private bool _IsPay { get; set; }

        public bool Ispay { get => _IsPay; set { _IsPay = value; OnPropertyChanged(); } }

        private bool _IsImport { get; set; }
        public bool IsImport { get=> _IsImport; set { _IsImport = value; OnPropertyChanged(); } }

        public ICommand CalculateTotal { get; set; }
        public ICommand ImportCommand { get; set; }

        public ICommand PayCommand { get; set; }

        public ICommand AddGoodCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand ViewCommand { get; set; }

        private ObservableCollection<SUPPLIES> _ListSupplies { get; set; }
        public ObservableCollection<SUPPLIES> ListSupplies { get => _ListSupplies; set { _ListSupplies = value; OnPropertyChanged(); } }

        private ObservableCollection<IMPORT_GOODS_DETAIL> _ListImport { get; set; }
        public ObservableCollection<IMPORT_GOODS_DETAIL> ListImport { get => _ListImport; set { _ListImport = value; OnPropertyChanged(); } }


        private int _ImportPrice { get; set; }
        public int ImportPrice { get => _ImportPrice; set { _ImportPrice = value; OnPropertyChanged(); } }

        private int _ImportAmount{ get; set; }

        public int ImportAmount { get => _ImportAmount; set { _ImportAmount = value; OnPropertyChanged(); } }
        private int _Total { get; set; }
        public int Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }

        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }

        private DateTime _ImportDate { get; set; }
        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }

        private SUPPLIES _SelectedSupply { get; set; }
        public SUPPLIES SelectedSupply { get => _SelectedSupply; set { _SelectedSupply = value;OnPropertyChanged(); } }

        private IMPORT_GOODS_DETAIL _SelectedItem { get; set; }

        public IMPORT_GOODS_DETAIL SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private IMPORT_GOODS _ImportGoods { get; set; }

        public IMPORT_GOODS ImportGoods { get => _ImportGoods; set { _ImportGoods = value; OnPropertyChanged(); } }

        private IMPORT_GOODS_DETAIL _ImportDetail { get; set; }
        public IMPORT_GOODS_DETAIL ImportDetail { get => _ImportDetail; set { _ImportDetail = value; OnPropertyChanged(); } }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

        public ImportGoodViewModel()

        {
            IsClose = true;
            ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
            IsImport = false;
            Command();



        }
        public ImportGoodViewModel(IMPORT_GOODS import)
        {
            IsClose = true;
            IsImport = false;
            ImportGoods = import;
            ListSupplies = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
            ListImport = new ObservableCollection<IMPORT_GOODS_DETAIL>(DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Where(x=> x.IdImportGood == ImportGoods.ImportGoods_Id));
            ImportDate = import.ImportGoods_Date;
            if (import.ImportGoods_TotalMoney != 0) IsImport = true;

            Command();
        }
        public void Command()
        {
            AddGoodCommand = new RelayCommand<ImportWindow>(
                (p) =>
                {
                    if (IsImport) return false;
                    if (Total == 0) return false;
                    if (string.IsNullOrEmpty(p.txbPrice.Text) || string.IsNullOrEmpty(p.txbAmount.Text)) return false;
                    Regex regex = new Regex(@"^[0-9]+$");
                    if (!regex.IsMatch((p.txbPrice.Text)) && !string.IsNullOrEmpty((p.txbPrice.Text))) return false;
                    
                    if (!regex.IsMatch((p.txbAmount.Text)) && !string.IsNullOrEmpty((p.txbAmount.Text))) return false;

                    return true;

                },
                (p) =>
                {
                       
                    IMPORT_GOODS_DETAIL item = new IMPORT_GOODS_DETAIL() {IdImportGood = ImportGoods.ImportGoods_Id,IdSupplies = SelectedSupply.Supplies_Id,Price = Int32.Parse(p.txbPrice.Text), Amount = Int32.Parse(p.txbAmount.Text),TotalMoney = Total };
                    if (ListImport.Count >= 1)
                    {
                        foreach (var tempitem in ListImport)
                        {
                            if (tempitem.IdSupplies == item.IdSupplies)
                            {
                                if (tempitem.Price != item.Price)
                                {
                                    MessageBoxResult rs = MessageBox.Show("Vật tư bạn chọn đã được thêm vào với một giá nhập khác, bạn có muốn cập nhật lại giá vật tư?", "Thông báo", MessageBoxButton.YesNo);
                                    if (MessageBoxResult.Yes == rs)
                                    {
                                        tempitem.Price = item.Price;
                                        tempitem.TotalMoney = item.Price * tempitem.Amount;
                                    }
                                    else { item.Price = tempitem.Price; }
                                }

                                tempitem.Amount += item.Amount;
                                tempitem.TotalMoney += item.Price * item.Amount;
                                return;
                            }
                        }
                    }
                    SUPPLIES sp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == item.IdSupplies).FirstOrDefault();
                    DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Add(item);
                    DataProvider.Ins.DB.SaveChanges();
                    ListImport.Add(item);
                    UpdateTotalMoney();

                }
                );
            DeleteCommand = new RelayCommand<ImportWindow>(
                (p) =>
                {
                    if (IsImport) return false;

                    if (SelectedItem == null) return false;
                    return true;
                },
                (p) =>
                {
                    
                    MessageBoxResult rs = MessageBox.Show("Bạn có muốn xóa vật tư này ra khỏi danh sách?", "Thông báo", MessageBoxButton.YesNo);
                    if (MessageBoxResult.Yes == rs)
                    {
                        //if (SelectedItem.IdSupplies != null)
                        //{
                        //    var temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == SelectedItem.IdSupplies).SingleOrDefault();
                        //    temp.Supplies_Amount = temp.Supplies_Amount - SelectedItem.Amount;
                        //    DataProvider.Ins.DB.SaveChanges();
                        //}
                        DeleteModel delete = new DeleteModel();
                        delete.ImportInfo(SelectedItem);
                        ListImport.Remove(SelectedItem);

                    }

                }
                );
            CalculateTotal = new RelayCommand<ImportWindow>(
                (p) =>
                {
                    if (SelectedSupply == null) return false;
                    if  (string.IsNullOrEmpty(p.txbPrice.Text) || string.IsNullOrEmpty(p.txbAmount.Text)) return false;
                    return true;
                },
                (p) =>
                {   
                    Total = Int32.Parse(p.txbAmount.Text) * Int32.Parse(p.txbPrice.Text);
                });
            
            PayCommand = new RelayCommand<ImportWindow>(
                (p) =>
                {
                    if (ImportGoods.ImportGoods_TotalMoney != 0) return false;
                        if (ListImport.Count == 0) return false;
                    return true;
                },
                (p) =>
                {
                    ImportPayWindow wd = new ImportPayWindow(ImportGoods);
                    wd.ShowDialog();
                    if (ImportGoods.ImportGoods_TotalMoney != 0)
                    {
                        p.btnPay.Visibility = Visibility.Hidden;
                        p.btnView.Visibility = Visibility.Visible;
                        IsImport = true;
                    } 
                }
                );
            ViewCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                ImportPayWindow wd = new ImportPayWindow(ImportGoods);
                if (ImportGoods.ImportGoods_TotalMoney != 0)
                {
                    wd.btnPay.Visibility = Visibility.Hidden;
                }
                wd.ShowDialog();

            });
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                p.Close();

            });
        }
        public void UpdateTotalMoney()
        {
            TotalMoney = 0;
            foreach (var item in ListImport)
            {
                TotalMoney += (int)item.TotalMoney;
            }
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
