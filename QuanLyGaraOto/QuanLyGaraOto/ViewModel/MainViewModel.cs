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
    public class MainViewModel: BaseViewModel
    {
        public ICommand LoadedWindowCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<object>((p)=> { return true; }, (p)=> {
                //ServiceWindow serviceWindow = new ServiceWindow();
                //serviceWindow.ShowDialog();

                //SettingWindow settingWindow = new SettingWindow();
                //settingWindow.ShowDialog();

                PayWindow settingWindow = new PayWindow();
                settingWindow.ShowDialog();
            });
        }
    }
}
