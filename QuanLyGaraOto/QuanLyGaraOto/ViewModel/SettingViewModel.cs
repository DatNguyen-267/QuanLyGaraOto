using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
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
        private bool _IsEnableChangeButtonInChangePassword;
        public bool IsEnableChangeButtonInChangePassword
        {
            get => _IsEnableChangeButtonInChangePassword; set
            {
                _IsEnableChangeButtonInChangePassword = value;
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
                IsEnableModifyFieldInBrandSetting = true;
            }
        }

        public ICommand ChangeEnableButtonInCarBrandSetting { get; set; }
        public ICommand ModifyCarBrand { get; set; }
        public ICommand DeleteCarBrand { get; set; }
        public ICommand OpenAddCarBrandWindow { get; set; }

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
        public ICommand CancelChangePassword { get; set; }

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

            // Gara information
            GaraInfo = DataProvider.Ins.DB.GARA_INFO.First();
            MaxCarReception = GaraInfo.MaxCarReception;
            IsOverPay = GaraInfo.IsOverPay;

            ChangeGaraInformation = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtMaxCarReception.Text))
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

            ModifyCarBrand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.SaveChanges();
                SelectedBrandItem = null;
                IsEnableModifyButtonInBrandSetting = false;
                IsEnableModifyFieldInBrandSetting = false;
                IsEnableDeleteButtonInBrandSetting = false;
            });
            DeleteCarBrand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataProvider.Ins.DB.CAR_BRAND.Remove(SelectedBrandItem);
                DataProvider.Ins.DB.SaveChanges();
                ListCarBrand.Remove(SelectedBrandItem);
                IsEnableModifyButtonInBrandSetting = false;
                IsEnableModifyFieldInBrandSetting = false;
                IsEnableDeleteButtonInBrandSetting = false;
            });
            OpenAddCarBrandWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCarBrandWindow window = new AddCarBrandWindow();
                window.ShowDialog();
                AddCarBrandViewModel addCarBrandViewModel = window.DataContext as AddCarBrandViewModel;
                ListCarBrand.Add(addCarBrandViewModel.br);
            });

            // User information
            userInfo = DataProvider.Ins.DB.USER_INFO.Where(x => x.IdUser == 1).FirstOrDefault();
            if (userInfo != null)
            {
                UserName = userInfo.UserInfo_Name;
                UserAddress = userInfo.UserInfo_Address;
                UserBirth = userInfo.UserInfo_BirthDate.Value;
                UserTelephone = userInfo.UserInfo_Telephone;
                UserCMND = userInfo.UserInfo_CMND;
            }

            ChangeUserInformation = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtAddress.Text)
                             || string.IsNullOrEmpty(p.txtName.Text)
                             || DateTime.TryParse(p.dpBirth.Text, out DateTime time)
                             || string.IsNullOrEmpty(p.txtTelephone.Text)
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
            CheckPassword = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("Checked");
                IsEnableOldPasswordField = false;
                IsEnableCheckButtonInChangePassword = false;
                IsCorrectPassword = true;
            });
            CancelChangePassword = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsCorrectPassword = false;
                IsEnableChangeButtonInChangePassword = false;
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

            // Set enable button to false
            SetEnableStatusButtonInGaraInformation(false);
        }

        private object Base64Encode(string oldPassword)
        {
            throw new NotImplementedException();
        }

        private string MD5Hash(object p)
        {
            throw new NotImplementedException();
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
    }
}
