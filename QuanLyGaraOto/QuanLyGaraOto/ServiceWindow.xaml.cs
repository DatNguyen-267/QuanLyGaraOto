﻿using QuanLyGaraOto.ViewModel;
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
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : UserControl
    {
        public ServiceViewModel serviceViewModel { get; set; }
        public ServiceWindow()
        {
            InitializeComponent();
            this.DataContext = (serviceViewModel = new ServiceViewModel());
        }
    }
}
