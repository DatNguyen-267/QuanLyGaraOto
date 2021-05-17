using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace QuanLyGaraOto.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SupplierCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //ServiceWindow serviceWindow = new ServiceWindow();
                //serviceWindow.ShowDialog();

                //SettingWindow settingWindow = new SettingWindow();
                //settingWindow.ShowDialog();




                //IsLoaded = true;
                //if (p == null)
                //    return;
                //p.Hide();
                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.ShowDialog();
                //if (loginWindow.DataContext == null)
                //    return;
                //var loginVM = loginWindow.DataContext as LoginViewModel;
                //if (loginVM.IsLogin)
                //{
                //    p.Show();
                //}
                //else
                //{

                //    p.Close();
                //}

                //BunkWindow bunkWindow = new BunkWindow();
                //bunkWindow.ShowDialog();
            });
            
            //SupplierCommand = new RelayCommand<object>((p) => { return true; }, p => { BunkWindow wd = new BunkWindow(); wd.ShowDialog(); });
        }
    }
}
