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
using System.Configuration;
using System.Text.RegularExpressions;

namespace QuanLyGaraOto.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
        #region Command
            public ICommand CancelAddCarBrandWindow { get; set; }
            public ICommand AddCarBrand { get; set; }
            public ICommand ChangeUserInformation { get; set; }
            public ICommand ResetUserInformation { get; set; }
            public ICommand OldPasswordChangedCommand { get; set; }
            public ICommand NewPasswordChangedCommand { get; set; }
            public ICommand CheckPassword { get; set; }
            public ICommand ChangePassword { get; set; }
            public ICommand CancelChangePassword { get; set; }
            public ICommand SearchWageCommand { get; set; }
            public ICommand RefeshWageCommand { get; set; }
            public ICommand SearchBrandCommand { get; set; }
            public ICommand RefeshBrandCommand { get; set; }
            public ICommand SearchSupplierCommand { get; set; }
            public ICommand RefeshSupplierCommand { get; set; }
            public ICommand OpenAddSupplierWindow { get; set; }
            public ICommand OpenModifySupplierWindow { get; set; }
            public ICommand DeleteSupplier { get; set; }
            public ICommand SupplierSelectionChanged { get; set; }
            public ICommand ExportSupplierCommand { get; set; }
            public ICommand ChangeAppSettingVis { get; set; }
            public ICommand ChangeUserSettingVis { get; set; }
            public ICommand Logout { get; set; }
            public ICommand ChangeEnableButttonInGaraInformation { get; set; }
            public ICommand ChangeEnableButtonInUserInformation { get; set; }
            public ICommand ChangeGaraInformation { get; set; }
            public ICommand ResetGaraInformation { get; set; }
            public ICommand ChangeEnableButtonInCarBrandSetting { get; set; }
            public ICommand OpenModifyCarBrandWindow { get; set; }
            public ICommand DeleteCarBrand { get; set; }
            public ICommand OpenAddCarBrandWindow { get; set; }
            public ICommand BrandSelectionChanged { get; set; }
            public ICommand ExportBrandCommand { get; set; }
            public ICommand ChangeEnableButtonInWageBrandSetting { get; set; }
            public ICommand OpenModifyWageWindow { get; set; }
            public ICommand DeleteWage { get; set; }
            public ICommand OpenAddWageWindow { get; set; }
            public ICommand WageSelectionChanged { get; set; }
            public ICommand ExportWageCommand { get; set; }
            public ICommand CancelAddWageWindow { get; set; }
            public ICommand AddWage { get; set; }
        #endregion

        #region Vis
            private bool _AppSettingVis;
            public bool AppSettingVis { get => _AppSettingVis; set { _AppSettingVis = value; OnPropertyChanged(); } }

            private bool _UserSettingVis;
            public bool UserSettingVis { get => _UserSettingVis; set { _UserSettingVis = value; OnPropertyChanged(); } }
        #endregion

        #region Enabled
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
        #endregion

        #region Data 
            public USER user;
            private GARA_INFO GaraInfo;
            USER_INFO userInfo;
            private int _MaxCarReception;
            public int MaxCarReception { get => _MaxCarReception; set { _MaxCarReception = value; OnPropertyChanged(); } }

            private bool _IsOverPay;
            public bool IsOverPay { get => _IsOverPay; set { _IsOverPay = value; OnPropertyChanged(); } }
        #endregion

        #region List 
            private ObservableCollection<CAR_BRAND> _ListCarBrand;
            public ObservableCollection<CAR_BRAND> ListCarBrand
            {
                get => _ListCarBrand; set
                {
                    _ListCarBrand = value;
                    OnPropertyChanged();
                }
            }
            public ObservableCollection<CAR_BRAND> SelectedBrandItems { get; set; }
            private ObservableCollection<CAR_BRAND> _TempListBrand { get; set; }
            public ObservableCollection<CAR_BRAND> TempListBrand { get => _TempListBrand; set { _TempListBrand = value; OnPropertyChanged(); } }
            private ObservableCollection<WAGE> _ListWage;
            public ObservableCollection<WAGE> ListWage
            {
                get => _ListWage; set
                {
                    _ListWage = value;
                    OnPropertyChanged();
                }
            }
            private ObservableCollection<WAGE> _TempListWage { get; set; }
            public ObservableCollection<WAGE> TempListWage { get => _TempListWage; set { _TempListWage = value; OnPropertyChanged(); } }
            public ObservableCollection<WAGE> SelectedWageItems { get; set; }
            private ObservableCollection<SUPPLIER> _ListSupplier { get; set; }
            public ObservableCollection<SUPPLIER> ListSupplier { get => _ListSupplier; set { _ListSupplier = value; OnPropertyChanged(); } }
            private ObservableCollection<SUPPLIER> _TempListSupplier { get; set; }
            public ObservableCollection<SUPPLIER> TempListSupplier { get => _TempListSupplier; set { _TempListSupplier = value; OnPropertyChanged(); } }


        #endregion

        #region data tao lao di do
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

        private string _CarBrandInAdd;
        public string CarBrandInAdd
        {
            get => _CarBrandInAdd; set
            {
                _CarBrandInAdd = value;
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
        private string _WageName { get; set; }
        public string WageName { get => _WageName; set { _WageName = value; OnPropertyChanged(); } }
        private string _WageValue { get; set; }
        public string WageValue { get => _WageValue; set { _WageValue = value; OnPropertyChanged(); } }

        private string _BrandName { get; set; }
        public string BrandName { get => _BrandName; set { _BrandName = value; OnPropertyChanged(); } }

        bool _isGaraInfo = false;
        public bool isGaraInfo { get => _isGaraInfo; set { _isGaraInfo = value; OnPropertyChanged(); } }

        bool _isWage = false;
        public bool isWage { get => _isWage; set { _isWage = value; OnPropertyChanged(); } }

        bool _isCarBranch = false;
        public bool isCarBranch { get => _isCarBranch; set { _isCarBranch = value; OnPropertyChanged(); } }

        bool _isSuplier = false;
        public bool isSuplier { get => _isSuplier; set { _isSuplier = value; OnPropertyChanged(); } }

        private SUPPLIER _SelectedSupplier { get; set; }
        public SUPPLIER SelectedSupplier { get => _SelectedSupplier; set { _SelectedSupplier = value; OnPropertyChanged(); } }
        private string _SupplierPhone { get; set; }
        public string SupplierPhone { get => _SupplierPhone; set { _SupplierPhone = value; OnPropertyChanged(); } }

        #endregion

        public SettingViewModel()
        {
            // Visibility
            AppSettingVis = true;
            UserSettingVis = false;
            // All command and setting Supplier
            LoadListBrand();
            LoadListSupplier();
            LoadListWage();

            OpenAddSupplierWindow = new RelayCommand<SettingWindow>((p) => { if (isSuplier == false) return false; return true; }, (p) =>
            {
                AddSupplierWindow window = new AddSupplierWindow();
                window.ShowDialog();
                LoadListSupplier();
            });
            OpenModifySupplierWindow = new RelayCommand<SettingWindow>((p) => {

                if (SelectedSupplier == null) return false;
                if (isSuplier == false) return false;
                return true; }, (p) =>
            {
                EditSupplierWindow window = new EditSupplierWindow(SelectedSupplier);
                window.ShowDialog();
                LoadListSupplier();
            });
            SupplierSelectionChanged = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                TempListSupplier = new ObservableCollection<SUPPLIER>(p.SelectedItems.Cast<SUPPLIER>().ToList());
            });
            DeleteSupplier = new RelayCommand<SettingWindow>((p) =>
            {
                if (SelectedSupplier == null)
                {
                    return false;
                }
                if (isSuplier == false)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                string s = "";
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ObservableCollection<IMPORT_GOODS> details = new ObservableCollection<IMPORT_GOODS>(DataProvider.Ins.DB.IMPORT_GOODS);
                    foreach (var item in TempListSupplier)
                    {
                        if (details.Any(x => x.IdSupplier == item.Supplier_Id))
                        {
                            s = s + "Không thể xóa nhà cung cấp " + item.Supplier_Name + " vì đang được sử dụng!" + "\n";
                            continue;
                        }
                        DataProvider.Ins.DB.SUPPLIERs.Remove(item);
                        ListSupplier.Remove(item);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show(s , "Thông báo", MessageBoxButton.OK);
                }
            });
            SearchSupplierCommand = new RelayCommand<SettingWindow>((p) => {
                if (p == null) return false;

                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch(p.txbPhoneSupplier.Text) && !string.IsNullOrEmpty(p.txbPhoneSupplier.Text)) return false;

                if (string.IsNullOrEmpty(p.txbSupplier.Text) && string.IsNullOrEmpty(p.txbSupplierEmail.Text)
                && string.IsNullOrEmpty(p.txbPhoneSupplier.Text))
                    return false;
                return true;
            }, (p) =>
            {
                LoadListSupplier();
                UnicodeConvert uni = new UnicodeConvert();
                TempListSupplier = new ObservableCollection<SUPPLIER>();

                foreach (var item in ListSupplier)
                {
                    if ((string.IsNullOrEmpty(p.txbSupplier.Text) ||
                    (!string.IsNullOrEmpty(p.txbSupplier.Text) && uni.RemoveUnicode(item.Supplier_Name).ToLower().Contains(uni.RemoveUnicode(p.txbSupplier.Text).ToLower())))
                    && ((string.IsNullOrEmpty(p.txbPhoneSupplier.Text) ||
                    (!string.IsNullOrEmpty(p.txbPhoneSupplier.Text) && uni.RemoveUnicode(item.Supplier_Phone).ToLower().Contains(uni.RemoveUnicode(p.txbPhoneSupplier.Text).ToLower()))))
                    && ((string.IsNullOrEmpty(p.txbSupplierEmail.Text) ||
                    (!string.IsNullOrEmpty(p.txbSupplierEmail.Text) && uni.RemoveUnicode(item.Supplier_Email).ToLower().Contains(uni.RemoveUnicode(p.txbSupplierEmail.Text).ToLower()))))
                    )
                        TempListSupplier.Add(item);
                }
                ListSupplier = TempListSupplier;
            });
            RefeshSupplierCommand = new RelayCommand<SettingWindow>((p) => { return true; }, (p) =>
            {
                p.txbSupplier.Text = "";
                p.txbPhoneSupplier.Text = "";
                p.txbSupplierEmail.Text = "";
                LoadListSupplier();
            });


            //------------------------------------------
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
            //  Brand
            SearchBrandCommand = new RelayCommand<SettingWindow>((p) => {
                if (p == null) return false;
                if (string.IsNullOrEmpty(p.txtBrand.Text))
                    return false;
                return true;
            }, (p) =>
            {
                LoadListBrand();
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
                LoadListBrand();
            });
            // Search Wage
            SearchWageCommand = new RelayCommand<SettingWindow>((p) => {
                if (p == null) return false;
                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch(p.txbWageValue.Text) && !string.IsNullOrEmpty(p.txbWageValue.Text)) return false;

                if (string.IsNullOrEmpty(p.txbWageName.Text)
                    && string.IsNullOrEmpty(p.txbWageValue.Text))
                    return false;
                return true;
            }, (p) => 
            {
                LoadListWage();
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
                LoadListWage();
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
                if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    String query_string = "update GARA_INFO set MaxCarReception=" + this.MaxCarReception + 
                                                ", IsOverPay=" + (this.IsOverPay ? "1" : "0")
                                            + "where MaxCarReception=" + GaraInfo.MaxCarReception.ToString();
                    string connectionString = ConfigurationManager.ConnectionStrings["GARAEntities"].ConnectionString;
                    if (connectionString.ToLower().StartsWith("metadata="))
                    {
                        System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder 
                        = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(connectionString);

                        connectionString = efBuilder.ProviderConnectionString;
                    }

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();

                    SqlCommand command = new SqlCommand(query_string, connection);

                    command.ExecuteNonQuery();
                    connection.Close();

                    SetEnableStatusButtonInGaraInformation(false);
                }
            });
            ResetGaraInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaxCarReception = GaraInfo.MaxCarReception;
                IsOverPay = GaraInfo.IsOverPay;
                SetEnableStatusButtonInGaraInformation(false);
            });

            // Car brand information
            LoadListBrand();

            BrandSelectionChanged = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                SelectedBrandItems = new ObservableCollection<CAR_BRAND>(p.SelectedItems.Cast<CAR_BRAND>().ToList());
            });
            OpenModifyCarBrandWindow = new RelayCommand<object>((p) => {
                if (SelectedBrandItem == null) return false;
                if (isCarBranch == false) return false;  return true; }, (p) =>
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
                LoadListBrand();
            });
            DeleteCarBrand = new RelayCommand<object>((p) => { 
                if (SelectedBrandItem == null ) return false;
                if (isCarBranch == false) return false; return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ObservableCollection<RECEPTION> receptions = new ObservableCollection<RECEPTION>(DataProvider.Ins.DB.RECEPTIONs);
                    foreach (var item in SelectedBrandItems)
                    {
                        if(receptions.Any(x => x.IdCarBrand == item.CarBrand_Id)
                        || DataProvider.Ins.DB.SALES_REPORT_DETAIL.Where(x => x.IdCarBrand == item.CarBrand_Id).Count()>0)
                        {
                            MessageBox.Show("Không thể xóa hãng xe " + item.CarBrand_Name + " vì đang được sử dụng!");
                            continue;
                        }
                        DataProvider.Ins.DB.CAR_BRAND.Remove(item);
                        DataProvider.Ins.DB.SaveChanges();
                        ListCarBrand.Remove(item);
                    }
                    IsEnableModifyButtonInBrandSetting = false;
                    IsEnableModifyFieldInBrandSetting = false;
                    IsEnableDeleteButtonInBrandSetting = false;
                }
            });
            OpenAddCarBrandWindow = new RelayCommand<object>((p) => { if (isCarBranch == false) return false; return true; }, (p) =>
            {
                AddCarBrandWindow window = new AddCarBrandWindow();
                window.ShowDialog();
                AddCarBrandViewModel addCarBrandViewModel = window.DataContext as AddCarBrandViewModel;
                if (addCarBrandViewModel.br != null)
                    ListCarBrand.Insert(0,addCarBrandViewModel.br);
            });

            ExportBrandCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PrintViewModel printViewModel = new PrintViewModel();
                printViewModel.Print_ThongTinHangXe(ListCarBrand);
            });
            // Wage information
            LoadListWage();

            WageSelectionChanged = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                SelectedWageItems = new ObservableCollection<WAGE>(p.SelectedItems.Cast<WAGE>().ToList());
            });
            OpenModifyWageWindow = new RelayCommand<object>((p) => {
                if (SelectedWageItem == null) return false;
                if (isWage == false) return false; return true; }, (p) =>
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
                LoadListWage();
            });
            DeleteWage = new RelayCommand<object>((p) => 
            { 
                if(SelectedWageItem == null)
                {
                    return false;
                }
                if (isWage == false)
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
            OpenAddWageWindow = new RelayCommand<object>((p) => { if (isWage == false) return false; return true; }, (p) =>
            {
                AddWageWindow window = new AddWageWindow();
                window.ShowDialog();
                AddWageViewModel addWageViewModel = window.DataContext as AddWageViewModel;
                if (addWageViewModel.wage !=null) 
                ListWage.Insert(0,addWageViewModel.wage);
            });
            ExportWageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                  PrintViewModel printViewModel = new PrintViewModel();
                  printViewModel.Print_ThongTinTienCong(ListWage);

             });
            ExportSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PrintViewModel printViewModel = new PrintViewModel();
                printViewModel.XuatDanhSachNhaCungCap(ListSupplier);

            });
            // User information
            ChangeUserInformation = new RelayCommand<SettingWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.txtAddress.Text)
                                || string.IsNullOrEmpty(p.txtName.Text)
                                || string.IsNullOrEmpty(p.dpBirth.Text)
                                || string.IsNullOrEmpty(p.txtTelephone.Text) 
                                || p.txtTelephone.Text.Any(x => !(x >= '0' && x <= '9'))
                                || string.IsNullOrEmpty(p.txtCMND.Text))
                {

                    return false;
                }
                return true;
            }, (p) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    userInfo.UserInfo_Name = _UserName;
                    userInfo.UserInfo_Address = _UserAddress;
                    userInfo.UserInfo_BirthDate = _UserBirth;
                    userInfo.UserInfo_Telephone = UserTelephone;
                    userInfo.UserInfo_CMND = UserCMND;
                    DataProvider.Ins.DB.SaveChanges();
                    SetEnableStatusButtonInUserInformation(false);
                }
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
            CheckPassword = new RelayCommand<SettingWindow>((p) => 
            { 
                if(string.IsNullOrEmpty(OldPassword))
                {
                    return false;
                }
                return true;
            }, (p) =>
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
                if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string encode = MD5Hash(Base64Encode(NewPassword));
                    user.Password = encode;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công");

                    p.NewPasswordBox.Password = string.Empty;
                    IsCorrectPassword = false;
                    IsEnableCheckButtonInChangePassword = true;
                    IsEnableOldPasswordField = true;
                }
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
        public SettingViewModel(string username, bool garaInfo, bool wage, bool carBranch, bool suplier, MainWindow mainWindow) : this()
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
            isGaraInfo = garaInfo;
            isWage = wage;
            isCarBranch = carBranch;
            isSuplier = suplier;

            // Log out
            Logout = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                mainWindow.DataContext = new MainViewModel();
                LoadLoginWindow(mainWindow);
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

        public void LoadLoginWindow(MainWindow p)
        {
            p.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel();
            loginWindow.ShowDialog();
            var loginVM = loginWindow.DataContext as LoginViewModel;
            //loginVM.IsLogin = false;
            if (loginVM.IsLogin)
            {
                p.Show();
                (p.DataContext as MainViewModel).User = loginVM.User;
                (p.DataContext as MainViewModel).LoadRole();
            }
            else
            {
                p.Close();
            }
        }
        public void LoadListWage()
        {
            ListWage = new ObservableCollection<WAGE>();
            ObservableCollection<WAGE> tempWage = new ObservableCollection<WAGE>(DataProvider.Ins.DB.WAGEs);
            foreach (var item in tempWage)
            {
                ListWage.Insert(0, item);
            }
        }
        public void LoadListBrand()
        {
            ListCarBrand = new ObservableCollection<CAR_BRAND>();
            ObservableCollection<CAR_BRAND> tempCarBrand = new ObservableCollection<CAR_BRAND>(DataProvider.Ins.DB.CAR_BRAND);
            foreach (var item in tempCarBrand)
            {
                ListCarBrand.Insert(0, item);
            }
        }
        public void LoadListSupplier()
        {
            ListSupplier = new ObservableCollection<SUPPLIER>();
            ObservableCollection<SUPPLIER> tempSupplier = new ObservableCollection<SUPPLIER>(DataProvider.Ins.DB.SUPPLIERs);
            foreach (var item in tempSupplier)
            {
                ListSupplier.Insert(0, item);
            }
        }
    }

}
