using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public WAGE wage;

        public ICommand CancelAddWageWindow { get; set; }
        public ICommand AddWage { get; set; }

        public AddWageViewModel()
        {
            CancelAddWageWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddWage = new RelayCommand<AddWageWindow>((p) =>
            {
                if(p == null || string.IsNullOrEmpty(p.txtWage.Text) || string.IsNullOrEmpty(p.txtValue.Text))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                foreach(WAGE w in DataProvider.Ins.DB.WAGEs)
                {
                    if(w.Wage_Name == WageInAdd.Trim())
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
            });
        }
    }
}
