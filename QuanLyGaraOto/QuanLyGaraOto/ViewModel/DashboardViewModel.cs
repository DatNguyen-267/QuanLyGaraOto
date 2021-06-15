﻿using LiveCharts;
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

        public ICommand YearChangedCommand_Revenue { get; set; }
        public ICommand YearChangedCommand_Car { get; set; }

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

        private int _SelectedYear_Revenue { get; set; }
        public int SelectedYear_Revenue
        {
            get => _SelectedYear_Revenue; set
            {
                _SelectedYear_Revenue = value;
                OnPropertyChanged();
            }
        }
        private int _SelecteYear_Car { get; set; }
        public int SelectedYear_Car
        {
            get => _SelecteYear_Car; set
            {
                _SelecteYear_Car = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection SeriesCollection_Revenue { get; set; }
        public List<string> Labels_Revenue { get; set; }
        public Func<double, string> Formatter_Revenue { get; set; }

        public Func<ChartPoint, string> PointLabel_Revenue { get; set; }

        public SeriesCollection SeriesCollection_Car { get; set; }
        public List<string> Labels_Car { get; set; }
        public Func<double, string> Formatter_Car { get; set; }

        public Func<ChartPoint, string> PointLabel_Car { get; set; }

        public DashboardViewModel()
        {
            Labels_Revenue = new List<string>();
            Labels_Car = new List<string>();
            ItemSource_Year = new ObservableCollection<string>();

            LoadDataToChart_Revenue(DateTime.Now.Year);
            LoadDataToChart_Car(DateTime.Now.Year);
            LoadData();

            SelectedYear_Revenue = DateTime.Now.Year - firstYear;

            Formatter_Revenue = value => value.ToString();
            PointLabel_Revenue = ChartPoint =>
                    string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation);

            Formatter_Car = value => value.ToString();
            PointLabel_Car = ChartPoint =>
                    string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation);


            YearChangedCommand_Revenue = new RelayCommand<DashboardWindow>((p) => { return true; }, (p) =>
            {
                LoadDataToChart_Revenue(firstYear + p.cbSelectYear_Revenue.SelectedIndex);
                p.liveChart_Revenue.Series = SeriesCollection_Revenue;
            });
            YearChangedCommand_Car = new RelayCommand<DashboardWindow>((p) => { return true; }, (p) =>
            {
                LoadDataToChart_Car(firstYear + p.cbSelectedYear_Car.SelectedIndex);
            });
        }

        public void LoadData()
        {
            Revenue = 0;
            DateTime time = DateTime.Now;
            info = DataProvider.Ins.DB.GARA_INFO.First();
            CurrentNumber = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.ReceptionDate.Day == time.Day).Count();
            TotalCarInMonth = DataProvider.Ins.DB.RECEPTIONs.Where(x => x.ReceptionDate.Month == time.Month).Count();
            foreach (var receipt in DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Month == time.Month))
            {
                Revenue += (long)receipt.MoneyReceived;
            }
                try
                {
                    ObservableCollection<RECEIPT> receipts = new ObservableCollection<RECEIPT>
                        (DataProvider.Ins.DB.RECEIPTs.OrderBy(x => x.ReceiptDate));


                    ItemSource_Year.Clear();
                    firstYear = receipts.First().ReceiptDate.Date.Year;
                    ItemSource_Year.Add("Năm " + firstYear.ToString());

                    foreach (var receipt in receipts)
                    {
                        if (receipt.ReceiptDate.Date.Year <= firstYear + ItemSource_Year.Count - 1) continue;
                        ItemSource_Year.Add("Năm " + receipt.ReceiptDate.Date.Year);
                    }
                } catch (Exception ex)
                {
                    if (receipt.ReceiptDate.Date.Year <= firstYear + ItemSource_Year.Count - 1) continue;
                    ItemSource_Year.Add("Năm " + receipt.ReceiptDate.Date.Year);
                }
            } catch(Exception)
            {

            }
        }

        public void LoadDataToChart_Revenue(int year)
        {
            ObservableCollection<RECEIPT> receipts = new ObservableCollection<RECEIPT>
                (DataProvider.Ins.DB.RECEIPTs.Where(x => x.ReceiptDate.Year == year).OrderBy(x => x.ReceiptDate));
            Labels_Revenue.Clear();
            ChartValues<long> values = new ChartValues<long>();
            for(int i = 1; i <= 12; i++)
            {
                if (receipts.Count == 0 || i < receipts.First().ReceiptDate.Month)
                {
                    values.Add(0);
                    Labels_Revenue.Add(i.ToString());
                    continue;
                }

                Labels_Revenue.Add(i.ToString());
                long totalMoney = 0;

                while(receipts.Count > 0 && i == receipts.First().ReceiptDate.Month)
                {
                    totalMoney += (long)receipts.First().MoneyReceived;
                    receipts.Remove(receipts.First());
                }

                values.Add(totalMoney);
            }

            SeriesCollection_Revenue = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = values,
                    DataLabels = true
                }
            };
        }

        public void LoadDataToChart_Car(int year)
        {
            ObservableCollection<RECEPTION> receptions = new ObservableCollection<RECEPTION>
                (DataProvider.Ins.DB.RECEPTIONs.Where(x => x.ReceptionDate.Year == year).OrderBy(x => x.ReceptionDate));
            Labels_Car.Clear();
            ChartValues<long> values = new ChartValues<long>();

            for(int i = 1; i <= 12; i++)
            {
                Labels_Car.Add(i.ToString());
                if (receptions.Count == 0 || i < receptions.First().ReceptionDate.Month)
                {
                    values.Add(0);
                    continue;
                }

                values.Add(receptions.Where(x => x.ReceptionDate.Month == i).LongCount());
            }

            SeriesCollection_Car = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Xe",
                    Values = values,
                    DataLabels = true
                }
            };
        }
    }
}
