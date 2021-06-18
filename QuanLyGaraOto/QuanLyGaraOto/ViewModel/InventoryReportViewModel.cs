using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class InventoryReportViewModel:BaseViewModel
    {
        private InventoryReport _InventoryReport { get; set; }
        public InventoryReport InventoryReport{ get => _InventoryReport; set { _InventoryReport = value; OnPropertyChanged(); } }
        public InventoryReportViewModel(InventoryReport inventoryReport)
        {
            InventoryReport = inventoryReport;
            
        }
    }
}
