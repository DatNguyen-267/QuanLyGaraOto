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

        public ModifyCarBrandViewModel(CAR_BRAND brand)
        {
            CarBrandInModify = brand.CarBrand_Name;

            CancelModifyCarBrand = new RelayCommand<ModifyCarBrandWindow>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
                
            });
            ModifyCarBrand = new RelayCommand<ModifyCarBrandWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtBrand.Text.Trim()))
                {
                    return false;
                }
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
                p.Close();
            });
        }
    }
}
