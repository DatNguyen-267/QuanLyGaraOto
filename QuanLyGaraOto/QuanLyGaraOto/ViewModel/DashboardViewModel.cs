using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public Func<ChartPoint, string> PointLabel { get; set; }


        public DashboardViewModel()
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2020",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "2021",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Maria", "Susan", "Charles", "Florida" };
            Formatter = value => value.ToString();


            PointLabel = ChartPoint =>
                    string.Format("{0} ({1:P})", ChartPoint.Y, ChartPoint.Participation);
        }
    }
}
