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
    public class MainViewModel: BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ServiceWindow Service { get; set; }
        public Employee Employee { get; set; }
        public BunkWindow Bunk { get; set; }
        public SettingWindow Setting { get; set; }
        public ReportWindow Report { get; set; }
        public Grid Container { get; set; }
        public UserControl CurrentWindow { get; set; }
        public ICommand OpenService { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenBunk { get; set; }
        public ICommand OpenSetting { get; set; }
        public ICommand OpenReport { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Container = (p as MainWindow).Container;
                InitData();
                LoadLoginWindow();
            });
            OpenService = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Service.Visibility = Visibility.Visible;
                if (CurrentWindow != null) CurrentWindow.Visibility = Visibility.Hidden;
                CurrentWindow = Service;
            });
            OpenEmployee = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Employee.Visibility = Visibility.Visible;
                if (CurrentWindow != null) CurrentWindow.Visibility = Visibility.Hidden;
                CurrentWindow = Employee;
            });
        }
        public void InitData()
        {
            Service = new ServiceWindow();
            Employee = new Employee();
            Bunk = new BunkWindow();
            Setting = new SettingWindow();
            Report = new ReportWindow();

            Service.Height = Double.NaN;
            Employee.Height = Double.NaN;
            Bunk.Height = Double.NaN;
            Setting.Height = Double.NaN;
            Report.Height = Double.NaN;
            Service.Width = Double.NaN;
            Employee.Width = Double.NaN;
            Bunk.Width = Double.NaN;
            Setting.Width = Double.NaN;
            Report.Width = Double.NaN;

            Container.Children.Add(Service);
            Container.Children.Add(Employee);
            Container.Children.Add(Bunk);
            Container.Children.Add(Setting);
            Container.Children.Add(Report);

            Service.Visibility = Visibility.Visible;
            Employee.Visibility = Visibility.Hidden;
            Bunk.Visibility = Visibility.Hidden;
            Setting.Visibility = Visibility.Hidden;
            Report.Visibility = Visibility.Hidden;
        }
        public void LoadLoginWindow()
        {
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
        }
    }
}
