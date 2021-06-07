using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.Model;
using System.Windows.Input;
using System.Windows.Data;
using QuanLyGaraOto.Properties;
using System.Data;
using System.Windows;

namespace QuanLyGaraOto.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand LookUpCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand ButtonCommand { get; set; }
        public ICommand EditWindowCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }

        public ObservableCollection<USER_INFO> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public USER_INFO SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.UserInfo_Name;
                    BrithDate = SelectedItem.UserInfo_BirthDate;
                    CMND = SelectedItem.UserInfo_CMND;
                    Telephone = SelectedItem.UserInfo_Telephone;
                    Address = SelectedItem.UserInfo_Address;
                    if (SelectedItem.IdUser == null)
                    {
                        UserName = "Chưa có tài khoản";
                    }
                    else
                    {
                        var Account = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == SelectedItem.IdUser).SingleOrDefault();
                        UserName = Account.UserName.ToString();
                    }
                    Id = SelectedItem.UserInfo_Id;
                }
            }
        }

        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        public DateTime? BrithDate { get => _BrithDate; set { _BrithDate = value; OnPropertyChanged(); } }
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        public string Telephone { get => _Telephone; set { _Telephone = value; OnPropertyChanged(); } }
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
        public object Datatable { get; private set; }
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public int IdUser { get => _IdUser; set { _IdUser = value; OnPropertyChanged(); } }

        private ObservableCollection<USER_INFO> _List;

        private USER_INFO _SelectedItem;

        private string _Name;

        private DateTime? _BrithDate;

        private string _CMND;

        private string _Telephone;

        private string _Address;

        private string _UserName;

        private int _Id;

        private int _IdUser;

        public EmployeeViewModel(USER_INFO u)
        {
            Name = u.UserInfo_Name;
            Id = u.UserInfo_Id;
            BrithDate = u.UserInfo_BirthDate;
            CMND = u.UserInfo_CMND;
            Telephone = u.UserInfo_Telephone;
            Address = u.UserInfo_Address;
            if (u.IdUser == null)
            {
                UserName = null;
            }
            else
            {
                var Account = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == u.IdUser).SingleOrDefault();
                UserName = Account.UserName.ToString();
                IdUser = Account.Users_Id;
            }

            //Nút sửa thông tin trên cửa sổ Edit
            EditEmployeeCommand = new RelayCommand<Window>((p) =>
            {
                if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(BrithDate.ToString()) || String.IsNullOrEmpty(CMND) || String.IsNullOrEmpty(Telephone) || String.IsNullOrEmpty(Address))
                {
                    return false;
                }
                return true;
            }, (p) => {
                if (MessageBox.Show("Bạn chắc chắn muốn sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == System.Windows.MessageBoxResult.OK)
                {
                    var in4 = DataProvider.Ins.DB.USER_INFO.Where(x => x.UserInfo_Id == Id).SingleOrDefault();
                    in4.UserInfo_Name = Name;
                    in4.UserInfo_CMND = CMND;
                    in4.UserInfo_BirthDate = BrithDate;
                    in4.UserInfo_Telephone = Telephone;
                    in4.UserInfo_Address = Address;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }
            });

            //Nút thoát khỏi cửa sổ 
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            }
            );

            //Nút đặt lại mật khẩu
            ResetPasswordCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                if (MessageBox.Show("Bạn chắc chắn muốn đặt lại mật khẩu không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == System.Windows.MessageBoxResult.OK)
                {
                    var Account = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == IdUser).SingleOrDefault();
                    Account.Password = "1";
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đặt lại mật khẩu thành công!\nMật khẩu hiện tại: 1", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }

            }
            );

            //Thêm tài khoản
            AddAccountCommand = new RelayCommand<Window>(
            (p) =>
            {
                if (String.IsNullOrEmpty(UserName))
                    return false;

                var displayList = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn tạo tài khoản không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == System.Windows.MessageBoxResult.OK)
                {
                    var account = new USER() { UserName = UserName, Password = "1", IdRole = 2 };
                    DataProvider.Ins.DB.USERS.Add(account);
                    DataProvider.Ins.DB.SaveChanges();

                    var acc = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName).SingleOrDefault();
                    var info = DataProvider.Ins.DB.USER_INFO.Where(x => x.UserInfo_Id == Id).SingleOrDefault();
                    info.IdUser = acc.Users_Id;
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm tài khoản thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }


            });

        }

        public EmployeeViewModel()
        {
            //Thêm danh sách nhân viên vào List
            List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);

            //Mở cửa số để thêm nhân viên
            AddEmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddEmployee wd = new AddEmployee(); wd.ShowDialog(); List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO); });

            //Tìm kiếm nhân viên
            LookUpCommand = new RelayCommand<Employee>((p) => { return true; }, (p) => { LoadUsersToView(p); });

            //Nút thêm nhân viên trên AddEmployee
            AddCommand = new RelayCommand<AddEmployee>((p) =>
            {
                if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(BrithDate.ToString()) || String.IsNullOrEmpty(CMND) || String.IsNullOrEmpty(Telephone) || String.IsNullOrEmpty(Address))
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    DateTime? date = BrithDate;
                    DataProvider.Ins.DB.USER_INFO.Add(new USER_INFO { UserInfo_Name = Name, UserInfo_BirthDate = date, UserInfo_CMND = CMND, UserInfo_Telephone = Telephone, UserInfo_Address = Address });
                    DataProvider.Ins.DB.SaveChanges();
                    //var user = new UserInfoes { Name = Name, BirthDate = date, CMND = CMND, Telephone = Telephone, Address = Address };

                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }



            });

            //Nút thoát khỏi Window AddEmployee
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            }
            );

            //Mở cửa sổ chỉnh sửa thông tin nhân viên
            EditWindowCommand = new RelayCommand<EditEmployeeWindow>((p) => { if (SelectedItem == null) return false; return true; }, (p) => { EditEmployeeWindow wd = new EditEmployeeWindow(SelectedItem); wd.ShowDialog(); List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO); });

            //Mở cửa sổ cho nút tài khoản
            AccountCommand = new RelayCommand<Window>(
            (p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                if (SelectedItem.IdUser == null)
                {
                    AddAccountWindow addAccountWindow = new AddAccountWindow(SelectedItem);
                    addAccountWindow.ShowDialog();
                }
                else
                {
                    EditAccountWindow editAccountWindow = new EditAccountWindow(SelectedItem);
                    editAccountWindow.ShowDialog();
                }
            });

            //Xóa nhân viên
            DeleteEmployeeCommand = new RelayCommand<Window>(
            (p) =>
            {
                if (SelectedItem == null || UserName == "admin")
                    return false;
                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên này không?", "Cảnh báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == System.Windows.MessageBoxResult.OK)
                {
                    if (SelectedItem.IdUser == null)
                    {
                        DataProvider.Ins.DB.USER_INFO.Remove(SelectedItem);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        USER acc = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == SelectedItem.IdUser).SingleOrDefault();
                        DataProvider.Ins.DB.USER_INFO.Remove(SelectedItem);
                        DataProvider.Ins.DB.USERS.Remove(acc);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
                }


            });

        }

        public void LoadUsersToView(Employee p)
        {
            List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
            if (String.IsNullOrEmpty(p.txtLookUp.Text))
            {
                
                p.lvUsers.ItemsSource = List;
            }
            else
            {
                ObservableCollection<USER_INFO> _ListTempt = new ObservableCollection<USER_INFO>();

                foreach (var item in List)
                {
                    if (item.UserInfo_Name.Contains(p.txtLookUp.Text.ToString()))
                    {
                        _ListTempt.Add(item);
                    }
                }
                List = _ListTempt;

            }


        }
    }
}
