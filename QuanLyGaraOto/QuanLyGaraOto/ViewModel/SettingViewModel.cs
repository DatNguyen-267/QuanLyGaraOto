using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyGaraOto.Convert;

namespace QuanLyGaraOto.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
        // login account
        public USER user;

        // Setting visibility
        private bool _AppSettingVis;
        public bool AppSettingVis
        {
            get => _AppSettingVis;
            set
            {
                _AppSettingVis = value;
                OnPropertyChanged();
            }
        }

        private bool _UserSettingVis;
        public bool UserSettingVis
        {
            get => _UserSettingVis;
            set
            {
                _UserSettingVis = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeAppSettingVis { get; set; }
        public ICommand ChangeUserSettingVis { get; set; }

        // IsEnable button setting in gara information
        private bool _IsEnableChangeButtonInGaraInformation;
        public bool IsEnableChangeButtonInGaraInformation
        {
            get => _IsEnableChangeButtonInGaraInformation; set
            {
                _IsEnableChangeButtonInGaraInformation = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableResetButtonInGaraInformation;
        public bool IsEnableResetButtonInGaraInformation
        {
            get => _IsEnableResetButtonInGaraInformation; set
            {
                _IsEnableResetButtonInGaraInformation = value;
                OnPropertyChanged();
            }
        }
        // IsEnable button setting in user information
        private bool _IsEnableChangeButtonInUserInformation;
        public bool IsEnableChangeButtonInUserInformation
        {
            get => _IsEnableChangeButtonInUserInformation; set
            {
                _IsEnableChangeButtonInUserInformation = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableResetButtonInUserInformation;
        public bool IsEnableResetButtonInUserInformation
        {
            get => _IsEnableResetButtonInUserInformation; set
            {
                _IsEnableResetButtonInUserInformation = value;
                OnPropertyChanged();
            }
        }
        // IsEnable button setting in change password
        private bool _IsEnableCheckButtonInChangePassword;
        public bool IsEnableCheckButtonInChangePassword
        {
            get => _IsEnableCheckButtonInChangePassword; set
            {
                _IsEnableCheckButtonInChangePassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeEnableButttonInGaraInformation { get; set; }
        public ICommand ChangeEnableButtonInUserInformation { get; set; }

        // Setting gara information
        private GARA_INFO GaraInfo;

        private int _MaxCarReception;
        public int MaxCarReception
        {
            get => _MaxCarReception;
            set
            {
                _MaxCarReception = value;
                OnPropertyChanged();
            }
        }

        private bool _IsOverPay;
        public bool IsOverPay
        {
            get => _IsOverPay; set
            {
                _IsOverPay = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeGaraInformation { get; set; }
        public ICommand ResetGaraInformation { get; set; }

        // Is enable button in car brand setting
        private bool _IsEnableModifyButtonInBrandSetting;
        public bool IsEnableModifyButtonInBrandSetting
        {
            get => _IsEnableModifyButtonInBrandSetting; set
            {
                _IsEnableModifyButtonInBrandSetting = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableDeleteButtonInBrandSetting;
        public bool IsEnableDeleteButtonInBrandSetting
        {
            get => _IsEnableDeleteButtonInBrandSetting; set
            {
                _IsEnableDeleteButtonInBrandSetting = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableModifyFieldInBrandSetting;
        public bool IsEnableModifyFieldInBrandSetting
        {
            get => _IsEnableModifyFieldInBrandSetting; set
            {
                _IsEnableModifyFieldInBrandSetting = value;
                OnPropertyChanged();
            }
        }

        // Setting car brand

        private ObservableCollection<CAR_BRAND> _ListCarBrand;
        public ObservableCollection<CAR_BRAND> ListCarBrand
        {
            get => _ListCarBrand; set
            {
                _ListCarBrand = value;
                OnPropertyChanged();
            }
        }

        private CAR_BRAND _SelectedBrandItem;
        public CAR_BRAND SelectedBrandItem
        {
            get => _SelectedBrandItem; set
            {
                _SelectedBrandItem = value;
                OnPropertyChanged();
                IsEnableDeleteButtonInBrandSetting = true;
                IsEnableModifyButtonInBrandSetting = true;
            }
        }

        public ObservableCollection<CAR_BRAND> SelectedBrandItems { get; set; }

        public ICommand ChangeEnableButtonInCarBrandSetting { get; set; }
        public ICommand OpenModifyCarBrandWindow { get; set; }
        public ICommand DeleteCarBrand { get; set; }
        public ICommand OpenAddCarBrandWindow { get; set; }
        public ICommand BrandSelectionChanged { get; set; }

        // Add brand window
        private string _CarBrandInAdd;
        public string CarBrandInAdd
        {
            get => _CarBrandInAdd; set
            {
                _CarBrandInAdd = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelAddCarBrandWindow { get; set; }
        public ICommand AddCarBrand { get; set; }


        // Is enable button in wage setting

        private bool _IsEnableModifyButtonInWageSetting;
        public bool IsEnableModifyButtonInWageSetting
        {
            get => _IsEnableModifyButtonInWageSetting; set
            {
                _IsEnableModifyButtonInWageSetting = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableDeleteButtonInWageSetting;
        public bool IsEnableDeleteButtonInWageSetting
        {
            get => _IsEnableDeleteButtonInWageSetting; set
            {
                _IsEnableDeleteButtonInWageSetting = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEnableModifyFieldInWageSetting;
        public bool IsEnableModifyFieldInWageSetting
        {
            get => _IsEnableModifyFieldInWageSetting; set
            {
                _IsEnableModifyFieldInWageSetting = value;
                OnPropertyChanged();
            }
        }

        // setting wage information

        private ObservableCollection<WAGE> _ListWage;
        public ObservableCollection<WAGE> ListWage
        {
            get => _ListWage; set
            {
                _ListWage = value;
                OnPropertyChanged();
            }
        }

        private WAGE _SelectedWageItem;
        public WAGE SelectedWageItem
        {
            get => _SelectedWageItem; set
            {
                _SelectedWageItem = value;
                OnPropertyChanged();
                IsEnableDeleteButtonInWageSetting = true;
                IsEnableModifyButtonInWageSetting = true;
            }
        }

        public ObservableCollection<WAGE> SelectedWageItems { get; set; }

        public ICommand ChangeEnableButtonInWageBrandSetting { get; set; }
        public ICommand OpenModifyWageWindow { get; set; }
        public ICommand DeleteWage { get; set; }
        public ICommand OpenAddWageWindow { get; set; }
        public ICommand WageSelectionChanged { get; set; }

        // Add wage window
        public ICommand CancelAddWageWindow { get; set; }
        public ICommand AddWage { get; set; }

        // Setting user information

        USER_INFO userInfo;

        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        private string _UserAddress;
        public string UserAddress
        {
            get => _UserAddress;
            set
            {
                _UserAddress = value;
                OnPropertyChanged();
            }
        }

        private DateTime _UserBirth;
        public DateTime UserBirth
        {
            get => _UserBirth;
            set
            {
                _UserBirth = value;
                OnPropertyChanged();
            }
        }

        private string _UserTelephone;
        public string UserTelephone
        {
            get => _UserTelephone;
            set
            {
                _UserTelephone = value;
                OnPropertyChanged();
            }
        }

        private string _UserCMND;
        public string UserCMND
        {
            get => _UserCMND;
            set
            {
                _UserCMND = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeUserInformation { get; set; }
        public ICommand ResetUserInformation { get; set; }

        // Change password
        private bool _IsEnableOldPasswordField;
        public bool IsEnableOldPasswordField
        {
            get => _IsEnableOldPasswordField; set
            {
                _IsEnableOldPasswordField = value;
                OnPropertyChanged();
            }
        }
        private string _OldPassword;
        public string OldPassword
        {
            get => _OldPassword; set
            {
                _OldPassword = value;
                OnPropertyChanged();
            }
        }
        private string _NewPassword;
        public string NewPassword
        {
            get => _NewPassword; set
            {
                _NewPassword = value;
                OnPropertyChanged();
            }
        }

        private bool _IsCorrectPassword;
        public bool IsCorrectPassword
        {
            get => _IsCorrectPassword; set
            {
                _IsCorrectPassword = value;
                OnPropertyChanged();
            }
        }
        public ICommand OldPasswordChangedCommand { get; set; }
        public ICommand NewPasswordChangedCommand { get; set; }
        public ICommand CheckPassword { get; set; }
        public ICommand ChangePassword { get; set; }
        public ICommand CancelChangePassword { get; set; }

        // Search Wage

        private string _WageName { get; set; }
        public string WageName { get => _WageName; set { _WageName = value; OnPropertyChanged(); } }
        private string _WageValue { get; set; }
        public string WageValue { get => _WageValue; set { _WageValue = value; OnPropertyChanged(); } }
        private ObservableCollection<WAGE> _TempListWage { get; set; }
        public ObservableCollection<WAGE> TempListWage { get => _TempListWage; set { _TempListWage = value; OnPropertyChanged(); } }
        public ICommand SearchWageCommand { get; set; }
        public ICommand RefeshWageCommand { get; set; }

        // Search Brand
        private string _BrandName { get; set; }
        public string BrandName { get => _BrandName; set { _BrandName = value; OnPropertyChanged(); } }
        private ObservableCollection<CAR_BRAND> _TempListBrand { get; set; }
        public ObservableCollection<CAR_BRAND> TempListBrand { get => _TempListBrand; set { _TempListBrand = value; OnPropertyChanged(); } }
        public ICommand SearchBrandCommand { get; set; }
        public ICommand RefeshBrandCommand { get; set; }

        //Role
        bool _isSettingApp = false;
        public bool isSettingApp { get => _isSettingApp; set { _isSettingApp = value; OnPropertyChanged(); } }

        public SettingViewModel()
        {
            // Visibility
            AppSettingVis = true;
            UserSettingVis = false;

            ChangeAppSettingVis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AppSettingVis = true;
                UserSettingVis = false;
            });
            ChangeUserSettingVis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserSettingVis = true;
                AppSettingVis = false;
            });
            // Search Brand
            SearchBrandCommand = new RelayCommand<SettingWindow>((p) => {
                if (p == null) return false;
                if (string.IsNullOrEmpty(p.txtBrand.Text))
                    return false;
                return true;
            }, (p) =>
            {
                ListCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
                UnicodeConvert uni = new UnicodeConvert();
                TempListBrand = new ObservableCollection<CAR_BRAND>();

                foreach (var item in ListCarBrand)
                {
                    if ((!string.IsNullOrEmpty(p.txtBrand.Text)
                    && uni.RemoveUnicode(item.CarBrand_Name).ToLower().Contains(uni.RemoveUnicode(p.txtBrand.Text).ToLower())))
                        TempListBrand.Add(item);
                }
                ListCarBrand = TempListBrand;
            });
            RefeshBrandCommand = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                BrandName = "";
                ListCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            });
            // Search Wage
            SearchWageCommand = new RelayCommand<SettingWindow>((p) => {
                if (p == null) return false;
                if (string.IsNullOrEmpty(p.txbWageName.Text)
                    && string.IsNullOrEmpty(p.txbWageValue.Text))
                    return false;
                return true;
            }, (p) => 
            {
                ListWage = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
                UnicodeConvert uni = new UnicodeConvert();
                TempListWage = new ObservableCollection<WAGE>();

                foreach (var item in ListWage)
                {
                if (((!string.IsNullOrEmpty(p.txbWageName.Text)
                && uni.RemoveUnicode(item.Wage_Name).ToLower().Contains(uni.RemoveUnicode(p.txbWageName.Text).ToLower()))
                || (string.IsNullOrEmpty(p.txbWageName.Text)))
                &&
                (((!string.IsNullOrEmpty(p.txbWageValue.Text)
                && uni.RemoveUnicode(item.Wage_Value.ToString()).Contains(uni.RemoveUnicode(p.txbWageValue.Text)))
                    || (string.IsNullOrEmpty(p.txbWageValue.Text))))) 
                        
                TempListWage.Add(item);
                }
                ListWage = TempListWage;
            });
            RefeshWageCommand = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                WageName = "";
                WageValue = "";
                ListWage = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
            });
            // Gara information
            GaraInfo = DataProvider.Ins.DB.GARA_INFO.First();
            MaxCarReception = GaraInfo.MaxCarReception;
            IsOverPay = GaraInfo.IsOverPay;

            ChangeGaraInformation = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtMaxCarReception.Text)
                               || p.txtMaxCarReception.Text.Any(x => char.IsLetter(x)))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                String query_string = "update GARA_INFO set MaxCarReception=" + this.MaxCarReception
                                        + "where MaxCarReception=" + GaraInfo.MaxCarReception.ToString();
                SqlConnection connection = new SqlConnection("Data Source=PARACETAMOL;Initial Catalog=GARA;" +
                                                                    "Integrated Security=True");
                connection.Open();

                SqlCommand command = new SqlCommand(query_string, connection);

                command.ExecuteNonQuery();
                connection.Close();

                SetEnableStatusButtonInGaraInformation(false);
            });
            ResetGaraInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaxCarReception = GaraInfo.MaxCarReception;
                IsOverPay = GaraInfo.IsOverPay;
                SetEnableStatusButtonInGaraInformation(false);
            });

            // Car brand information
            ListCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);

            BrandSelectionChanged = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                SelectedBrandItems = new ObservableCollection<CAR_BRAND>(p.SelectedItems.Cast<CAR_BRAND>().ToList());
            });
            OpenModifyCarBrandWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedBrandItems.Count > 1)
                {
                    MessageBox.Show("Vui lòng chỉ chọn một hàng dữ liệu");
                    return;
                }
                ModifyCarBrandWindow window = new ModifyCarBrandWindow(SelectedBrandItem);
                window.ShowDialog();
                SelectedBrandItem = null;
                IsEnableModifyButtonInBrandSetting = false;
                IsEnableModifyFieldInBrandSetting = false;
                IsEnableDeleteButtonInBrandSetting = false;
                ListCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            });
            DeleteCarBrand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ObservableCollection<RECEPTION> receptions = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
                    foreach (var item in SelectedBrandItems)
                    {
                        if(receptions.Any(x => x.IdCarBrand == item.CarBrand_Id))
                        {
                            MessageBox.Show("Không thể xóa hãng xe " + item.CarBrand_Name + " vì tồn tại trong tiếp nhận xe!");
                            continue;
                        }
                        DataProvider.Ins.DB.CAR_BRAND.Remove(item);
                        ListCarBrand.Remove(item);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    IsEnableModifyButtonInBrandSetting = false;
                    IsEnableModifyFieldInBrandSetting = false;
                    IsEnableDeleteButtonInBrandSetting = false;
                }
            });
            OpenAddCarBrandWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCarBrandWindow window = new AddCarBrandWindow();
                window.ShowDialog();
                AddCarBrandViewModel addCarBrandViewModel = window.DataContext as AddCarBrandViewModel;
                ListCarBrand.Add(addCarBrandViewModel.br);
            });

            // Wage information
            ListWage = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);

            WageSelectionChanged = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                SelectedWageItems = new ObservableCollection<WAGE>(p.SelectedItems.Cast<WAGE>().ToList());
            });
            OpenModifyWageWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedWageItems.Count > 1)
                {
                    MessageBox.Show("Vui lòng chỉ chọn một hàng dữ liệu");
                    return; 
                }
                ModifyWageWindow window = new ModifyWageWindow(SelectedWageItem);
                window.ShowDialog();
                SelectedWageItem = null;
                IsEnableModifyButtonInWageSetting = false;
                IsEnableModifyFieldInWageSetting = false;
                IsEnableDeleteButtonInWageSetting = false;
                ListWage = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
            });
            DeleteWage = new RelayCommand<object>((p) => 
            { 
                if(SelectedWageItem == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                if(MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ObservableCollection<REPAIR_DETAIL> details = new ObservableCollection<REPAIR_DETAIL>(DataProvider.Ins.DB.REPAIR_DETAIL);
                    foreach(var item in SelectedWageItems)
                    {
                        if(details.Any(x => x.IdWage == item.Wage_Id))
                        {
                            MessageBox.Show("Không thể xóa loại tiền công " + item.Wage_Name + " vì tồn tại trong chi tiết sửa chữa!");
                            continue;
                        }
                        DataProvider.Ins.DB.WAGEs.Remove(item);
                        ListWage.Remove(item);
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    IsEnableModifyButtonInWageSetting = false;
                    IsEnableModifyFieldInWageSetting = false;
                    IsEnableDeleteButtonInWageSetting = false;
                }
            });
            OpenAddWageWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddWageWindow window = new AddWageWindow();
                window.ShowDialog();
                AddWageViewModel addWageViewModel = window.DataContext as AddWageViewModel;
                ListWage.Add(addWageViewModel.wage);
            });

            // User information
            ChangeUserInformation = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtAddress.Text)
                             || string.IsNullOrEmpty(p.txtName.Text)
                             || string.IsNullOrEmpty(p.dpBirth.DisplayDate.ToShortDateString())
                             || !DateTime.TryParse(p.dpBirth.DisplayDate.ToShortDateString(), out var value)
                                 || string.IsNullOrEmpty(p.txtTelephone.Text) || p.txtTelephone.Text.Any(x => char.IsLetter(x))
                                 || string.IsNullOrEmpty(p.txtCMND.Text))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                userInfo.UserInfo_Name = _UserName;
                userInfo.UserInfo_Address = _UserAddress;
                userInfo.UserInfo_BirthDate = _UserBirth;
                userInfo.UserInfo_Telephone = UserTelephone;
                userInfo.UserInfo_CMND = UserCMND;
                DataProvider.Ins.DB.SaveChanges();
                SetEnableStatusButtonInUserInformation(false);
            });
            ResetUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserName = userInfo.UserInfo_Name;
                UserAddress = userInfo.UserInfo_Address;
                UserBirth = userInfo.UserInfo_BirthDate.Value;
                UserTelephone = userInfo.UserInfo_Telephone;
                UserCMND = userInfo.UserInfo_CMND;
                SetEnableStatusButtonInUserInformation(false);
            });

            // Change password
            IsCorrectPassword = false;
            IsEnableCheckButtonInChangePassword = true;
            IsEnableOldPasswordField = true;

            OldPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { OldPassword = p.Password; });
            NewPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPassword = p.Password; });
            CheckPassword = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {

                string encode = MD5Hash(Base64Encode(OldPassword));
                if(encode != user.Password)
                {
                    MessageBox.Show("Sai mật khẩu!");
                    return;
                }

                p.OldPasswordBox.Password = string.Empty;
                IsEnableOldPasswordField = false;
                IsEnableCheckButtonInChangePassword = false;
                IsCorrectPassword = true;
            });
            ChangePassword = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.NewPasswordBox.Password)
                            || p.NewPasswordBox.Password.Length < 5)
                {
                    return false;
                }
                return true;

            }, (p) =>
            {
                string encode = MD5Hash(Base64Encode(NewPassword));
                user.Password = encode;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công");

                p.NewPasswordBox.Password = string.Empty;
                IsCorrectPassword = false;
                IsEnableCheckButtonInChangePassword = true;
                IsEnableOldPasswordField = true;
            });
            CancelChangePassword = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                p.NewPasswordBox.Password = string.Empty;
                IsCorrectPassword = false;
                IsEnableCheckButtonInChangePassword = true;
                IsEnableOldPasswordField = true;
            });

            // Change enable button
            ChangeEnableButtonInUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SetEnableStatusButtonInUserInformation(true);
            });
            ChangeEnableButttonInGaraInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SetEnableStatusButtonInGaraInformation(true);
            });
            ChangeEnableButtonInCarBrandSetting = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsEnableModifyButtonInBrandSetting = true;
            });
            ChangeEnableButtonInWageBrandSetting = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsEnableModifyButtonInWageSetting = true;
            });

            // Set enable button to false
            SetEnableStatusButtonInGaraInformation(false);
        }
        public SettingViewModel(string username, bool role) : this()
        {
            // User information
            user = DataProvider.Ins.DB.USERS.Where(x => x.UserName == username).FirstOrDefault();
            userInfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == user.Users_Id).FirstOrDefault();
            if (userInfo != null)
            {
                UserName = userInfo.UserInfo_Name;
                UserAddress = userInfo.UserInfo_Address;
                UserBirth = userInfo.UserInfo_BirthDate.Value;
                UserTelephone = userInfo.UserInfo_Telephone;
                UserCMND = userInfo.UserInfo_CMND;
            }

            //Set role
            isSettingApp = role;
        }

        private void SetEnableStatusButtonInGaraInformation(bool b)
        {
            IsEnableChangeButtonInGaraInformation = b;
            IsEnableResetButtonInGaraInformation = b;
        }
        private void SetEnableStatusButtonInUserInformation(bool b)
        {
            IsEnableChangeButtonInUserInformation = b;
            IsEnableResetButtonInUserInformation = b;
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
