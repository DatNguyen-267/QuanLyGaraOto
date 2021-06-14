using LiveCharts;
using LiveCharts.Wpf;
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
    public class DashboardViewModel : BaseViewModel
    {
        private long _Revenue { get; set; }
        public long Revenue
        {
            get => _Revenue; set
            {
                _Revenue = value;
                OnPropertyChanged();
            }
        }
        private int _TotalCarInMonth { get; set; }
        public int TotalCarInMonth
        {
            get => _TotalCarInMonth; set
            {
                _TotalCarInMonth = value;
                OnPropertyChanged();
            }
        }
        private int _CurrentNumber { get; set; }
        public int CurrentNumber
        {
            get => _CurrentNumber; set
            {
                _CurrentNumber = value;
                OnPropertyChanged();
            }
        }
        private GARA_INFO _info { get; set; }
        public GARA_INFO info 
        {
            get => _info; set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        public ICommand YearChangedCommand { get; set; }

        private ObservableCollection<string> _ItemSource_Year { get; set; }
        public ObservableCollection<string> ItemSource_Year
        {
            get => _ItemSource_Year; set
            {
                _ItemSource_Year = value;
                OnPropertyChanged();
            }
        }


        private int firstYear;

        private int _SelectedYear { get; set; }
        public int SelectedYear
        {
            get => _SelectedYear; set
            {
                _SelectedYear = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }


        public DashboardViewModel()
        {
            Labels = new List<string>();
            ItemSource_Year = new ObservableCollection<string>();
            LoadDataToChart(DateTime.Now.Year);
            LoadData();

            SelectedYear = DateTime.Now.Year - firstYear;

            Formatter = value => value.ToString();
            PointLabel = ChartPoint =>
                    string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation);

            
            YearChangedCommand = new RelayCommand<DashboardWindow>((p) => { return true; }, (p) =>
            {
                LoadDataToChart(firstYear + p.cbSelectYear.SelectedIndex);
                p.liveCharts.Series = SeriesCollection;
            });
        }

        public void LoadData()
        {
            Revenue = 0;
            DateTime time = DateTime.Now;
            info = DataProvider.Ins.DB.GARA_INFO.First();
            CurrentNumber = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.ReceptionDate.Day == time.Day).Count();
            TotalCarInMonth = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.ReceptionDate.Month == time.Month).Count();
            foreach(var receipt in DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Month == time.Month))
            {
                Revenue += (long) receipt.MoneyReceived;
            }

            ObservableCollection<RECEIPT> receipts = new ObservableCollection<RECEIPT>
                (DataProvider.Ins.DB.RECEIPTs.OrderBy(x => x.ReceiptDate));
            
            ItemSource_Year.Clear();
            firstYear = receipts.First().ReceiptDate.Date.Year;
            ItemSource_Year.Add("Năm " + firstYear.ToString());

            foreach(var receipt in receipts)
            {
                if (receipt.ReceiptDate.Date.Year <= firstYear + ItemSource_Year.Count - 1) continue;
                ItemSource_Year.Add("Năm " + receipt.ReceiptDate.Date.Year);
            }
        }

        public void LoadDataToChart(int year)
        {
            ObservableCollection<RECEIPT> receipts = new ObservableCollection<RECEIPT>
                (DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Year == year).OrderBy(x => x.ReceiptDate));
            Labels.Clear();
            ChartValues<long> values = new ChartValues<long>();
            for(int i = 1; i <= 12; i++)
            {
                if (receipts.Count == 0) break;
                if (i < receipts.First().ReceiptDate.Month) continue;
                Labels.Add(i.ToString());
                long totalMoney = 0;

                while(receipts.Count > 0 && i == receipts.First().ReceiptDate.Month)
                {
                    totalMoney += (long)receipts.First().MoneyReceived;
                    receipts.Remove(receipts.First());
                }

                values.Add(totalMoney);
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = values
                }
            };
        }
    }
}
