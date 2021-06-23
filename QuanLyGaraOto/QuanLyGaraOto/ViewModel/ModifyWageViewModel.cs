﻿using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ModifyWageViewModel : BaseViewModel
    {
        private string _WageInModify { get; set; }
        public string WageInModify
        {
            get => _WageInModify; set
            {
                _WageInModify = value;
                OnPropertyChanged();
            }
        }
        private int _ValueInModify { get; set; }
        public int ValueInModify
        {
            get => _ValueInModify; set
            {
                _ValueInModify = value;
                OnPropertyChanged();
            }
        }

        public ICommand ModifyWage { get; set; }
        public ICommand CancelModifyWage { get; set; }

        public ModifyWageViewModel(WAGE wage)
        {
            WageInModify = wage.Wage_Name;
            ValueInModify = wage.Wage_Value;

            CancelModifyWage = new RelayCommand<ModifyWageWindow>((p) => { return true; }, (p) => 
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
                
            });
            ModifyWage = new RelayCommand<ModifyWageWindow>((p) => 
            {
                if (p == null || string.IsNullOrEmpty(p.txtWage.Text) || string.IsNullOrEmpty(p.txtValue.Text))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                foreach (WAGE w in DataProvider.Ins.DB.WAGEs)
                {
                    if (w.Wage_Name == WageInModify.Trim() && WageInModify.Trim() != wage.Wage_Name)
                    {
                        MessageBox.Show("Đã tồn tại loại tiền công:" + WageInModify.Trim() + "!");
                        return;
                    }
                }

                wage.Wage_Name = WageInModify;
                wage.Wage_Value = ValueInModify;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Sửa thành công!");
                p.Close();
            });
        }
    }
}
