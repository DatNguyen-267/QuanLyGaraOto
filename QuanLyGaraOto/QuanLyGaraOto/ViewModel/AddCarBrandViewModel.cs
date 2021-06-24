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
    public class AddCarBrandViewModel : BaseViewModel
    {
        private string _CarBrandInAdd;
        public string CarBrandInAdd
        {
            get => _CarBrandInAdd; set
            {
                _CarBrandInAdd = value;
                OnPropertyChanged();
            }
        }
        public CAR_BRAND br { get; set; }

        public ICommand CancelAddCarBrand { get; set; }
        public ICommand AddCarBrand { get; set; }
        public ICommand CheckName { get; set; }

        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }
        public AddCarBrandViewModel()
        {
            IsClose = true;
            CancelAddCarBrand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                    p.Close();
            });


            AddCarBrand = new RelayCommand<AddCarBrandWindow>((p) =>
            {
                if (VisExistsName == true) return false;
                if (p == null || string.IsNullOrEmpty(p.txtBrand.Text.Trim()))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn thêm", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (CAR_BRAND brand in DataProvider.Ins.DB.CAR_BRAND)
                    {
                        if (brand.CarBrand_Name == CarBrandInAdd.Trim())
                        {
                            MessageBox.Show("Đã tồn tại hãng xe " + CarBrandInAdd.Trim() + "!");
                            return;
                        }
                    }

                    br = new CAR_BRAND { CarBrand_Name = CarBrandInAdd.Trim() };
                    DataProvider.Ins.DB.CAR_BRAND.Add(br);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thành công!");
                    IsClose = false;
                    p.Close();
                }

            });
            CheckName = new RelayCommand<AddCarBrandWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.CAR_BRAND.Where(x => x.CarBrand_Name == p.txtBrand.Text).Count() == 0)
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

