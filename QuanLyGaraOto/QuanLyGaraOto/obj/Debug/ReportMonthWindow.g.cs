﻿#pragma checksum "..\..\ReportMonthWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BC8F427C5745FC3AD61F98C2CACC8624DF24EB519048D6112B5A8F170E74EE50"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using QuanLyGaraOto;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace QuanLyGaraOto {
    
    
    /// <summary>
    /// ReportMonthWindow
    /// </summary>
    public partial class ReportMonthWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal QuanLyGaraOto.ReportMonthWindow wdReportMonth;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Rpcb_SelectYear;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Rpcb_SelectMonth;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvs_Report_Inventory;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvs_Report_Sales;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Print;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_ViewReport;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\ReportMonthWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Report;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/QuanLyGaraOto;component/reportmonthwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReportMonthWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.wdReportMonth = ((QuanLyGaraOto.ReportMonthWindow)(target));
            return;
            case 2:
            this.Rpcb_SelectYear = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.Rpcb_SelectMonth = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.lvs_Report_Inventory = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.lvs_Report_Sales = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.bt_Print = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.bt_ViewReport = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.bt_Report = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

