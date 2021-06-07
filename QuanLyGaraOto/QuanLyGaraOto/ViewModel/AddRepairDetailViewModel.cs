﻿using Prism.Services.Dialogs;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AddRepairDetailViewModel:BaseViewModel
    {
        private ObservableCollection<SUPPLIES> _ListSupply { get; set; }
        public ObservableCollection<SUPPLIES> ListSupply { get => _ListSupply; set { _ListSupply = value; OnPropertyChanged(); } }
        private ObservableCollection<WAGE> _ListPay { get; set; }
        public ObservableCollection<WAGE> ListPay { get => _ListPay; set { _ListPay = value; OnPropertyChanged(); } }
        private List<int> _ListAmount { get; set; }
        public List<int> ListAmount { get => _ListAmount; set { _ListAmount = value; OnPropertyChanged(); } }
        private string _Content { get; set; }
        public string Content { get => _Content; set { _Content = value; OnPropertyChanged(); } }
        private SUPPLIES _TempSupplies { get; set; }
        public SUPPLIES TempSupplies { get => _TempSupplies; set { _TempSupplies = value; } }
        private SUPPLIES _SelectedSupply { get; set; }
        public SUPPLIES SelectedSupply { get => _SelectedSupply; set { _SelectedSupply = value; 
                if (_SelectedSupply != null)
                {
                    ListAmount = new List<int>();
                    if (IsAdd)
                    {
                        ListAmount = Enumerable.Range(1, (int)SelectedSupply.Supplies_Amount).ToList();
                    } else if (RepairDetail != null && TempSupplies.Supplies_Name == SelectedSupply.Supplies_Name)
                    {
                        ListAmount = Enumerable.Range(1, (int)SelectedSupply.Supplies_Amount + (int)TempSupplies.Supplies_Amount).ToList();
                    } else
                    {
                        ListAmount = Enumerable.Range(1, (int)SelectedSupply.Supplies_Amount).ToList();
                    }
                    
                }
                OnPropertyChanged(); } }
        private WAGE _SelectedPay { get; set; }
        public WAGE SelectedPay { get => _SelectedPay; set { _SelectedPay = value; OnPropertyChanged(); } }
        private Nullable<int> _SelectedAmount { get; set; }
        public Nullable<int> SelectedAmount { get => _SelectedAmount; set { _SelectedAmount = value; OnPropertyChanged(); } }
        private Nullable<int> _TempAmount { get; set; }
        public Nullable<int> TempAmount { get => _TempAmount; set { _TempAmount = value; OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
        private REPAIR_DETAIL _RepairDetail { get; set; }
        public REPAIR_DETAIL RepairDetail { get => _RepairDetail; set { _RepairDetail = value;} }
        private REPAIR _Repair { get; set; }
        public REPAIR Repair { get => _Repair; set { _Repair = value; OnPropertyChanged(); } }

        public ICommand UpdateValue { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ResetNumner { get; set; }
        private REPAIR_DETAIL _ReturnRepairDetail { get; set; }
        public REPAIR_DETAIL ReturnRepairDetail { get => _ReturnRepairDetail; set { _ReturnRepairDetail = value; OnPropertyChanged(); } }

        public bool IsAdd { get; set; }
        public AddRepairDetailViewModel(REPAIR_DETAIL repair_Detail)
        {
            this.RepairDetail = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x=>x.RepairDetail_Id == repair_Detail.RepairDetail_Id).SingleOrDefault();
            IsAdd = false;
            LoadRepairDetail();
            LoadData();
            Command();
        }
        public AddRepairDetailViewModel(REPAIR Repair)
        {
            this.Repair = Repair;
            IsAdd = true;
            LoadData();
            Command();
        }
        public void LoadData()
        {
            ListPay = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
            ListSupply = new ObservableCollection<SUPPLIES>(DataProvider.Ins.DB.SUPPLIES);
            ListAmount = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                ListAmount.Add(i);
            }
            if (RepairDetail!= null)
            {
                ListAmount = new List<int>();
                ListAmount = Enumerable.Range(1, (int)RepairDetail.SUPPLIES.Supplies_Amount+(int)RepairDetail.SuppliesAmount).ToList();
            }
        }
        public void LoadRepairDetail()
        {
            TempAmount = RepairDetail.SuppliesAmount;
            TempSupplies = new SUPPLIES() { Supplies_Name = RepairDetail.SUPPLIES.Supplies_Name, Supplies_Amount = RepairDetail.SuppliesAmount };

            Content = RepairDetail.Content;
            SelectedSupply = RepairDetail.SUPPLIES;
            SelectedPay = RepairDetail.WAGE;
            SelectedAmount = RepairDetail.SuppliesAmount;
            TotalMoney = RepairDetail.TotalMoney;

            
        }
        public void Command()
        {
            UpdateValue = new RelayCommand<Object>(
               (p) => {
                   if (SelectedSupply == null || SelectedAmount == null || SelectedPay == null
                   ) return false;
                   return true;
               },
               (p) =>
               {
                   Calculate();
               });
            
            ConfirmCommand = new RelayCommand<Window>(
               (p) => {
                   if (TotalMoney == 0
                   ) return false;
                   return true;
               },
                (p) =>
                {

                    if (IsAdd)
                    {
                        MessageBoxResult rs = MessageBox.Show("Bạn đồng ý thêm", "Thêm thông tin sửa chữa", MessageBoxButton.OKCancel);
                        if (MessageBoxResult.OK == rs)
                        {
                            ReturnRepairDetail = new REPAIR_DETAIL()
                            {
                                Content = Content,
                                IdWage = SelectedPay.Wage_Id,
                                IdRepair = Repair.Repair_Id,
                                IdSupplies = SelectedSupply.Supplies_Id,
                                SuppliesPrice = SelectedSupply.Supplies_Price,
                                SuppliesAmount = SelectedAmount,
                                TotalMoney = TotalMoney
                            };
                            DataProvider.Ins.DB.REPAIR_DETAIL.Add(ReturnRepairDetail);
                            var temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == ReturnRepairDetail.IdSupplies).SingleOrDefault();
                            temp.Supplies_Amount = temp.Supplies_Amount - ReturnRepairDetail.SuppliesAmount;
                            DataProvider.Ins.DB.SaveChanges();
                            p.Close();
                        }
                    }
                    else
                    {
                        MessageBoxResult rs = MessageBox.Show("Bạn đồng ý sửa", "Sửa thông tin sửa chữa", MessageBoxButton.OKCancel);
                        if (MessageBoxResult.OK == rs)
                        {
                            ReturnRepairDetail = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.RepairDetail_Id == RepairDetail.RepairDetail_Id).SingleOrDefault();
                            ReturnRepairDetail.Content = Content;
                            ReturnRepairDetail.IdSupplies = SelectedSupply.Supplies_Id;
                            ReturnRepairDetail.SuppliesPrice = SelectedSupply.Supplies_Price;
                            ReturnRepairDetail.IdWage = SelectedPay.Wage_Id;
                            ReturnRepairDetail.SuppliesAmount = SelectedAmount;
                            ReturnRepairDetail.TotalMoney = TotalMoney;

                            if (DataProvider.Ins.DB.SUPPLIES.Where(x=> x.Supplies_Id == ReturnRepairDetail.IdSupplies).SingleOrDefault().Supplies_Name == TempSupplies.Supplies_Name)
                            {
                                var temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == ReturnRepairDetail.IdSupplies).SingleOrDefault();
                                temp.Supplies_Amount = temp.Supplies_Amount + TempSupplies.Supplies_Amount - ReturnRepairDetail.SuppliesAmount;
                                DataProvider.Ins.DB.SaveChanges();
                            } else
                            {
                                var temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == ReturnRepairDetail.IdSupplies).SingleOrDefault();
                                temp.Supplies_Amount = temp.Supplies_Amount - ReturnRepairDetail.SuppliesAmount;

                                var temp2 = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == TempSupplies.Supplies_Name).SingleOrDefault();
                                temp2.Supplies_Amount = temp2.Supplies_Amount + TempSupplies.Supplies_Amount;
                                DataProvider.Ins.DB.SaveChanges();
                            }
                            DataProvider.Ins.DB.SaveChanges();
                            p.Close();
                        }
                    }
                });
            CloseCommand = new RelayCommand<Window>(
               (p) => { return true;},
               (p) => {   p.Close(); });
        }
        public void Calculate()
        {
            TotalMoney = (int)SelectedSupply.Supplies_Price * (int)SelectedAmount + (int)SelectedPay.Wage_Value;
        }
    }
}
