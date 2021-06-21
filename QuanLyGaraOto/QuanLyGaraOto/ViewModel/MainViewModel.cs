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
        private MainWindow mainWindow { get; set; }

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

        bool _isServiceWindow = false;
        public bool isServiceWindow { get => _isServiceWindow; set { _isServiceWindow = value; OnPropertyChanged(); } }

        bool _isBunkWindow = false;
        public bool isBunkWindow { get => _isBunkWindow; set { _isBunkWindow = value; OnPropertyChanged(); } }

        bool _isImportBunk = false;
        public bool isImportBunk { get => _isImportBunk; set { _isImportBunk = value; OnPropertyChanged(); } }

        bool _isEmployeeWindow = false;
        public bool isEmployeeWindow { get => _isEmployeeWindow; set { _isEmployeeWindow = value; OnPropertyChanged(); } }

        bool _isAddEmployee = false;
        public bool isAddEmployee { get => _isAddEmployee; set { _isAddEmployee = value; OnPropertyChanged(); } }

        bool _isAddRole = false;
        public bool isAddRole { get => _isAddRole; set { _isAddRole = value; OnPropertyChanged(); } }

        bool _isReportWindow = false;
        public bool isReportWindow { get => _isReportWindow; set { _isReportWindow = value; OnPropertyChanged(); } }

        bool _isReport = false;
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }

        bool _isGaraInfo = false;
        public bool isGaraInfo { get => _isGaraInfo; set { _isGaraInfo = value; OnPropertyChanged(); } }

        bool _isWage = false;
        public bool isWage { get => _isWage; set { _isWage = value; OnPropertyChanged(); } }

        bool _isCarBranch = false;
        public bool isCarBranch { get => _isCarBranch; set { _isCarBranch = value; OnPropertyChanged(); } }

        bool _isSuplier = false;
        public bool isSuplier { get => _isSuplier; set { _isSuplier = value; OnPropertyChanged(); } }

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
                if (isServiceWindow == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisService = true;

            });
            OpenEmployee = new RelayCommand<Employee>((p) => 
            {
                if (isEmployeeWindow == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisEmployee = true;
                p.DataContext = new EmployeeViewModel(isAddEmployee, isAddRole,User.UserName);

            });
            OpenBunk = new RelayCommand<BunkWindow>((p) => 
            {
                if (isBunkWindow == false)
                    return false;
                return true; 
            }, 
            (p) =>
            {
                InitVis();
                VisBunk = true;
                p.DataContext = new BunkViewModel(isImportBunk);

            });
            OpenReport = new RelayCommand<ReportWindow>((p) => 
            {
                if (isReportWindow == false)
                    return false;
                return true; 
            },
            (p) =>
            {
                InitVis();
                VisReport = true;
                p.DataContext = new ReportViewModel(isReport,User);

            });
            OpenSetting = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                InitVis();
                VisSetting = true;
                p.DataContext = new SettingViewModel(User.UserName,isGaraInfo,isWage,isCarBranch,isSuplier,mainWindow);
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
                mainWindow = p;
                

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission == true)
                    isServiceWindow = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission == true)
                    isBunkWindow = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission == true)
                    isImportBunk = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission == true)
                    isEmployeeWindow = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission == true)
                    isAddEmployee = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission == true)
                    isAddRole = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission == true)
                    isReportWindow = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 8).SingleOrDefault().Permission == true)
                    isReport = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 9).SingleOrDefault().Permission == true)
                    isGaraInfo = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 10).SingleOrDefault().Permission == true)
                    isWage = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 11).SingleOrDefault().Permission == true)
                    isCarBranch = true;

                if (User.ROLE.ROLE_DETAIL.Where(x => x.IdPermissionItem == 12).SingleOrDefault().Permission == true)
                    isSuplier = true;
            }
            else
            {
                p.Close();
            }
        }
    }
}
