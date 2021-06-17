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
        public CAR_BRAND br;

        public ICommand CancelAddCarBrand { get; set; }
        public ICommand AddCarBrand { get; set; }

        public AddCarBrandViewModel()
        {
            CancelAddCarBrand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddCarBrand = new RelayCommand<AddCarBrandWindow>((p) => 
            { 
                if(p == null || string.IsNullOrEmpty(p.txtBrand.Text.Trim()))
                {
                    return false;
                }
                return true; 
            }, (p) =>
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
                p.Close();
            });
        }
    }
}
