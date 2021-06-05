using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            }
        }

        public ICommand ChangeEnableButtonInCarBrandSetting { get; set; }
        public ICommand ModifyCarBrand { get; set; }
        public ICommand DeleteCarBrand { get; set; }

        // Setting user information
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                SetEnableStatusButtonInUserInformation(true);
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
                SetEnableStatusButtonInUserInformation(true);
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
                SetEnableStatusButtonInUserInformation(true);
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
                SetEnableStatusButtonInUserInformation(true);
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

        USER_INFO userInfo;

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
                UserSettingVis = true; ;
                AppSettingVis = false;
            });

            // Gara information
            GaraInfo = DataProvider.Ins.DB.GARA_INFO.First();
            MaxCarReception = GaraInfo.MaxCarReception;
            IsOverPay = GaraInfo.IsOverPay;

            ChangeGaraInformation = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                GaraInfo.MaxCarReception = _MaxCarReception;
                GaraInfo.IsOverPay = _IsOverPay;
                DataProvider.Ins.DB.SaveChanges();
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

            ChangeUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
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

            // Set enable button to false
            IsEnableChangeButtonInGaraInformation = false;
            IsEnableChangeButtonInUserInformation = false;
            IsEnableResetButtonInGaraInformation = false;
            IsEnableResetButtonInUserInformation = false;


            // Change enable button
            ChangeEnableButtonInUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SetEnableStatusButtonInUserInformation(true);
            });
            ChangeEnableButttonInGaraInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SetEnableStatusButtonInGaraInformation(true);
            });
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
