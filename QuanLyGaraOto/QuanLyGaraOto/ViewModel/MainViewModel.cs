using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLyGaraOto.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public Grid Container { get; set; }

        public ICommand OpenDashboard { get; set; }
        public ICommand OpenService { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenBunk { get; set; }
        public ICommand OpenSetting { get; set; }
        public ICommand OpenReport { get; set; }
        public bool _VisDashboard { get; set; }
        public bool VisDashboard { get => _VisDashboard; set { _VisDashboard = value; OnPropertyChanged(); } }
        public bool _VisService { get; set; }
        public bool VisService { get => _VisService; set { _VisService = value; OnPropertyChanged(); } }
        public bool _VisBunk { get; set; }
        public bool VisBunk { get => _VisBunk; set { _VisBunk = value; OnPropertyChanged(); } }
        public bool _VisEmployee { get; set; }
        public bool VisEmployee { get => _VisEmployee; set { _VisEmployee = value; OnPropertyChanged(); } }
        public bool _VisReport { get; set; }
        public bool VisReport { get => _VisReport; set { _VisReport = value; OnPropertyChanged(); } }
        public bool _VisSetting { get; set; }
        public bool VisSetting { get => _VisSetting; set { _VisSetting = value; OnPropertyChanged(); } }
        public bool _CurrentWindow { get; set; }
        public bool CurrentWindow { get => _CurrentWindow; set { _CurrentWindow = value; OnPropertyChanged(); } }

        private USER _User;
        public USER User { get => _User; set { _User = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            InitVis();
            LoadedWindowCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Container = (p as MainWindow).Container;

                LoadLoginWindow(p);
            });
            OpenDashboard = new RelayCommand<DashboardWindow>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisDashboard = true;
                p.DataContext = new DashboardViewModel();
            });
            OpenService = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisService = true;

            });
            OpenEmployee = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisEmployee = true;

            });
            OpenBunk = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisBunk = true;

            });
            OpenReport = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisReport = true;

            });
            OpenSetting = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisSetting = true;
                p.DataContext = new SettingViewModel(User.UserName);
            });
        }
        public void InitVis()
        {
            VisDashboard = false;
            VisService = false;
            VisBunk = false;
            VisReport = false;
            VisEmployee = false;
            VisSetting = false;
        }
        public void LoadLoginWindow(MainWindow p)
        {
            IsLoaded = true;
            if (p == null)
                return;
            p.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            if (loginWindow.DataContext == null)
                return;
            var loginVM = loginWindow.DataContext as LoginViewModel;
            if (loginVM.IsLogin)
            {
                p.Show();
                User = loginVM.User;
            }
            else
            {
                p.Close();
            }
        }
    }
}
