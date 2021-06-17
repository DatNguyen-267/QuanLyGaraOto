﻿using QuanLyGaraOto.Model;
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
    /// Interaction logic for ModifyWageWindow.xaml
    /// </summary>
    public partial class ModifyWageWindow : Window
    {
        public ModifyWageViewModel ViewModel { get; set; }
        public ModifyWageWindow(WAGE wage)
        {
            InitializeComponent();
            DataContext = ViewModel = new ModifyWageViewModel(wage);
        }
    }
}
