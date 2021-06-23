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
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Security.Cryptography;

namespace QuanLyGaraOto.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ICommand CheckExistAddRoleName { get; set; }
        public ICommand CheckExistEditRoleName { get; set; }
        private bool _VisExistsAddRoleName { get; set; }
        public bool VisExistsAddRoleName { get => _VisExistsAddRoleName; set { _VisExistsAddRoleName = value; OnPropertyChanged(); } }
        private bool _VisExistsEditRoleName { get; set; }
        public bool VisExistsEditRoleName { get => _VisExistsEditRoleName; set { _VisExistsEditRoleName = value; OnPropertyChanged(); } }
        // Validation exist name role
        public ICommand CheckExistAddUserName { get; set; }
        private bool _VisExistsAddUserName { get; set; }
        public bool VisExistsAddUserName { get => _VisExistsAddUserName; set { _VisExistsAddUserName = value; OnPropertyChanged(); } }
  
        // validation exist name user
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
        public ICommand CheckBunk { get; set; }
        public ICommand CheckEmployee { get; set; }
        public ICommand CheckReport { get; set; }
        public ICommand CheckParentRole { get; set; }
        public ICommand SelectionChangedRole { get; set; }
        public ICommand SelectionChangedUser { get; set; }

        public ObservableCollection<USER_INFO> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public ObservableCollection<ROLE> ListRoles { get => _ListRoles; set { _ListRoles = value; OnPropertyChanged(); } }

        public USER_INFO SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                
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
                OnPropertyChanged();
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
                    isServiceWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission;
                    isBunkWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission;
                    isImportBunk = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission;
                    isEmployeeWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission;
                    isEmployeeInfo = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission;
                    isEmployeeRole = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission;
                    isReportWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission;
                    isReport = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 8).SingleOrDefault().Permission;
                    isGaraInfo = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 9).SingleOrDefault().Permission;
                    isWage = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 10).SingleOrDefault().Permission;
                    isCarBranch = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 11).SingleOrDefault().Permission;
                    isSuplier = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 12).SingleOrDefault().Permission;
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
        public bool isServiceWindow { get => _isServiceWindow; set { _isServiceWindow = value; OnPropertyChanged(); } }
        public bool isBunkWindow { get => _isBunkWindow; set { _isBunkWindow = value; OnPropertyChanged(); } }
        public bool isImportBunk { get => _isImportBunk; set { _isImportBunk = value; OnPropertyChanged(); } }
        public bool isEmployeeWindow { get => _isEmployeeWindow; set { _isEmployeeWindow = value; OnPropertyChanged(); } }
        public bool isEmployeeInfo { get => _isEmployeeInfo; set { _isEmployeeInfo = value; OnPropertyChanged(); } }
        public bool isEmployeeRole { get => _isEmployeeRole; set { _isEmployeeRole = value; OnPropertyChanged(); } }
        public bool isReportWindow { get => _isReportWindow; set { _isReportWindow = value; OnPropertyChanged(); } }
        public bool isReport { get => _isReport; set { _isReport = value; OnPropertyChanged(); } }
        public bool isGaraInfo { get => _isGaraInfo; set { _isGaraInfo = value; OnPropertyChanged(); } }
        public bool isWage { get => _isWage; set { _isWage = value; OnPropertyChanged(); } }
        public bool isCarBranch { get => _isCarBranch; set { _isCarBranch = value; OnPropertyChanged(); } }
        public bool isSuplier { get => _isSuplier; set { _isSuplier = value; OnPropertyChanged(); } }

        private ObservableCollection<USER_INFO> _List { get; set; }

        private ObservableCollection<ROLE> _ListRoles;

        private USER_INFO _SelectedItem { get; set; }

        private ROLE _SelectedItemRole;

        //Thông tin 
        private string _Name { get; set; }
        private DateTime? _BrithDate { get; set; }
        private string _CMND { get; set; }
        private string _Telephone { get; set; }
        private string _Address { get; set; }
        private string _UserName { get; set; }
        private int _Id { get; set; }
        private int _IdUser { get; set; }

        // Chức vụ
        private int _RoleId;
        private string _RoleName;
        private bool _isServiceWindow;
        private bool _isBunkWindow;
        private bool _isImportBunk;
        private bool _isEmployeeWindow;
        private bool _isEmployeeInfo;
        private bool _isEmployeeRole;
        private bool _isReportWindow;
        private bool _isReport;
        private bool _isGaraInfo;
        private bool _isWage;
        private bool _isCarBranch;
        private bool _isSuplier;

        //Quyền
        bool _roleInfo = false;
        public bool roleInfo { get => _roleInfo; set { _roleInfo = value; OnPropertyChanged(); } }

        bool _roleRole = false;
        public bool roleRole { get => _roleRole; set { _roleRole = value; OnPropertyChanged(); } }

        String _currentUserName;
        public String currentUserName { get => _currentUserName; set { _currentUserName = value; OnPropertyChanged(); } }

        //Danh sách tạm
        private ObservableCollection<ROLE> _TempRole { get; set; }
        public ObservableCollection<ROLE> TempRole { get => _TempRole; set { _TempRole = value; OnPropertyChanged(); } }

        private ObservableCollection<USER_INFO> _TempUser { get; set; }
        public ObservableCollection<USER_INFO> TempUser { get => _TempUser; set { _TempUser = value; OnPropertyChanged(); } }

        public EmployeeViewModel(bool role1, bool role2, String username) : this()
        {
            roleInfo = role1;
            roleRole = role2;
            currentUserName = username;
        }

        public EmployeeViewModel(ROLE r)
        {

            ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
            RoleName = r.Role_Name;
            RoleId = r.Role_Id;           
            isServiceWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 1).SingleOrDefault().Permission;
            isBunkWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault().Permission;
            isImportBunk = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault().Permission;
            isEmployeeWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault().Permission;
            isEmployeeInfo = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault().Permission;
            isEmployeeRole = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault().Permission;
            isReportWindow = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault().Permission;
            isReport = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 8).SingleOrDefault().Permission;
            isGaraInfo = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 9).SingleOrDefault().Permission;
            isWage = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 10).SingleOrDefault().Permission;
            isCarBranch = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 11).SingleOrDefault().Permission;
            isSuplier = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 12).SingleOrDefault().Permission;

            String oldName = RoleName;
            // validate name
            CheckExistEditRoleName = new RelayCommand<EditRoleWindow>((p) => { return true; }, (p) =>
            {
                VisExistsEditRoleName = false;
                if (DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == p.txtRoleName.Text && r.Role_Name != p.txtRoleName.Text).Count() != 0)
                {
                    VisExistsEditRoleName = true;
                }
            });
            // Sửa chức vụ
            EditRoleCommand = new RelayCommand<EditRoleWindow>((p) => {
                if (String.IsNullOrEmpty(p.txtRoleName.Text))
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
                    is1.Permission = isServiceWindow;
                    DataProvider.Ins.DB.SaveChanges();

                    var is2 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 2).SingleOrDefault();
                    is2.Permission = isBunkWindow;
                    DataProvider.Ins.DB.SaveChanges();

                    var is3 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 3).SingleOrDefault();
                    is3.Permission = isImportBunk;
                    DataProvider.Ins.DB.SaveChanges();

                    var is4 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 4).SingleOrDefault();
                    is4.Permission = isEmployeeWindow;
                    DataProvider.Ins.DB.SaveChanges();

                    var is5 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 5).SingleOrDefault();
                    is5.Permission = isEmployeeInfo;
                    DataProvider.Ins.DB.SaveChanges();

                    var is6 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 6).SingleOrDefault();
                    is6.Permission = isEmployeeRole;
                    DataProvider.Ins.DB.SaveChanges();

                    var is7 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 7).SingleOrDefault();
                    is7.Permission = isReportWindow;
                    DataProvider.Ins.DB.SaveChanges();

                    var is8 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 8).SingleOrDefault();
                    is8.Permission = isReport;
                    DataProvider.Ins.DB.SaveChanges();

                    var is9 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 9).SingleOrDefault();
                    is9.Permission = isGaraInfo;
                    DataProvider.Ins.DB.SaveChanges();

                    var is10 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 10).SingleOrDefault();
                    is10.Permission = isWage;
                    DataProvider.Ins.DB.SaveChanges();

                    var is11 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 11).SingleOrDefault();
                    is11.Permission = isCarBranch;
                    DataProvider.Ins.DB.SaveChanges();

                    var is12 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == RoleId).Where(x => x.IdPermissionItem == 12).SingleOrDefault();
                    is12.Permission = isSuplier;
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }

            });

            //Nút thoát khỏi cửa sổ 
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                if (MessageBox.Show("Bạn chắc chắn muốn đóng cửa sổ này", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    p.Close();
            });

            // Kiểm tra quyền truy cập kho hàng khi bỏ check
            CheckBunk = new RelayCommand<EditRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isBunkWindow == false)
                {
                    isImportBunk = false;
                }
            });

            // Kiểm tra quyền truy cập nhân viên khi bỏ check
            CheckEmployee = new RelayCommand<EditRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isEmployeeWindow == false)
                {
                    isEmployeeInfo = false;
                    isEmployeeRole = false;
                }
            });

            // Kiểm tra quyền truy cập thống kê khi bỏ check
            CheckReport = new RelayCommand<EditRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isReportWindow == false)
                {
                    isReport = false;
                }
            });

            // Kiểm tra quyền cha đã được check chưa
            CheckParentRole = new RelayCommand<EditRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isImportBunk == true)
                {
                    isBunkWindow = true;
                }

                if (isEmployeeInfo == true || isEmployeeRole == true)
                {
                    isEmployeeWindow = true;
                }

                if (isReport == true)
                {
                    isReportWindow = true;
                }
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
            EditEmployeeCommand = new RelayCommand<EditEmployeeWindow>((p) =>
            {
                Regex regex = new Regex(@"^[0-9]+$");
                if (String.IsNullOrEmpty(p.txtName.Text) || String.IsNullOrEmpty(p.dpkBrithDate.Text) || String.IsNullOrEmpty(p.txtCMND.Text) || String.IsNullOrEmpty(p.txtTelephone.Text) || String.IsNullOrEmpty(p.txtAddress.Text) || String.IsNullOrEmpty(p.cbxRoleName.Text) || !regex.IsMatch(p.txtTelephone.Text))
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
                    acc.Password = MD5Hash(Base64Encode("1"));
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đặt lại mật khẩu thành công!\nMật khẩu hiện tại: 1", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    p.Close();
                }

            }
            );

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
                Regex regex = new Regex(@"^[0-9]+$");
                if (String.IsNullOrEmpty(p.txtName.Text) || String.IsNullOrEmpty(p.dpkBrithDate.Text) || String.IsNullOrEmpty(p.txtCMND.Text) || String.IsNullOrEmpty(p.txtTelephone.Text) || String.IsNullOrEmpty(p.txtAddress.Text) || String.IsNullOrEmpty(p.cbxRoleName.Text) || String.IsNullOrEmpty(p.txtUserName.Text) || !regex.IsMatch(p.txtTelephone.Text))
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
                    var account = new USER() { UserName = UserName, Password = MD5Hash(Base64Encode("1")), IdRole = id_role };
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
                    wd.cbxRoleName.IsEnabled = false;
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
                    int deleteSuccess = 0;
                    bool deleteCurrent = false;

                    foreach (var item in TempUser)
                    {
                        if (item.USER.UserName != currentUserName)
                        {
                            USER acc = DataProvider.Ins.DB.USERS.Where(x => x.Users_Id == item.IdUser).SingleOrDefault();
                            DataProvider.Ins.DB.USER_INFO.Remove(item);
                            DataProvider.Ins.DB.USERS.Remove(acc);
                            DataProvider.Ins.DB.SaveChanges();

                            deleteSuccess++;
                            //MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                        else
                        {
                            deleteCurrent = true;
                            //MessageBox.Show("Không thể xóa người dùng đang đăng nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    if (deleteCurrent == true)
                    {
                        if (deleteSuccess == 0)
                        {
                            MessageBox.Show("Không thể xóa người dùng đang đăng nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            MessageBox.Show(deleteSuccess.ToString() + " nhân viên đã được xóa thành công!"
                                + "\n \nNgười dùng có tên tài khoản '" + currentUserName + "' không thể xóa do đang đăng nhập", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }    
                    else
                    {
                        if (deleteSuccess == 1)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                        else
                        {
                            MessageBox.Show(deleteSuccess.ToString() + " nhân viên đã được xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }    
                        

                    List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);

                }


            });

            //Mở cửa số để thêm chức vụ
            AddRoleWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddRoleWindow wd = new AddRoleWindow(); wd.ShowDialog(); ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs); });
            // Valididate name
            CheckExistAddRoleName = new RelayCommand<AddRoleWindow>((p) => { return true; }, (p) =>
            {
                VisExistsAddRoleName = false;
                if (DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == p.txtRoleName.Text).Count() == 0)
                {
                    VisExistsAddRoleName = false;
                }
                else VisExistsAddRoleName = true;
            });
            CheckExistAddUserName = new RelayCommand<AddEmployee>((p) => { return true; }, (p) =>
            {
                VisExistsAddUserName = false;
                if (DataProvider.Ins.DB.USERS.Where(x => x.UserName == p.txtUserName.Text).Count() == 0)
                {
                    VisExistsAddUserName = false;
                }
                else VisExistsAddUserName = true;
            });
            //Nút thêm chức vụ trên AddRoleWindow
            AddRoleCommand = new RelayCommand<AddRoleWindow>((p) =>
            {
                if (String.IsNullOrEmpty(p.txtRoleName.Text))
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
                if (MessageBox.Show("Bạn có chắc chắn muốn thêm chức vụ này", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    //DateTime? date = BrithDate;
                    //DataProvider.Ins.DB.USER_INFO.Add(new USER_INFO { UserInfo_Name = Name, UserInfo_BirthDate = date, UserInfo_CMND = CMND, UserInfo_Telephone = Telephone, UserInfo_Address = Address });
                    //DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLEs.Add(new ROLE { Role_Name = RoleName });
                    DataProvider.Ins.DB.SaveChanges();

                    int idRole = Int32.Parse(DataProvider.Ins.DB.ROLEs.Where(x => x.Role_Name == RoleName).SingleOrDefault().Role_Id.ToString());

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 1, Permission = isServiceWindow });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 2, Permission = isBunkWindow });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 3, Permission = isImportBunk });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 4, Permission = isEmployeeWindow });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 5, Permission = isEmployeeInfo });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 6, Permission = isEmployeeRole });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 7, Permission = isReportWindow });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 8, Permission = isReport });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 9, Permission = isGaraInfo });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 10, Permission = isWage });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 11, Permission = isCarBranch });
                    DataProvider.Ins.DB.SaveChanges();

                    DataProvider.Ins.DB.ROLE_DETAIL.Add(new ROLE_DETAIL { IdRole = idRole, IdPermissionItem = 12, Permission = isSuplier });
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
            }, 
            (p) => {
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
                    int deleteSuccess = 0;
                    int deleteFail = 0;
                    string listUsernameFail = "";         

                    foreach (var item in TempRole)
                    {
                        var displayList = DataProvider.Ins.DB.USERS.Where(x => x.IdRole == item.Role_Id);
                        if (displayList == null || displayList.Count() != 0)
                        {
                            listUsernameFail += "\n  - " + item.Role_Name;
                            deleteFail++;
                          
                            //MessageBox.Show("Chức vụ này đang được sử dụng \nKhông thể xóa", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            var dt1 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 1).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt1);

                            var dt2 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 2).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt2);

                            var dt3 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 3).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt3);

                            var dt4 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 4).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt4);

                            var dt5 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 5).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt5);

                            var dt6 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 6).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt6);

                            var dt7 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 7).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt7);

                            var dt8 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 8).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt8);

                            var dt9 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 9).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt9);

                            var dt10 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 10).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt10);

                            var dt11 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 11).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt11);

                            var dt12 = DataProvider.Ins.DB.ROLE_DETAIL.Where(x => x.IdRole == item.Role_Id).Where(x => x.IdPermissionItem == 12).SingleOrDefault();
                            DataProvider.Ins.DB.ROLE_DETAIL.Remove(dt12);

                            DataProvider.Ins.DB.ROLEs.Remove(item);
                            DataProvider.Ins.DB.SaveChanges();

                            //MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            deleteSuccess++;
                        }
                    }  
                    
                    if (deleteFail == 0)
                    {
                        if (deleteSuccess == 1)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }   
                        else
                        {
                            MessageBox.Show(deleteSuccess.ToString() + " chức vụ đã được xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }    
                    else
                    {
                        if (deleteFail == 1)
                        {
                            if (deleteSuccess == 0)
                            {
                                MessageBox.Show("Chức vụ này đang được sử dụng \nKhông thể xóa", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }    
                            else
                            {
                                MessageBox.Show(deleteSuccess.ToString() + " chức vụ đã được xóa thành công!\n\n"
                                + deleteFail.ToString() + "chức vụ không thể xóa do đang được sử dụng" + listUsernameFail, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }    
                        }    
                       else
                        {
                            MessageBox.Show(deleteSuccess.ToString() + " chức vụ đã được xóa thành công!\n\n"
                            + deleteFail.ToString() + "chức vụ không thể xóa do đang được sử dụng" + listUsernameFail, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }    
                    }    
                    

                    ListRoles = new ObservableCollection<ROLE>(DataProvider.Ins.DB.ROLEs);
                }


            });

            // Kiểm tra quyền truy cập kho hàng khi bỏ check
            CheckBunk = new RelayCommand<AddRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isBunkWindow == false)
                {
                    isImportBunk = false;
                }    
            });

            // Kiểm tra quyền truy cập nhân viên khi bỏ check
            CheckEmployee = new RelayCommand<AddRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isEmployeeWindow == false)
                {
                    isEmployeeInfo = false;
                    isEmployeeRole = false;
                }
            });

            // Kiểm tra quyền truy cập thống kê khi bỏ check
            CheckReport = new RelayCommand<AddRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isReportWindow == false)
                {
                    isReport = false;
                }
            });

            // Kiểm tra quyền cha đã được check chưa
            CheckParentRole = new RelayCommand<AddRoleWindow>((p) => {
                return true;
            },
            (p) => {
                if (isImportBunk == true)
                {
                    isBunkWindow = true;
                }

                if (isEmployeeInfo == true || isEmployeeRole == true)
                {
                    isEmployeeWindow = true;
                }

                if (isReport == true)
                {
                    isReportWindow = true;
                }
            });

            // Danh sách chức vụ tạm
            SelectionChangedRole = new RelayCommand<DataGrid>
                ((p) =>
                {
                    return true;
                }, (p) =>
                {
                    TempRole = new ObservableCollection<ROLE>(p.SelectedItems.Cast<ROLE>().ToList());
                });

            // Danh sách nhân viên tạm
            SelectionChangedUser = new RelayCommand<DataGrid>
                ((p) =>
                {
                    return true;
                }, (p) =>
                {
                    TempUser = new ObservableCollection<USER_INFO>(p.SelectedItems.Cast<USER_INFO>().ToList());
                });

        }

        public void LoadUsersToView(Employee p)
        {
            List = new ObservableCollection<USER_INFO>(DataProvider.Ins.DB.USER_INFO);
            if (String.IsNullOrEmpty(p.txtLookUp.Text))
            {
                
                p.datagridUser.ItemsSource = List;
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
                p.datagrid.ItemsSource = ListRoles;
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

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
