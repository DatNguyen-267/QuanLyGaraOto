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
        public ICommand AddRoleWindowCommand { get; set; }
        public ICommand AddRoleCommand { get; set; }
        public ICommand EditRoleWindowCommand { get; set; }
        public ICommand EditRoleCommand { get; set; }
        public ICommand DeleteRoleCommand { get; set; }
        public ICommand LookUpRoleCommand { get; set; }

        public ObservableCollection<USER_INFO> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public ObservableCollection<ROLE> ListRoles { get => _ListRoles; set { _ListRoles = value; OnPropertyChanged(); } }

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
                    Id = SelectedItem.UserInfo_Id;
                    IdUser = (int)SelectedItem.IdUser;

                    var Account = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == SelectedItem.IdUser).SingleOrDefault();
                    UserName = Account.UserName.ToString();

                    RoleId = Account.IdRole;
                    RoleName = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Id == RoleId).SingleOrDefault().Role_Name;
                }
            }
        }

        public ROLE SelectedItemRole
        {
            get => _SelectedItemRole;
            set
            {
                _SelectedItemRole = value;
                OnPropertyChanged();
                if (SelectedItemRole != null)
                {
                    RoleName = SelectedItemRole.Role_Name;
                    RoleId = SelectedItemRole.Role_Id;
                    isDashboard = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission;
                    isService = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission;
                    isBunk = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission;
                    isEmployee = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission;
                    isReport = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission;
                    isSettingAccount = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission;
                    isSettingApp = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission;
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

        public int RoleId { get => _RoleId; set { _RoleId = value; OnPropertyChanged(); } }
        public string RoleName { get => _RoleName; set { _RoleName = value; OnPropertyChanged(); } }
        public bool isDashboard { get => _isDashboard; set { _isDashboard = value; OnPropertyChanged(); } }
        public bool isService { get => _isService; set { _isService = value; OnPropertyChanged(); } }
        public bool isBunk { get => _isBunk; set { _isBunk = value; OnPropertyChanged(); } }
        public bool isEmployee { get => _isEmployee; set { _isEmployee = value; OnPropertyChanged(); } }
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }
        public bool isSettingAccount { get => _isSettingAccount; set { _isSettingAccount = value; OnPropertyChanged(); } }
        public bool isSettingApp { get => _isSettingApp; set { _isSettingApp = value; OnPropertyChanged(); } }

        private ObservableCollection<USER_INFO> _List;

        private ObservableCollection<ROLE> _ListRoles;

        private USER_INFO _SelectedItem;

        private ROLE _SelectedItemRole;

        //Thông tin 
        private string _Name;
        private DateTime? _BrithDate;
        private string _CMND;
        private string _Telephone;
        private string _Address;
        private string _UserName;
        private int _Id;
        private int _IdUser;

        // Chức vụ
        private int _RoleId;
        private string _RoleName;
        private bool _isDashboard;
        private bool _isService;
        private bool _isBunk;
        private bool _isEmployee;
        private bool _isReport;
        private bool _isSettingAccount;
        private bool _isSettingApp;

        public EmployeeViewModel(ROLE r)
        {

            ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
            RoleName = r.Role_Name;
            RoleId = r.Role_Id;
            isDashboard = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission;
            isService = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission;
            isBunk = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission;
            isEmployee = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission;
            isReport = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission;
            isSettingAccount = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission;
            isSettingApp = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission;

            String oldName = RoleName;

            // Sửa chức vụ
            EditRoleCommand = new RelayCommand<EditRoleWindow>((p) => {
                if (String.IsNullOrEmpty(RoleName))
                    return false;

                var displayList = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName).Where(x => x.Role_Name != oldName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            }, (p) => {
                if (MessageBox.Show("Bạn chắc chắn muốn sửa thông tin chức vụ này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == System.Windows.MessageBoxResult.OK)
                {
                    var in4 = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Id == RoleId).SingleOrDefault();
                    in4.Role_Name = RoleName;
                    DataProvider.Ins.DB.SaveChanges();

                    var is1 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault();
                    is1.Permission = isDashboard;
                    DataProvider.Ins.DB.SaveChanges();

                    var is2 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault();
                    is2.Permission = isService;
                    DataProvider.Ins.DB.SaveChanges();

                    var is3 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault();
                    is3.Permission = isBunk;
                    DataProvider.Ins.DB.SaveChanges();

                    var is4 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault();
                    is4.Permission = isEmployee;
                    DataProvider.Ins.DB.SaveChanges();

                    var is5 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault();
                    is5.Permission = isReport;
                    DataProvider.Ins.DB.SaveChanges();

                    var is6 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault();
                    is6.Permission = isSettingAccount;
                    DataProvider.Ins.DB.SaveChanges();

                    var is7 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault();
                    is7.Permission = isSettingApp;
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }

            });

            //Nút thoát khỏi cửa sổ 
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Close();
            });
        }

        public EmployeeViewModel(USER_INFO u)
        {
            ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
            Name = u.UserInfo_Name;
            BrithDate = u.UserInfo_BirthDate;
            CMND = u.UserInfo_CMND;
            Telephone = u.UserInfo_Telephone;
            Address = u.UserInfo_Address;
            Id = u.UserInfo_Id;
            IdUser = (int)u.IdUser;

            var Account = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == u.IdUser).SingleOrDefault();
            UserName = Account.UserName.ToString();

            RoleId = Account.IdRole;
            RoleName = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Id == RoleId).SingleOrDefault().Role_Name.ToString();

            //Nút sửa thông tin trên cửa sổ Edit
            EditEmployeeCommand = new RelayCommand<Window>((p) =>
            {
                if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(BrithDate.ToString()) || String.IsNullOrEmpty(CMND) || String.IsNullOrEmpty(Telephone) || String.IsNullOrEmpty(Address) || String.IsNullOrEmpty(RoleName))
                {
                    return false;
                }
                return true;
            }, (p) => {
                if (MessageBox.Show("Bạn chắc chắn muốn sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == System.Windows.MessageBoxResult.OK)
                {
                    int id_role = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName).SingleOrDefault().Role_Id;
                    var acc = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == IdUser).SingleOrDefault();
                    acc.IdRole = id_role;
                    DataProvider.Ins.DB.SaveChanges();

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
                    var acc = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == IdUser).SingleOrDefault();
                    acc.Password = "1";
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

            //Thêm danh sách chức vụ vào ListRole
            ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);

            //Mở cửa số để thêm nhân viên
            AddEmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddEmployee wd = new AddEmployee(); wd.ShowDialog(); List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO); });

            //Tìm kiếm nhân viên
            LookUpCommand = new RelayCommand<Employee>((p) => { return true; }, (p) => { LoadUsersToView(p); });

            //Tìm kiếm chức vụ
            LookUpRoleCommand = new RelayCommand<Employee>((p) => { return true; }, (p) => { LoadRolesToView(p); });

            //Nút thêm nhân viên trên AddEmployee
            AddCommand = new RelayCommand<AddEmployee>((p) =>
            {
                if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(BrithDate.ToString()) || String.IsNullOrEmpty(CMND) || String.IsNullOrEmpty(Telephone) || String.IsNullOrEmpty(Address) || String.IsNullOrEmpty(RoleName) || String.IsNullOrEmpty(UserName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName);
                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }
                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    int id_role = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName).SingleOrDefault().Role_Id;
                    var account = new USER() { UserName = UserName, Password = "1", IdRole = id_role };
                    DataProvider.Ins.DB.USERS.Add(account);
                    DataProvider.Ins.DB.SaveChanges();

                    int id_user = DataProvider.Ins.DB.USERS.Where(x => x.UserName == UserName).SingleOrDefault().Users_Id;
                    DateTime? date = BrithDate;
                    DataProvider.Ins.DB.USER_INFO.Add(new USER_INFO { UserInfo_Name = Name, UserInfo_BirthDate = date, UserInfo_CMND = CMND, UserInfo_Telephone = Telephone, UserInfo_Address = Address, IdUser = id_user });
                    DataProvider.Ins.DB.SaveChanges();

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
            EditWindowCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                EditEmployeeWindow wd = new EditEmployeeWindow(SelectedItem);
                if (SelectedItem.USER.UserName == "admin")
                {
                    wd.cbxListRoles.IsEnabled = false;
                }
                wd.ShowDialog();
                List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
            });

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
                EditAccountWindow editAccountWindow = new EditAccountWindow(SelectedItem);
                editAccountWindow.ShowDialog();
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
                    USER acc = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == SelectedItem.IdUser).SingleOrDefault();
                    DataProvider.Ins.DB.USER_INFO.Remove(SelectedItem);
                    DataProvider.Ins.DB.USERS.Remove(acc);
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
                }


            });

            //Mở cửa số để thêm chức vụ
            AddRoleWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddRoleWindow wd = new AddRoleWindow(); wd.ShowDialog(); ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs); });

            //Nút thêm chức vụ trên AddRoleWindow
            AddRoleCommand = new RelayCommand<AddRoleWindow>((p) =>
            {
                if (String.IsNullOrEmpty(RoleName))
                {
                    return false;
                }

                var displayList = DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn chức vụ này", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    //DateTime? date = BrithDate;
                    //DataProvider.Ins.DB.USER_INFO.Add(new USER_INFO { UserInfo_Name = Name, UserInfo_BirthDate = date, UserInfo_CMND = CMND, UserInfo_Telephone = Telephone, UserInfo_Address = Address });
                    //DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLEs.Add(new ROLE { Role_Name = RoleName });
                    DataProvider.Ins.DB.SaveChanges();

                    int idRole = Int32.Parse(DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName).SingleOrDefault().Role_Id.ToString());

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 1, Permission = isDashboard });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 2, Permission = isService });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 3, Permission = isBunk });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 4, Permission = isEmployee });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 5, Permission = isReport });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 6, Permission = isSettingAccount });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 7, Permission = isSettingApp });
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }



            });

            // Mở cửa sổ chỉnh sửa thông tin chức vụ
            EditRoleWindowCommand = new RelayCommand<object>((p) => {
                if (SelectedItemRole == null)
                    return false;
                if (SelectedItemRole.Role_Name == "admin")
                    return false;
                return true;
            }, (p) => {
                EditRoleWindow wd = new EditRoleWindow(SelectedItemRole);
                wd.ShowDialog();
                ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
                List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
            });

            //Xóa chức vụ
            DeleteRoleCommand = new RelayCommand<Window>(
            (p) =>
            {
                if (SelectedItemRole == null || RoleName == "admin")
                    return false;
                return true;
            },
            (p) =>
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa chức vụ này không?", "Cảnh báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == System.Windows.MessageBoxResult.OK)
                {
                    var displayList = DataProvider.Ins.DB.USERS.Where(x => x.IdRole == RoleId);
                    if (displayList == null || displayList.Count() != 0)
                    {
                        MessageBox.Show("Chức vụ này đang được sử dụng \nKhông thể xóa", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        var dt1 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt1);

                        var dt2 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt2);

                        var dt3 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt3);

                        var dt4 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt4);

                        var dt5 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt5);

                        var dt6 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt6);

                        var dt7 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault();
                        DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt7);

                        DataProvider.Ins.DB.ROLEs.Remove(SelectedItemRole);
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
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

        public void LoadRolesToView(Employee p)
        {
            if (String.IsNullOrEmpty(p.txtLookUpRole.Text))
            {
                ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
                p.lvRoles.ItemsSource = ListRoles;
            }
            else
            {
                ObservableCollection<ROLE> _ListTempt = new ObservableCollection<ROLE>();

                foreach (var item in ListRoles)
                {
                    if (item.Role_Name.Contains(p.txtLookUpRole.Text.ToString()))
                    {
                        _ListTempt.Add(item);
                    }
                }
                ListRoles = _ListTempt;

            }


        }
    }
}
