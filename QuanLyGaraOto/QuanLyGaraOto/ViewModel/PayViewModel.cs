using QuanLyGaraOto.Model;
using QuanLyGaraOto.Template;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class PayViewModel : BaseViewModel
    {
        public bool IsPay {get;set;}
        
        private bool _VisValidationPay { get; set; }
        public bool VisValidationPay { get => _VisValidationPay; set {
                _VisValidationPay = value;
                    
                    OnPropertyChanged(); } }
        private bool _IsOverPay { get; set; }
        public bool IsOverPay { get => _IsOverPay; set { _IsOverPay = value; OnPropertyChanged(); } }

        private bool _VisOverDate { get; set; }
        public bool VisOverDate { get => _VisOverDate; set { _VisOverDate = value; OnPropertyChanged(); } }
        private bool _VisErrorDate { get; set; }
        public bool VisErrorDate { get => _VisErrorDate; set { _VisErrorDate = value; OnPropertyChanged(); } }

        private bool _RolReceivedMoney { get; set; }
        public bool RolReceivedMoney { get => _RolReceivedMoney; set { _RolReceivedMoney = value; OnPropertyChanged(); } }
        private bool _RolEmail { get; set; }
        public bool RolEmail { get => _RolEmail; set { _RolEmail = value; OnPropertyChanged(); } }
        private bool _EnabledReceiptDate { get; set; }
        public bool EnabledReceiptDate { get => _EnabledReceiptDate; set { _EnabledReceiptDate = value; OnPropertyChanged(); } }
        private bool _VisPay { get; set; }
        public bool VisPay { get => _VisPay; set { _VisPay = value; OnPropertyChanged(); } }
        private string _ReceivedMoney { get; set; }
        public string ReceivedMoney { get => _ReceivedMoney; set {
                        _ReceivedMoney = value;
                OnPropertyChanged(); } }
        private int _TotalMoney { get; set; }
        public int TotalMoney { get => _TotalMoney; set { _TotalMoney = value; OnPropertyChanged(); } }
        private string _Email { get; set; }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private DateTime _SelectedDate { get; set; }
        public DateTime SelectedDate { get => _SelectedDate; set { _SelectedDate = value; OnPropertyChanged(); } }
        private RECEPTION _Reception { get; set; }
        public RECEPTION Reception { get => _Reception; set { _Reception = value; OnPropertyChanged(); } }
        private REPAIR _Repair { get; set; }
        public REPAIR Repair { get => _Repair; set { _Repair = value; OnPropertyChanged(); } }
        private ObservableCollection<ListRepair> _ListRepair { get; set; }
        public ObservableCollection<ListRepair> ListRepair { get => _ListRepair; set { _ListRepair = value; OnPropertyChanged(); } }
        public ICommand PayCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CheckIsOverPay { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand CheckDate { get; set; }
        public PayViewModel()
        {

        }
        public PayViewModel(RECEPTION Reception)
        {
            
            TotalMoney = 0;
            SelectedDate = DateTime.Now.Date;
            this.Reception = Reception;
            InitData();
            ReceivedMoney = TotalMoney.ToString();
            Command();

            if (DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == Reception.Reception_Id).Count() > 0)
            {
                Email = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == Reception.Reception_Id).SingleOrDefault().Email;
                VisPay = false;
                RolReceivedMoney = true;
                RolEmail = true;
                EnabledReceiptDate = false;
                SelectedDate = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == Reception.Reception_Id).SingleOrDefault().ReceiptDate;
                ReceivedMoney = DataProvider.Ins.DB.RECEIPTs.Where(x => x.IdReception == Reception.Reception_Id).SingleOrDefault().MoneyReceived.ToString() ;
            }
        }
        public void Command()
        {
            PayCommand = new RelayCommand<PayWindow>((p) => {
                if (SelectedDate == null || string.IsNullOrEmpty(p.txtPay.Text) || string.IsNullOrEmpty(p.txbEmail.Text))
                {
                    return false;
                }
                if (VisErrorDate == true || VisOverDate == true) return false;
                Regex regex = new Regex(@"^[0-9]+$");
                if (!regex.IsMatch(p.txtPay.Text.ToString())) return false;
                if (int.Parse(ReceivedMoney) > Reception.Debt && !DataProvider.Ins.DB.GARA_INFO.FirstOrDefault().IsOverPay) return false;
                return true; }, (p) =>
            {

                if (MessageBox.Show("Bạn chắc chắn đồng ý thanh toán", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RECEIPT newReceipt = new RECEIPT();
                    newReceipt.ReceiptDate = SelectedDate;
                    newReceipt.MoneyReceived = int.Parse(ReceivedMoney);
                    newReceipt.Phone = Reception.CUSTOMER.Customer_Phone;
                    newReceipt.IdReception = Reception.Reception_Id;
                    newReceipt.Email = Email;
                    DataProvider.Ins.DB.RECEIPTs.Add(newReceipt);
                    DataProvider.Ins.DB.RECEPTIONs.Where(x => x.Reception_Id == Reception.Reception_Id).SingleOrDefault().Debt = 0;
                    DataProvider.Ins.DB.SaveChanges();
                    IsPay = true;
                    VisPay = false;
                    EnabledReceiptDate = false;
                    RolReceivedMoney = true;
                    RolEmail = true;
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => {
                return true;
            }, (p) =>
            {
                    p.Close();
            });
            CheckIsOverPay = new RelayCommand<TextBox>((p) => {
                return true;
            }, (p) =>
            {
               
                IsOverPay = true;
                VisValidationPay = false;
                try
                {
                    if (!(bool)DataProvider.Ins.DB.GARA_INFO.FirstOrDefault().IsOverPay && int.Parse(p.Text) > Reception.Debt)
                    {
                        IsOverPay = false;
                        VisValidationPay = true;
                    }
                    else
                    {
                        IsOverPay = true;
                        VisValidationPay = false;
                    }
                }catch { }
                
            });
            CheckDate = new RelayCommand<DatePicker>((p) => {
                return true;
            }, (p) =>
            {
                VisErrorDate = false;
                VisOverDate = false;
                
                if (p.SelectedDate < Repair.RepairDate)
                {
                    VisErrorDate = true;
                } 
                else if (p.SelectedDate > DateTime.Now.Date)
                {
                    VisOverDate = true;
                }
            });
            PrintCommand = new RelayCommand<PayWindow>((p) => {
                if (VisPay == true) return false;
                return true;
            }, (p) =>
            {
                BillTemplate billTemplate = new BillTemplate(Reception);
                PrintViewModel printViewModel = new PrintViewModel();
                billTemplate.Show();
                billTemplate.Close();
                printViewModel.PrintBill(billTemplate);

                //for (int i = 10; i < ListRepair.Count(); i = i + 30)
                //{
                //    ObservableCollection<ListRepair> newlist = new ObservableCollection<ListRepair>();
                //    for (int j = i ; j < i + 30 ; j++)
                //    {
                //        newlist.Add(ListRepair[j]);
                //    }
                //    SubBillTemplate subBillTemplate = new SubBillTemplate(newlist);
                //    PrintViewModel subPrint = new PrintViewModel();
                //    subPrint.PrintSubBill(subBillTemplate);

                //}
            });
        }
        public void InitData()
        {
            VisErrorDate = false;
            VisOverDate = false;
            IsPay = false;
            VisPay = true;
            IsOverPay = true;
            VisValidationPay = false;
            RolReceivedMoney = false;
            RolEmail = false;
            EnabledReceiptDate = true;
            Repair = DataProvider.Ins.DB.REPAIRs.Where(x => x.IdReception == Reception.Reception_Id).SingleOrDefault();
            var repairDetail = DataProvider.Ins.DB.REPAIR_DETAIL.Where(x => x.IdRepair == Repair.Repair_Id);
            ListRepair = new ObservableCollection<ListRepair>();
            int i = 1;
            foreach (var item in repairDetail)
            {
                ListRepair temp = new ListRepair();
                temp.STT = i++;
                temp.RepairDetail = item;
                ListRepair.Add(temp);
                TotalMoney = TotalMoney + temp.RepairDetail.TotalMoney;
            }
        }
    }
}
