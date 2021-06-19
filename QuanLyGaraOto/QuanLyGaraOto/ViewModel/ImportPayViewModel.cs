using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLyGaraOto.Model;
using QuanLyGaraOto.Template;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace QuanLyGaraOto.ViewModel
{
    public class ImportPayViewModel : BaseViewModel
    {
        public ICommand PayCommand { get; set; }

        public ICommand PrintBillCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private bool _Ispay { get; set; }
        public bool Ispay { get => _Ispay; set { _Ispay = value; OnPropertyChanged(); } }
        private IMPORT_GOODS _Import { get; set; }
        public IMPORT_GOODS Import { get => _Import; set { _Import = value; OnPropertyChanged(); } }
        private ObservableCollection<IMPORT_GOODS_DETAIL> _ListImport { get; set; }
        public ObservableCollection<IMPORT_GOODS_DETAIL> ListImport { get => _ListImport; set { _ListImport = value; OnPropertyChanged(); } }

        private ObservableCollection<ListImport> _List { get; set; }
        public ObservableCollection<ListImport> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private int _TotalPay { get; set; }
        public int TotalPay { get => _TotalPay; set { _TotalPay = value; OnPropertyChanged(); } }

        private DateTime _ImportDate { get; set; }
        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }
        public ImportPayViewModel()
        {

        }
        public ImportPayViewModel(IMPORT_GOODS import)
        {
            Import = import;
            int i = 1;
            ImportDate = Import.ImportGoods_Date;
            if (Import.ImportGoods_TotalMoney != 0) Ispay = true ;
            List = new ObservableCollection<ListImport>();
            ListImport = new ObservableCollection<IMPORT_GOODS_DETAIL>(DataProvider.Ins.DB.IMPORT_GOODS_DETAIL.Where(x => x.IdImportGood == Import.ImportGoods_Id));
            foreach (var item in ListImport)
            {
                TotalPay += item.TotalMoney;
                ListImport temp = new ListImport();
                temp.STT = i++;
                temp.ImportDetail = item;
                List.Add(temp);
            }
            PayCommand = new RelayCommand<ImportPayWindow>(
                (p)=>
                {
                    return true;
                },
                (p)=>
                {
                    Import.ImportGoods_TotalMoney = TotalPay;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thanh toán thành công!");
                    p.btnPay.Visibility = Visibility.Hidden;

                }
                );
            CloseCommand = new RelayCommand<ImportPayWindow>(
                (p)=>
                {
                    return true;
                },
                (p)=>
                {
                    p.Close();
                }
                );
            PrintBillCommand = new RelayCommand<ImportPayWindow>(
                (p) =>
                {
                    if (!Ispay) return false;
                    return true;
                },
                (p) =>
                {
                    ImportBillTemplate bill = new ImportBillTemplate(Import);
                    bill.Show();
                    if(ListImport.Count()>14)
                    {
                        bill.Height = bill.Height + 35 * (ListImport.Count() - 13);
                    }
                    PrintViewModel printViewModel = new PrintViewModel();
                    printViewModel.PrintImportBill(bill);
                }
                );
        }
    }
        


}
