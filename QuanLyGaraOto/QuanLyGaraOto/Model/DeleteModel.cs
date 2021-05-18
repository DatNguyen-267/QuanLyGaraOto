using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class DeleteModel
    {
        public void CarReception(CARRECEPTION itemDelete)
        {
            // Xóa bảng Receipt
            var receiptItem = DataProvider.Ins.DB.RECEIPTs.Where
                (x => x.IdCarReception == itemDelete.Id).SingleOrDefault();
            if (receiptItem != null) DataProvider.Ins.DB.RECEIPTs.Remove(receiptItem);
            DataProvider.Ins.DB.SaveChanges();

            // Tìm bảng RepairForm
            var repairFormItem = DataProvider.Ins.DB.REPAIRFORMs.Where(
                    x => x.IdCarReception == itemDelete.Id
                    ).SingleOrDefault();

            // Xóa tất cả các item của RepairInfo thuộc RepairForm
            if (repairFormItem != null)
            {
                while (true)
                {
                    var repairInfoItem = DataProvider.Ins.DB.REPAIRINFOes.Where(
                    x => x.IdRepairForm == repairFormItem.Id
                    ).SingleOrDefault();
                    if (repairInfoItem != null)
                    {
                        DataProvider.Ins.DB.REPAIRINFOes.Remove(repairInfoItem);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else break;
                }

                // Xóa bảng RepairForm
                DataProvider.Ins.DB.REPAIRFORMs.Remove(repairFormItem);
                DataProvider.Ins.DB.SaveChanges();
            }

            DataProvider.Ins.DB.CARRECEPTIONs.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(REPAIRINFO itemDelete)
        {
            DataProvider.Ins.DB.REPAIRINFOes.Remove(itemDelete);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void RepairInfo(int ID)
        {
            DataProvider.Ins.DB.REPAIRINFOes.Remove(
                DataProvider.Ins.DB.REPAIRINFOes.Where(x => x.Id == ID).SingleOrDefault());
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
