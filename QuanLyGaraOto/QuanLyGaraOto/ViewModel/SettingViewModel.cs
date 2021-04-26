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


        // Setting gara information
        private int _MaxCarReception;
        public int MaxCarReception
        {
            get => _MaxCarReception;
            set
            {
                _MaxCarReception = value;
                SetEnableStatusButtonInGaraInformation(true);
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
                SetEnableStatusButtonInGaraInformation(true);
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
                SetEnableStatusButtonInGaraInformation(true);
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
                SetEnableStatusButtonInGaraInformation(true);
                OnPropertyChanged();
            }
        }

        public ICommand ChangeGaraInformation { get; set; }
        public ICommand ResetGaraInformation { get; set; }

        GaraInfo garaInfo;

        // Setting car brand
        private ObservableCollection<CarBrand> _ListBrands;
        public ObservableCollection<CarBrand> ListBrands
        {
            get => _ListBrands;
            set { _ListBrands = value; OnPropertyChanged(); }
        }

        private CarBrand _SelectedBrand;
        public CarBrand SelectedBrand
        {
            get => _SelectedBrand;
            set { _SelectedBrand = value; OnPropertyChanged(); }
        }

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
                SetEnableStatusButtonInUserInformation(true);
                OnPropertyChanged();
            }
        }

        public ICommand ChangeUserInformation { get; set; }
        public ICommand ResetUserInformation { get; set; }

        UserInfo userInfo;

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
            garaInfo = DataProvider.Ins.DB.GaraInfoes.SingleOrDefault(x => x.Id == 1);
            if (garaInfo != null)
            {
                MaxCarReception = garaInfo.MaxCarReception.Value;
                Telephone = garaInfo.Phone;
                Email = garaInfo.Email;
                Address = garaInfo.Address;
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
                    garaInfo.Phone = _Telephone;
                    garaInfo.Email = _Email;
                    garaInfo.Address = _Address;
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
                    Telephone = garaInfo.Phone;
                    Email = garaInfo.Email;
                    Address = garaInfo.Address;
                    SetEnableStatusButtonInGaraInformation(false);
                });

            // Car brands
            ListBrands = new ObservableCollection<CarBrand>(DataProvider.Ins.DB.CarBrands);

            // User information
            userInfo = DataProvider.Ins.DB.UserInfoes.Where(x => x.IdUser == 1).FirstOrDefault();
            if (userInfo != null)
            {
                UserName = userInfo.Name;
                UserAddress = userInfo.Address;
                UserBirth = userInfo.BirthDate.Value;
                UserTelephone = userInfo.Telephone;
                UserCMND = userInfo.CMND;
            }

            ChangeUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                userInfo.Name = _UserName;
                userInfo.Address = _UserAddress;
                userInfo.BirthDate = _UserBirth;
                userInfo.Telephone = UserTelephone;
                userInfo.CMND = UserCMND;
                DataProvider.Ins.DB.SaveChanges();
                SetEnableStatusButtonInUserInformation(false);
            });

            ResetUserInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserName = userInfo.Name;
                UserAddress = userInfo.Address;
                UserBirth = userInfo.BirthDate.Value;
                UserTelephone = userInfo.Telephone;
                UserCMND = userInfo.CMND;
                SetEnableStatusButtonInUserInformation(false);
            });

            // Set enable button to false
            IsEnableChangeButtonInGaraInformation = false;
            IsEnableChangeButtonInUserInformation = false;
            IsEnableResetButtonInGaraInformation = false;
            IsEnableResetButtonInUserInformation = false;
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
