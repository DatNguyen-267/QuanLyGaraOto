using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class DeleteModel
    {
        public void CarReception(CarReception itemDelete)
        {
            // Xóa bảng Receipt
            var receiptItem = DataProvider.Ins.DB.Receipts.Where
                (x => x.IdCarReception == itemDelete.Id).SingleOrDefault();
            if (receiptItem != null) DataProvider.Ins.DB.Receipts.Remove(receiptItem);
            DataProvider.Ins.DB.SaveChanges();

            // Tìm bảng RepairForm
            var repairFormItem = DataProvider.Ins.DB.RepairForms.Where(
                    x => x.IdCarReception == itemDelete.Id
                    ).SingleOrDefault();

            // Xóa tất cả các item của RepairInfo thuộc RepairForm
            if (repairFormItem != null)
            {
                while (true)
                {
                    var repairInfoItem = DataProvider.Ins.DB.RepairInfoes.Where(
                    x => x.IdRepairForm == repairFormItem.Id
                    ).SingleOrDefault();
                    if (repairInfoItem != null)
                    {
                        DataProvider.Ins.DB.RepairInfoes.Remove(repairInfoItem);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else break;
                }

                // Xóa bảng RepairForm
                DataProvider.Ins.DB.RepairForms.Remove(repairFormItem);
                DataProvider.Ins.DB.SaveChanges();
            }

            DataProvider.Ins.DB.CarReceptions.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(RepairInfo itemDelete)
        {
            DataProvider.Ins.DB.RepairInfoes.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(int ID)
        {
            DataProvider.Ins.DB.RepairInfoes.Remove(
                DataProvider.Ins.DB.RepairInfoes.Where(x => x.Id == ID).SingleOrDefault());
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
