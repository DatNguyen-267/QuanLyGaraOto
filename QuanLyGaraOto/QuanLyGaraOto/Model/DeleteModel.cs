using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class DeleteModel
    {
        public void CarReception(RECEPTION itemDelete)
        {
            // Xóa bảng Receipt
            var receiptItem = DataProvider.Ins.DB.RECEIPTs.Where
                (x => x.IdReception == itemDelete.Reception_Id).SingleOrDefault();
            if (receiptItem != null) DataProvider.Ins.DB.RECEIPTs.Remove(receiptItem);
            DataProvider.Ins.DB.SaveChanges();

            // Tìm bảng RepairForm
            var repairFormItem = DataProvider.Ins.DB.REPAIRs.Where(
                    x => x.IdReception == itemDelete.Reception_Id
                    ).SingleOrDefault();

            // Xóa tất cả các item của RepairInfo thuộc RepairForm
            if (repairFormItem != null)
            {
                ObservableCollection<REPAIR_DETAIL> RepairDetails = new ObservableCollection<REPAIR_DETAIL>(DataProvider.Ins.DB.REPAIR_DETAIL.Where(
                    x => x.IdRepair == repairFormItem.Repair_Id));
                if (RepairDetails!= null)
                {
                    foreach (var item in RepairDetails)
                    {
                        DataProvider.Ins.DB.REPAIR_DETAIL.Remove(item);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                // Xóa bảng RepairForm
                DataProvider.Ins.DB.REPAIRs.Remove(repairFormItem);
                DataProvider.Ins.DB.SaveChanges();
            }
            DataProvider.Ins.DB.RECEPTIONs.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(REPAIR_DETAIL itemDelete)
        {
            DataProvider.Ins.DB.REPAIR_DETAIL.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(int ID)
        {
            DataProvider.Ins.DB.REPAIR_DETAIL.Remove(
                DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.RepairDetail_Id == ID).SingleOrDefault());
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
