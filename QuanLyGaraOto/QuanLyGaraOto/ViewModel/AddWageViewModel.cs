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
    public class AddWageViewModel : BaseViewModel
    {
        private string _WageInAdd;
        public string WageInAdd
        {
            get => _WageInAdd; set
            {
                _WageInAdd = value;
                OnPropertyChanged();
            }
        }
        private int _ValueInAdd;
        public int ValueInAdd
        {
            get => _ValueInAdd; set
            {
                _ValueInAdd = value;
                OnPropertyChanged();
            }
        }
        public WAGE wage { get; set; }

        public ICommand CancelAddWage { get; set; }
        public ICommand AddWage { get; set; }
        public ICommand CheckName { get; set; }

        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public AddWageViewModel()
        {
            CancelAddWage = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
               
                    p.Close();
            });
            AddWage = new RelayCommand<AddWageWindow>((p) =>
            {
                if (VisExistsName == true) return false;
                if (p == null || string.IsNullOrEmpty(p.txtWage.Text) || string.IsNullOrEmpty(p.txtValue.Text))
                {
                    return false;
                }
                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch((p.txtValue.Text)) && !string.IsNullOrEmpty((p.txtValue.Text))) return false;

                return true;
            }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn thêm", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (WAGE w in DataProvider.Ins.DB.WAGEs)
                    {
                        if (w.Wage_Name == WageInAdd.Trim())
                        {
                            MessageBox.Show("Đã tồn tại loại tiền công:" + WageInAdd.Trim() + "!");
                            return;
                        }
                    }

                    wage = new WAGE { Wage_Name = WageInAdd.Trim(), Wage_Value = ValueInAdd };
                    DataProvider.Ins.DB.WAGEs.Add(wage);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công!");
                    p.Close();
                }
            });
            CheckName = new RelayCommand<AddWageWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.WAGEs.Where(x => x.Wage_Name == p.txtWage.Text).Count() == 0)
                {
                    VisExistsName = false;
                }
                else VisExistsName = true;
            });
        }
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}

