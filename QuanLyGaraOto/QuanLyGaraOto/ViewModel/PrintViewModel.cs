using Microsoft.Win32;
using QuanLyGaraOto.Model;
using QuanLyGaraOto.Template;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Size = System.Windows.Size;

namespace QuanLyGaraOto.ViewModel
{
    public class PrintViewModel: BaseViewModel
    {
        public ICommand PrintBillCommand { get; set; }
        public ICommand PrintInventoryReportCommand { get; set; }

        public ICommand PrintSalesReportCommand { get; set; }
        public ICommand PrintStockReceiptCommand { get; set; }
        public ICommand PrintSalaryRecordCommand { get; set; }

        public PrintViewModel()
        {
            PrintInventoryReportCommand = new RelayCommand<InventoryReport>((p) => true, (p) => PrintInventoryReport(p));
            PrintSalesReportCommand = new RelayCommand<ReportSalesTemplate>((p)=>true,(p)=>PrintSalesReport(p));
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
      
        public void PrintBill(BillTemplate billTemplate)
        {

            billTemplate.Height = billTemplate.Height + 2000;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(billTemplate.All, "dsada");
            }

        }

        public void StyleExcel(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");
            //System.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Color = System.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = System.Drawing.Color.White;
            pageHeader.Font.FontName = "Calibri";
            pageHeader.Font.Size = 18;
            pageHeader.Font.Bold = true;
            pageHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            pageHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;

            tableHeader.Font.Color = ExcelKnownColors.Black;
            tableHeader.Font.Bold = true;
            tableHeader.Font.Size = 12;
            tableHeader.Font.FontName = "Calibri";
            tableHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            tableHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;

            tableHeader.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            tableHeader.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;


            //Apply style to the header
           


            sheet["A1"].Text = "Báo cáo tồn";
            sheet["A1"].CellStyle = pageHeader;

            sheet["A2"].Text = "Mã báo cáo";
            sheet["A2"].CellStyle.Font.Bold = false;
            sheet["A2"].CellStyle.Font.Size = 12;

            sheet["A3"].Text = "Tháng";
            sheet["A3"].CellStyle.Font.Bold = false;
            sheet["A3"].CellStyle.Font.Size = 12;

            sheet["A4"].Text = "Nhân viên";
            sheet["A4"].CellStyle.Font.Bold = false;
            sheet["A4"].CellStyle.Font.Size = 12;

            sheet["A1:E1"].Merge();


            sheet["A5"].Text = "STT";
            sheet["B5"].Text = "Vật tư phụ tùng";
            sheet["C5"].Text = "Tồn đầu";
            sheet["D5"].Text = "Phát sinh";
            sheet["E5"].Text = "Tồn cuối";
            sheet["A5:E5"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();
        }
        public void PrintSalesReport(ReportSalesTemplate p)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(p.grdPrint, "SalesReport");
                    p.Close();
                }
            }
            catch { }
        }
        public void PrintInventoryReport(InventoryReport inventoryReport)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel(workbook, worksheet);

                worksheet["B2"].Text = inventoryReport.IdReport.ToString();
                worksheet["B3"].Text = inventoryReport.ReportDate.ToString();
                worksheet["B4"].Text = inventoryReport.uSER_INFO.UserInfo_Name.ToString();

                int i = 6;
                foreach (var item in inventoryReport.ListInventory)
                {
                    Object[] list = new object[] { item.STT.ToString() , item.Supplies_Name, item.TonDau, item.PhatSinh
                    , item.TonCuoi};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;

                }
                worksheet.Columns[1].ColumnWidth = 30;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != null)
                {
                    Stream excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }

           }
        }

    }
}
