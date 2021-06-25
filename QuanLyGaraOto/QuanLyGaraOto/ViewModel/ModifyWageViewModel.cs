using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ModifyWageViewModel : BaseViewModel
    {
        public ICommand CheckName { get; set; }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }

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
        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public ModifyWageViewModel(WAGE wage)
        {
            WageInModify = wage.Wage_Name;
            ValueInModify = wage.Wage_Value;
            IsClose = true;
            CancelModifyWage = new RelayCommand<ModifyWageWindow>((p) => { return true; }, (p) => 
            {
               
                    p.Close();
                
            });
            ModifyWage = new RelayCommand<ModifyWageWindow>((p) => 
            {
                if (p == null || string.IsNullOrEmpty(p.txtWage.Text) || string.IsNullOrEmpty(p.txtValue.Text))
                {
                    return false;
                }
                if (VisExistsName) return false;
                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch((p.txtValue.Text)) && !string.IsNullOrEmpty((p.txtValue.Text))) return false;
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
                UpdateDebtModel update = new UpdateDebtModel();
                update.UpdateDebtWhenChanged(wage);
                MessageBox.Show("Sửa thành công!");
                IsClose = false;
                p.Close();
            });
            CheckName = new RelayCommand<ModifyWageWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.WAGEs.Where(x => x.Wage_Name == p.txtWage.Text && x.Wage_Id != wage.Wage_Id).Count() == 0)
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
