using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : UserControl
    {
        public SettingViewModel SettingViewModel { get; set; }
        public SettingWindow()
        {
            InitializeComponent();
            this.DataContext = (SettingViewModel = new SettingViewModel());
        }

        private void AppSetting_Click(object sender, RoutedEventArgs e)
        {
            AppSetting.Background = Brushes.Black;
            AppSetting.Foreground = Brushes.White;

            UserSetting.Background = Brushes.White;
            UserSetting.Foreground = Brushes.Black;
        }
        private void UserSetting_Click(object sender, RoutedEventArgs e)
        {
            AppSetting.Background = Brushes.White;
            AppSetting.Foreground = Brushes.Black;

            UserSetting.Background = Brushes.Black;
            UserSetting.Foreground = Brushes.White;
        }
    }
}
