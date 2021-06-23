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
    public class AddNewGoodViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public SUPPLIES Supplies { get; set; }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private int _Price { get; set; }
        public int Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }


        public AddNewGoodViewModel()
        {

            AddCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                {
                    var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                    if (List == null || List.Count() != 0) return false;
                    if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbPrice.Text) || Int32.Parse(p.txbPrice.Text) == 0)
                        return false;
                    return true;

                },
                (p) =>
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thêm phụ tùng", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Supplies = new SUPPLIES() { Supplies_Name = p.txbName.Text, Supplies_Price = Int32.Parse(p.txbPrice.Text), Supplies_Amount = 0 };
                        DataProvider.Ins.DB.SUPPLIES.Add(Supplies);
                        DataProvider.Ins.DB.SaveChanges();
                        p.Close();
                    }
                });
            CloseCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                { return true; },
                (p) =>
                {
                    p.Close();
                }
                );
        }
        public AddNewGoodViewModel(SUPPLIES supplies)
        {
            Name = supplies.Supplies_Name;
            Price = (int)supplies.Supplies_Price;
            EditCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                {
                    var Temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                    if ((Temp == null || Temp.Count() != 0) && p.txbName.Text != supplies.Supplies_Name) return false;
                    if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbPrice.Text) || Int32.Parse(p.txbPrice.Text) == 0)
                        return false;
                    if (supplies == null) return false;
                    return true;

                },
                (p) =>
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin phụ tùng", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var Temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                        if ((Temp == null || Temp.Count() != 0) && p.txbName.Text != supplies.Supplies_Name) return;
                        var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Id == supplies.Supplies_Id).SingleOrDefault();
                        List.Supplies_Name = p.txbName.Text;
                        List.Supplies_Price = Int32.Parse(p.txbPrice.Text);
                        DataProvider.Ins.DB.SaveChanges();
                        OnPropertyChanged("List");
                        UpdateDebtModel update = new UpdateDebtModel();
                        update.UpdateDebtWhenChanged(supplies);
                        p.Close();
                    }
                }
                );
            CloseCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                { return true; },
                (p) =>
                {
                    p.Close();
                }
                );
        }
    }
}
