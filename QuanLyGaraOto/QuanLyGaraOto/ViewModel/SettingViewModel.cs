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

        private bool _BrandSettingVis;
        public bool BrandSettingVis
        {
            get => _BrandSettingVis;
            set
            {
                _BrandSettingVis = value;
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
        public ICommand ChangeBrandSettingVis { get; set; }
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

        private string _Telephone;
        public string Telephone
        {
            get => _Telephone;
            set
            {
                _Telephone = value;
                OnPropertyChanged();
            }
        }

        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }

        private string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeGaraInformation { get; set; }
        public ICommand ResetGaraInformation { get; set; }

        GARA_INFO garaInfo;

        // Setting car brand
        private ObservableCollection<CAR_BRAND> _ListBrands;
        public ObservableCollection<CAR_BRAND> ListBrands
        {
            get => _ListBrands;
            set { _ListBrands = value; OnPropertyChanged(); }
        }

        private CAR_BRAND _SelectedBrand;
        public CAR_BRAND SelectedBrand
        {
            get => _SelectedBrand;
            set { _SelectedBrand = value; OnPropertyChanged(); }
        }

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
            BrandSettingVis = false;
            UserSettingVis = false;

            ChangeAppSettingVis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AppSettingVis = true;
                BrandSettingVis = false;
                UserSettingVis = false;
            });

            ChangeBrandSettingVis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BrandSettingVis = true;
                AppSettingVis = false;
                UserSettingVis = false;
            });

            ChangeUserSettingVis = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserSettingVis = true; ;
                BrandSettingVis = false;
                AppSettingVis = false;
            });

            // Gara information
            garaInfo = DataProvider.Ins.DB.GARA_INFO.SingleOrDefault(x => x.GaraInfo_Id == 1);
            if (garaInfo != null)
            {
                MaxCarReception = garaInfo.MaxCarReception.Value;
            }

            ChangeGaraInformation = new RelayCommand<object>(
                (p) =>
                {
                    if (garaInfo != null) return true;
                    return false;
                },
                (p) =>
                {
                    garaInfo.MaxCarReception = _MaxCarReception;
                    DataProvider.Ins.DB.SaveChanges();
                    SetEnableStatusButtonInGaraInformation(false);
                });

            ResetGaraInformation = new RelayCommand<object>(
                (p) =>
                {
                    if (garaInfo != null) return true;
                    return false;
                },
                (p) =>
                {
                    MaxCarReception = garaInfo.MaxCarReception.Value;

                    SetEnableStatusButtonInGaraInformation(false);
                });

            // Car brands
            ListBrands = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);

            IsEnableDeleteButtonInBrandSetting = false;
            IsEnableModifyButtonInBrandSetting = false;

            ChangeEnableButtonInCarBrandSetting = new RelayCommand<object>(
                (p) =>
                {
                    if (_SelectedBrand == null) return false;
                    return true;
                },
                (p) =>
                {
                    SetEnableStatusButotnInCarBrandSetting(true);
                }
            );

            ModifyCarBrand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // modify in database
                SetEnableStatusButotnInCarBrandSetting(false);
                SelectedBrand = null;
            });

            DeleteCarBrand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // delete in database
                SetEnableStatusButotnInCarBrandSetting(false);
                SelectedBrand = null;
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
        private void SetEnableStatusButotnInCarBrandSetting(bool b)
        {
            IsEnableModifyButtonInBrandSetting = b;
            IsEnableDeleteButtonInBrandSetting = b;
        }
    }
}
