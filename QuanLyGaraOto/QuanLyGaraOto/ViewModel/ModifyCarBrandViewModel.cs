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
    public class ModifyCarBrandViewModel : BaseViewModel
    {
        private string _CarBrandInModify { get; set; }
        public string CarBrandInModify
        {
            get => _CarBrandInModify; set
            {
                _CarBrandInModify = value;
                OnPropertyChanged();
            }
        }


        public ICommand ModifyCarBrand { get; set; }
        public ICommand CancelModifyCarBrand { get; set; }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }
        public ICommand CheckName { get; set; }
        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public ModifyCarBrandViewModel(CAR_BRAND brand)
        {
            IsClose = true;

            CarBrandInModify = brand.CarBrand_Name;

            CancelModifyCarBrand = new RelayCommand<ModifyCarBrandWindow>((p) => { return true; }, (p) =>
            {
              
                    p.Close();
                
            });
            ModifyCarBrand = new RelayCommand<ModifyCarBrandWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtBrand.Text.Trim()))
                {
                    return false;
                }
                if (VisExistsName) return false;
                return true;
            }, (p) =>
            {
                foreach (CAR_BRAND br in DataProvider.Ins.DB.CAR_BRAND)
                {
                    if (br.CarBrand_Name == CarBrandInModify.Trim() && CarBrandInModify.Trim() != brand.CarBrand_Name)
                    {
                        MessageBox.Show("Đã tồn tại hãng xe " + CarBrandInModify.Trim() + "!");
                        return;
                    }
                }

                brand.CarBrand_Name = CarBrandInModify;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Sửa thành công!");
                IsClose = false;
                p.Close();
            });
            CheckName = new RelayCommand<ModifyCarBrandWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.WAGEs.Where(x => x.Wage_Name == p.txtBrand.Text && x.Wage_Id != brand.CarBrand_Id).Count() == 0)
                {
                    VisExistsName = false;
                }
                else VisExistsName = true;
            });
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsClose)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else e.Cancel = true;
            }
            else e.Cancel = false;
        }
    }
}
