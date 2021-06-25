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
    public class AddNewGoodViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CheckName { get; set; }

        public SUPPLIES Supplies { get; set; }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private int _Price { get; set; }
        public int Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private bool _check { get; set; }
        public bool check { get => _check; set { _check = value; OnPropertyChanged(); } }
        public string Title { get; set; }
        private bool _IsClose { get; set; }
        public bool IsClose { get => _IsClose; set { _IsClose = value; OnPropertyChanged(); } }
        private bool _VisExistsName { get; set; }
        public bool VisExistsName { get => _VisExistsName; set { _VisExistsName = value; OnPropertyChanged(); } }
        public AddNewGoodViewModel()
        {
            Title = "Thêm phụ tùng";
            check = false;
            IsClose = true;
            AddCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                {
                    var List = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                    if (List == null || List.Count() != 0) return false;
                    if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbPrice.Text) )
                        return false;
                    if (VisExistsName) return false;
                    Regex regex = new Regex(@"^[0-9]+$");
                    if (!regex.IsMatch((p.txbPrice.Text)) && !string.IsNullOrEmpty((p.txbPrice.Text))) return false;

                    return true;
                },
                (p) =>
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thêm phụ tùng", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Supplies = new SUPPLIES() { Supplies_Name = p.txbName.Text, Supplies_Price = Int32.Parse(p.txbPrice.Text), Supplies_Amount = 0 };
                        DataProvider.Ins.DB.SUPPLIES.Add(Supplies);
                        DataProvider.Ins.DB.SaveChanges();
                        check = true;
                        IsClose = false;
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
            CheckName = new RelayCommand<AddNewGoodWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text).Count() == 0)
                {
                    VisExistsName = false;
                }
                else VisExistsName = true;
            });
        }
        public AddNewGoodViewModel(SUPPLIES supplies)
        {
            Title = "Sửa đổi phụ tùng";
            Name = supplies.Supplies_Name;
            Price = (int)supplies.Supplies_Price;
            EditCommand = new RelayCommand<AddNewGoodWindow>(
                (p) =>
                {
                    if (VisExistsName) return false;
                    var Temp = DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text);
                    if ((Temp == null || Temp.Count() != 0) && p.txbName.Text != supplies.Supplies_Name) return false;
                    if (string.IsNullOrEmpty(p.txbName.Text) || string.IsNullOrEmpty(p.txbPrice.Text) )
                        return false;

                    Regex regex = new Regex(@"^[0-9]+$");
                    if (!regex.IsMatch((p.txbPrice.Text)) && !string.IsNullOrEmpty((p.txbPrice.Text))) return false;

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
                        IsClose = false;
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
            CheckName = new RelayCommand<AddNewGoodWindow>((p) => { return true; }, (p) =>
            {
                VisExistsName = false;
                if (DataProvider.Ins.DB.SUPPLIES.Where(x => x.Supplies_Name == p.txbName.Text).Count() == 0)
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
