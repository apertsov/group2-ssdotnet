﻿#pragma checksum "D:\DiagnosticCenter\ExternalSite\DCExternalSite\Views\PatientList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F751A2C855778EB44BC4ED2258AC73ED"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.239
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace DCExternalSite.Views {
    
    
    public partial class PatientList : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ScrollViewer PageScrollViewer;
        
        internal System.Windows.Controls.StackPanel ContentStackPanel;
        
        internal System.Windows.Controls.TextBlock HeaderText;
        
        internal System.Windows.Controls.DataGrid dataGrid1;
        
        internal System.Windows.Controls.DataGridTextColumn startTimeColumn;
        
        internal System.Windows.Controls.DataGridTextColumn ExaminationTypeName;
        
        internal System.Windows.Controls.DataGridTextColumn conclusionColumn;
        
        internal System.Windows.Controls.DataGridTextColumn ExaminationTypePrice;
        
        internal System.Windows.Controls.DataForm dataForm1;
        
        internal System.Windows.Controls.Image imagePrint;
        
        internal System.Windows.Controls.Grid grid1;
        
        internal System.Windows.Controls.TextBlock FirstNameTextBlock;
        
        internal System.Windows.Controls.TextBlock SurnameTextBlock;
        
        internal System.Windows.Controls.TextBlock BirthDateTextBlock;
        
        internal System.Windows.Controls.DomainDataSource patientDomainDataSource;
        
        internal System.Windows.Controls.DomainDataSource examinationDataSource;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/DCExternalSite;component/Views/PatientList.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PageScrollViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("PageScrollViewer")));
            this.ContentStackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentStackPanel")));
            this.HeaderText = ((System.Windows.Controls.TextBlock)(this.FindName("HeaderText")));
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(this.FindName("dataGrid1")));
            this.startTimeColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("startTimeColumn")));
            this.ExaminationTypeName = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("ExaminationTypeName")));
            this.conclusionColumn = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("conclusionColumn")));
            this.ExaminationTypePrice = ((System.Windows.Controls.DataGridTextColumn)(this.FindName("ExaminationTypePrice")));
            this.dataForm1 = ((System.Windows.Controls.DataForm)(this.FindName("dataForm1")));
            this.imagePrint = ((System.Windows.Controls.Image)(this.FindName("imagePrint")));
            this.grid1 = ((System.Windows.Controls.Grid)(this.FindName("grid1")));
            this.FirstNameTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("FirstNameTextBlock")));
            this.SurnameTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("SurnameTextBlock")));
            this.BirthDateTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("BirthDateTextBlock")));
            this.patientDomainDataSource = ((System.Windows.Controls.DomainDataSource)(this.FindName("patientDomainDataSource")));
            this.examinationDataSource = ((System.Windows.Controls.DomainDataSource)(this.FindName("examinationDataSource")));
        }
    }
}
