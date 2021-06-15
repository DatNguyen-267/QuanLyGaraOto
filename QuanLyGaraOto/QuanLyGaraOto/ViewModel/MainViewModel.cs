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

        bool _isDashboard = false;
        public bool isDashboard { get => _isDashboard; set { _isDashboard = value; OnPropertyChanged(); } }

        bool _isService = false;
        public bool isService { get => _isService; set { _isService = value; OnPropertyChanged(); } }

        bool _isBunk = false;
        public bool isBunk { get => _isBunk; set { _isBunk = value; OnPropertyChanged(); } }

        bool _isEmployee = false;
        public bool isEmployee { get => _isEmployee; set { _isEmployee = value; OnPropertyChanged(); } }

        bool _isReport = false;
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }

        bool _isSettingAccount = false;
        public bool isSettingAccount { get => _isSettingAccount; set { _isSettingAccount = value; OnPropertyChanged(); } }

        bool _isSettingApp = false;
        public bool isSettingApp { get => _isSettingApp; set { _isSettingApp = value; OnPropertyChanged(); } }

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
            OpenService = new RelayCommand<object>((p) => 
            {
                if (isService == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisService = true;

            });
            OpenEmployee = new RelayCommand<object>((p) => 
            {
                if (isEmployee == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisEmployee = true;

            });
            OpenBunk = new RelayCommand<object>((p) => 
            {
                if (isBunk == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisBunk = true;

            });
            OpenReport = new RelayCommand<object>((p) => 
            {
                if (isReport == false)
                    return false;
                return true; 
            },
            (p) =>
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

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission == true)
                    isDashboard = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission == true)
                    isService = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission == true)
                    isBunk = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission == true)
                    isEmployee = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission == true)
                    isReport = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission == true)
                    isSettingAccount = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission == true)
                    isSettingApp = true;
            }
            else
            {
                p.Close();
            }
        }
    }
}
