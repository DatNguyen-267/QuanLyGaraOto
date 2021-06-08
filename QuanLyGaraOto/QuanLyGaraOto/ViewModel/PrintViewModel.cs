using QuanLyGaraOto.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class PrintViewModel: BaseViewModel
    {
        public ICommand PrintBillCommand { get; set; }
        public ICommand PrintStockReceiptCommand { get; set; }
        public ICommand PrintSalaryRecordCommand { get; set; }

        public PrintViewModel()
        {
            PrintBillCommand = new RelayCommand<BillTemplate>((p) => true, (p) => PrintBill(p));
            PrintStockReceiptCommand = new RelayCommand<Object>((p) => true, (p) => PrintStockReceipt());
            PrintSalaryRecordCommand = new RelayCommand<Object>((p) => true, (p) => PrintSalaryRecord());
        }
        public void PrintStockReceipt()
        {
            
        }
        public void PrintSalaryRecord()
        {
            
        }
        public void PrintBill(BillTemplate p)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
               
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(p, "Bill");
                    p.Close();
                }
            }
            catch { }

        }
    }
}
