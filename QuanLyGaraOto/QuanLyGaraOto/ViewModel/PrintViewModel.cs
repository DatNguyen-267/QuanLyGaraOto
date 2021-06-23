﻿
using Microsoft.Win32;
using QuanLyGaraOto.Model;
using QuanLyGaraOto.Template;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using IStyle = Syncfusion.XlsIO.IStyle;
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
        public ICommand PrintImportBillCommand { get; set; }
        public ICommand Print_DanhSachVatTuCommand { get; set; }

        public ICommand Print_ThongTinVatTuCommand { get; set; }

        public ICommand Print_ThongTinHangXeCommand { get; set; }
       
        public PrintViewModel()
        {
            Print_ThongTinHangXeCommand= new RelayCommand<ObservableCollection<CAR_BRAND>>((p) => true, (p) => Print_ThongTinHangXe(p));
            Print_ThongTinVatTuCommand =new RelayCommand<ObservableCollection<WAGE>>((p) => true, (p) => Print_ThongTinTienCong(p));
            Print_DanhSachVatTuCommand = new RelayCommand<ObservableCollection<SUPPLIES>>((p) => true, (p) => Print_DanhSachVatTu(p));
            PrintInventoryReportCommand = new RelayCommand<InventoryReport>((p) => true, (p) => PrintInventoryReport(p));
            PrintSalesReportCommand = new RelayCommand<SalesReport>((p)=>true,(p)=>PrintSalesReport(p));
            PrintBillCommand = new RelayCommand<BillTemplate>((p) => true, (p) => PrintBill(p));
            PrintStockReceiptCommand = new RelayCommand<Object>((p) => true, (p) => PrintStockReceipt());
            PrintSalaryRecordCommand = new RelayCommand<Object>((p) => true, (p) => PrintSalaryRecord());
            PrintImportBillCommand = new RelayCommand<ImportBillTemplate>((p) => true, (p)=> PrintImportBill(p)) ;

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

        public void StyleExcel_Inventory(IWorkbook workbook, IWorksheet sheet)
        {

            // THIẾT LẬP CÁI FILE EXCEL NÓ RA SAO
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");
            // này là tạo style 1 cái là header 1 cái là header của cái table

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
            pageHeader.Font.FontName = "Calibri";
            pageHeader.Font.Size = 18;
            pageHeader.Font.Bold = true;
            pageHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            pageHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;
            // thiết lập cho header (copy) 

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
            // cho table (copy)

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
            // này là gán dữ liệu vào mấy cái ô


            sheet["A1:E1"].Merge();
            // này là hợp nhất mấy cái ô từ đâu đến đâu

            sheet["A5"].Text = "STT";
            sheet["B5"].Text = "Vật tư phụ tùng";
            sheet["C5"].Text = "Tồn đầu";
            sheet["D5"].Text = "Phát sinh";
            sheet["E5"].Text = "Tồn cuối";
            sheet["A5:E5"].CellStyle = tableHeader;
            // này cũng là gán chữ

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();
            // copy
        }
        public void StyleExcel_Sales(IWorkbook workbook, IWorksheet sheet)
        {

            
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");
           

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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
            



            sheet["A1"].Text = "Báo cáo Doanh số";
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
            sheet["B5"].Text = "Hiệu xe";
            sheet["C5"].Text = "Số lượt sửa";
            sheet["D5"].Text = "Thành tiền";
            sheet["E5"].Text = "Tỉ lệ";
            sheet["A5:E5"].CellStyle = tableHeader;
            

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();
            
        }


        public void StyleExcel_DanhSachVatTu(IWorkbook workbook, IWorksheet sheet)
        {


            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");


            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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


            sheet["A1"].Text = "Danh sách vật tư";
            sheet["A1"].CellStyle = pageHeader;

            sheet["A1:D1"].Merge();
            sheet["A4"].Text = "ID";
            sheet["B4"].Text = "Tên vật tư";
            sheet["C4"].Text = "Giá";
            sheet["D4"].Text = "Số lượng";
            sheet["A4:D4"].CellStyle = tableHeader;


            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }


        public void StyleExcel_ThongTinTienCong(IWorkbook workbook, IWorksheet sheet)
        {


            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");


            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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




            sheet["A1"].Text = "Thông tin tiền công";
            sheet["A1"].CellStyle = pageHeader;





            sheet["A1:C1"].Merge();


            sheet["A4"].Text = "ID";
            sheet["B4"].Text = "Loại tiền công";
            sheet["C4"].Text = "Tiền công";
   
            sheet["A4:C4"].CellStyle = tableHeader;


            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }


        public void StyleExcel_ThongTinHangXe(IWorkbook workbook, IWorksheet sheet)
        {


            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");


            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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




            sheet["A1"].Text = "Thông tin hãng xe";
            sheet["A1"].CellStyle = pageHeader;





            sheet["A1:B1"].Merge();


            sheet["A4"].Text = "ID";
            sheet["B4"].Text = "Hãng xe";
           

            sheet["A4:B4"].CellStyle = tableHeader;


            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }


        public void Print_ThongTinHangXe(ObservableCollection<CAR_BRAND> ListCarBrand)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];


                StyleExcel_ThongTinHangXe(workbook, worksheet);


                int i = 5;
                foreach (var item in ListCarBrand)
                {
                    Object[] list = new object[] { item.CarBrand_Id.ToString(), item.CarBrand_Name };
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }

                worksheet.Columns[0].ColumnWidth = 20;
                worksheet.Columns[1].ColumnWidth = 30;
               




                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }

            }
        }


        public void Print_ThongTinTienCong(ObservableCollection<WAGE> ListWage)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];


                StyleExcel_ThongTinTienCong(workbook, worksheet);


                int i = 5;
                foreach (var item in ListWage)
                {
                    Object[] list = new object[] { item.Wage_Id.ToString(), item.Wage_Name, item.Wage_Value };
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }

                worksheet.Columns[0].ColumnWidth = 10;
                worksheet.Columns[1].ColumnWidth = 30;
                worksheet.Columns[2].ColumnWidth = 20;
                



                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }

            }
        }


        public void Print_DanhSachVatTu(ObservableCollection<SUPPLIES> ListSupplies)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];


                StyleExcel_DanhSachVatTu(workbook, worksheet);


                int i = 5;
                foreach (var item in ListSupplies)
                {
                    Object[] list = new object[] { item.Supplies_Id.ToString() , item.Supplies_Name, item.Supplies_Price, item.Supplies_Amount};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }

                worksheet.Columns[0].ColumnWidth = 10;
                worksheet.Columns[1].ColumnWidth = 40;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                


                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }

            }
        }


        public void PrintSalesReport(SalesReport salesReport)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                

                StyleExcel_Sales(workbook, worksheet);
                

                worksheet["B2"].Text = salesReport.IdReport.ToString();
                worksheet["B3"].Text = salesReport.ReportDate.ToString();
                worksheet["B4"].Text = salesReport.UserName;
               

                int i = 6;
                foreach (var item in salesReport.ListSales)
                {
                    Object[] list = new object[] { item.STT.ToString() , item.CarBrand_Name, item.AmountOfTurn, item.TotalMoney
                    , item.Rate};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                worksheet["D" + (i+1)].Text = "Tổng doanh thu: " + salesReport.TotalMoney;
                worksheet["D" + (i + 1) + ":" + "E" + (i + 1)].Merge();

                worksheet.Columns[1].ColumnWidth = 30;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;
                


                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                } 
            }
        }

        public void PrintInventoryReport(InventoryReport inventoryReport)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                // Copy

                StyleExcel_Inventory(workbook, worksheet);
                // này tự m tạo 1 cái hàm khác như trên

                worksheet["B2"].Text = inventoryReport.IdReport.ToString();
                worksheet["B3"].Text = inventoryReport.ReportDate.ToString();
                worksheet["B4"].Text = inventoryReport.UserName;
                //gán dữ liệu vô mấy cái ô trên

                int i = 6;
                foreach (var item in inventoryReport.ListInventory)
                {
                    Object[] list = new object[] { item.STT.ToString() , item.Supplies_Name, item.TonDau, item.PhatSinh
                    , item.TonCuoi};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô

                worksheet.Columns[1].ColumnWidth = 30;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;
                // này là set chiều rộng column


                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }
                // này là save. copy y chang vô
           }
        }
        public void PrintImportBill(ImportBillTemplate p)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(p.All, "Bill");
                    p.Close();
                }
            }
            catch { }
        }
        public void StyleExcel_LichSuKinhDoanh(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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

            sheet["A1"].Text = "Lịch sử kinh doanh";
            sheet["A1"].CellStyle = pageHeader;
            sheet["A1:E1"].Merge();

            sheet["A2"].Text = "Tháng";
            sheet["A3"].Text = "Năm";

            sheet["A4"].Text = "Mã hóa đơn";
            sheet["B4"].Text = "Tên khách hàng";
            sheet["C4"].Text = "Biển số xe";
            sheet["D4"].Text = "Ngày thanh toán";
            sheet["E4"].Text = "Doanh thu";
            sheet["A4:E4"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }
        public void XuatLichSuKinhDoanh(ObservableCollection<ListReceipt> ListReceipt)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel_LichSuKinhDoanh(workbook, worksheet);
                worksheet["B2"].Text = ListReceipt[0].Receipt.ReceiptDate.Month.ToString();
                worksheet["B3"].Text = ListReceipt[0].Receipt.ReceiptDate.ToString();
                int i = 5;
                foreach (var item in ListReceipt)
                {
                    Object[] list = new object[] { item.Receipt.Receipt_Id
                        , item.Customer_Name
                        , item.LicensePlate
                        , item.Receipt.ReceiptDate.Date.ToString("dd/MM/yyyy")
                        , item.Receipt.MoneyReceived.ToString()};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô
                worksheet.Columns[1].ColumnWidth = 30;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;
                // này là set chiều rộng column

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }
                // này là save. copy y chang vô
            }
        }
        public void StyleExcel_LichSuNhapHang(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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

            sheet["A1"].Text = "Lịch sử nhập hàng";
            sheet["A1"].CellStyle = pageHeader;
            sheet["A1:E1"].Merge();

            sheet["A2"].Text = "Tháng";
            sheet["A3"].Text = "Năm";

            sheet["A4"].Text = "Tên vật tư";
            sheet["B4"].Text = "Số lượng";
            sheet["C4"].Text = "Đơn giá";
            sheet["D4"].Text = "Ngày nhập";
            sheet["E4"].Text = "Tổng chi phí";
            sheet["A4:E4"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }
        public void XuatLichSuNhapHang(ObservableCollection<ImportTemp> ListImport)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel_LichSuNhapHang(workbook, worksheet);
                worksheet["B2"].Text = ListImport[0].ImportGoods_Date.Month.ToString();
                worksheet["B3"].Text = ListImport[0].ImportGoods_Date.Year.ToString();
                int i = 5;
                foreach (var item in ListImport)
                {
                    Object[] list = new object[] { item.Supplies_Name
                        , item.ImportInfo.Amount.ToString()
                        , item.ImportInfo.Price.ToString()
                        , item.ImportGoods_Date.Date.ToString("dd/MM/yyyy")
                        , item.ImportInfo.TotalMoney.ToString()};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô
                worksheet.Columns[1].ColumnWidth = 20;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;
                // này là set chiều rộng column

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                }
                // này là save. copy y chang vô
            }
        }
        public void StyleExcel_DanhSachXe(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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

            sheet["A1"].CellStyle = pageHeader;
            sheet["A1:F1"].Merge();

            sheet["A2"].Text = "ID";
            sheet["B2"].Text = "Biển số";
            sheet["C2"].Text = "Hiệu xe";
            sheet["D2"].Text = "Chủ xe";
            sheet["E2"].Text = "Tiền nợ";
            sheet["F2"].Text = "Ngày tiếp nhận";
            sheet["A2:F2"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }
        public void XuatDanhSachXe(ObservableCollection<ListCar> ListCar, string TrangThai)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel_DanhSachXe(workbook, worksheet);
                worksheet["A1"].Text = "Danh sách xe (" + TrangThai + ")";

                int i = 3;
                foreach (var item in ListCar)
                {
                    Object[] list = new object[] { item.CarReception.Reception_Id
                        , item.CarReception.LicensePlate
                        , item.CarReception.CAR_BRAND.CarBrand_Name
                        , item.CarReception.CUSTOMER.Customer_Name
                        , item.CarReception.Debt.ToString(),
                        item.CarReception.ReceptionDate.ToString("dd/MM/yyyy")};
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô
                worksheet.Columns[1].ColumnWidth = 20;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 20;
                worksheet.Columns[4].ColumnWidth = 20;
                // này là set chiều rộng column

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();

                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();
                   
                }
                // này là save. copy y chang vô
            }
        }
        public void StyleExcel_DanhSachNhaCungCap(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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

            sheet["A1"].Text = "Danh sách nhà cung cấp";
            sheet["A1"].CellStyle = pageHeader;
            sheet["A1:D1"].Merge();

            sheet["A2"].Text = "ID";
            sheet["B2"].Text = "Tên nhà cung cấp";
            sheet["C2"].Text = "Số điện thoại";
            sheet["D2"].Text = "Email";
            sheet["A2:D2"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }
        public void XuatDanhSachNhaCungCap(ObservableCollection<SUPPLIER> ListSupplier)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel_DanhSachNhaCungCap(workbook, worksheet);
               

                int i = 3;
                foreach (var item in ListSupplier)
                {
                    Object[] list = new object[] { item.Supplier_Id.ToString()
                        , item.Supplier_Name
                        , item.Supplier_Phone
                        , item.Supplier_Email
                        };
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô
                worksheet.Columns[1].ColumnWidth = 20;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 30;
                // này là set chiều rộng column

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();

                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();

                }
                // này là save. copy y chang vô
            }
        }
        public void StyleExcel_XuatDanhSachDonNhapHang(IWorkbook workbook, IWorksheet sheet)
        {
            IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
            IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

            pageHeader.Color = Syncfusion.Drawing.Color.FromArgb(69, 90, 100);
            pageHeader.Font.RGBColor = Syncfusion.Drawing.Color.White;
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

            sheet["A1"].Text = "Lịch sử hóa đơn nhập hàng";
            sheet["A1"].CellStyle = pageHeader;
            sheet["A1:D1"].Merge();

            sheet["A2"].Text = "Mã đơn nhập";
            sheet["B2"].Text = "Tên nhà cung cấp";
            sheet["C2"].Text = "Ngày nhập hàng";
            sheet["D2"].Text = "Tổng tiền thanh toán";
            sheet["A2:D2"].CellStyle = tableHeader;

            sheet.AutofitColumn(1);
            sheet.UsedRange.AutofitColumns();

        }
        public void XuatDanhSachDonNhapHang(ObservableCollection<IMPORT_GOODS> ListImport)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                StyleExcel_XuatDanhSachDonNhapHang(workbook, worksheet);


                int i = 3;
                foreach (var item in ListImport)
                {
                    Object[] list = new object[] { item.ImportGoods_Id.ToString()
                        , item.SUPPLIER.Supplier_Name
                        , item.ImportGoods_Date.ToString("dd/MM/yyyy")
                        , item.ImportGoods_TotalMoney.ToString()
                        };
                    worksheet.InsertRow(i, 1, ExcelInsertOptions.FormatDefault);
                    worksheet.ImportArray(list, i, 1, false);
                    i++;
                }
                // tạo từng dòng rồi load cái list vô
                worksheet.Columns[1].ColumnWidth = 20;
                worksheet.Columns[2].ColumnWidth = 20;
                worksheet.Columns[3].ColumnWidth = 30;
                // này là set chiều rộng column

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.ShowDialog();

                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    Stream excelStream;
                    application.Application.IgnoreSheetNameException = false;
                    if (File.Exists(Path.GetFullPath(saveFileDialog1.FileName)))
                    {
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName));
                    }
                    else
                        excelStream = File.Create(Path.GetFullPath(saveFileDialog1.FileName + ".xlsx"));
                    workbook.SaveAs(excelStream);
                    excelStream.Dispose();

                }
                // này là save. copy y chang vô
            }
        }
    }
}
