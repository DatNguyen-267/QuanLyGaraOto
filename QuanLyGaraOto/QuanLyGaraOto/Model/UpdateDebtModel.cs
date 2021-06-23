using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class UpdateDebtModel
    {
        public void UpdateDebtWhenChanged(SUPPLIES SupplierChanged)
        {
            ObservableCollection<RECEIPT> listReceipt = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            ObservableCollection<RECEPTION> listReception = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
            ObservableCollection<RECEPTION> tempListReception = new ObservableCollection<RECEPTION>();
            ObservableCollection<REPAIR_DETAIL> listRepairDetail = new ObservableCollection<REPAIR_DETAIL>();
            foreach (var item in listReception)
            {
                bool isPay = false;
                foreach (var item2 in listReceipt)
                {
                    if (item.Reception_Id == item2.IdReception)
                    {
                        isPay = true;
                        break;
                    }

                }
                if (!isPay)
                    tempListReception.Add(item);
            }
            // Lấy những hóa đơn chưa thanh toán
            foreach (var item in tempListReception)
            {
                REPAIR Repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == item.Reception_Id).SingleOrDefault();
                listRepairDetail = new ObservableCollection<REPAIR_DETAIL>(DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == Repair.Repair_Id));
                int debt = 0;
                foreach (var item2 in listRepairDetail)
                {
                    int totalMoney = 0;
                    if (item2.IdSupplies != null)
                    {
                        item2.SuppliesPrice = item2.SUPPLIES.Supplies_Price;
                        totalMoney += (int)item2.SUPPLIES.Supplies_Price * (int)item2.SuppliesAmount;
                    }  
                    if (item2.IdWage != null)
                    {
                        item2.WagePrice = item2.WAGE.Wage_Value;
                        totalMoney += item2.WAGE.Wage_Value;
                    }
                    item2.TotalMoney = totalMoney;
                    DataProvider.Ins.DB.SaveChanges();
                    debt += totalMoney;
                }
                item.Debt = debt;
                DataProvider.Ins.DB.SaveChanges();
            }
            
            // Tính toán lại tiền
           
        }
        public void UpdateDebtWhenChanged(WAGE WageChanged)
        {
            ObservableCollection<RECEIPT> listReceipt = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);
            ObservableCollection<RECEPTION> listReception = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
            ObservableCollection<RECEPTION> tempListReception = new ObservableCollection<RECEPTION>();
            ObservableCollection<REPAIR_DETAIL> listRepairDetail = new ObservableCollection<REPAIR_DETAIL>();
            foreach (var item in listReception)
            {
                bool isPay = false;
                foreach (var item2 in listReceipt)
                {
                    if (item.Reception_Id == item2.IdReception)
                    {
                        isPay = true;
                        break;
                    }

                }
                if (!isPay)
                    tempListReception.Add(item);
            }
            // Lấy những hóa đơn chưa thanh toán
            foreach (var item in tempListReception)
            {
                REPAIR Repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == item.Reception_Id).SingleOrDefault();
                listRepairDetail = new ObservableCollection<REPAIR_DETAIL>(DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == Repair.Repair_Id));
                int debt = 0;
                foreach (var item2 in listRepairDetail)
                {
                    int totalMoney = 0;
                    if (item2.IdSupplies != null)
                    {
                        item2.SuppliesPrice = item2.SUPPLIES.Supplies_Price;
                        totalMoney += (int)item2.SUPPLIES.Supplies_Price * (int)item2.SuppliesAmount;
                    }
                    if (item2.IdWage != null)
                    {
                        item2.WagePrice = item2.WAGE.Wage_Value;
                        totalMoney += item2.WAGE.Wage_Value;
                    }
                    item2.TotalMoney = totalMoney;
                    DataProvider.Ins.DB.SaveChanges();
                    debt += totalMoney;
                }
                item.Debt = debt;
                DataProvider.Ins.DB.SaveChanges();
            }

            // Tính toán lại tiền
        }
    }
}
