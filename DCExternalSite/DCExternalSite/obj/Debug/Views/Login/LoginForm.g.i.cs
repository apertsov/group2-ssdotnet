﻿#pragma checksum "D:\.net\.netProject\DCExternalSite\DCExternalSite\Views\Login\LoginForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3400FFFE5DD69979857A43973DB56026"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DCExternalSite.Controls;
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


namespace DCExternalSite.LoginUI {
    
    
    public partial class LoginForm : System.Windows.Controls.StackPanel {
        
        internal DCExternalSite.Controls.BusyIndicator busyIndicator;
        
        internal DCExternalSite.Controls.CustomDataForm loginForm;
        
        internal System.Windows.Controls.Button loginButton;
        
        internal System.Windows.Controls.Button loginCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DCExternalSite;component/Views/Login/LoginForm.xaml", System.UriKind.Relative));
            this.busyIndicator = ((DCExternalSite.Controls.BusyIndicator)(this.FindName("busyIndicator")));
            this.loginForm = ((DCExternalSite.Controls.CustomDataForm)(this.FindName("loginForm")));
            this.loginButton = ((System.Windows.Controls.Button)(this.FindName("loginButton")));
            this.loginCancel = ((System.Windows.Controls.Button)(this.FindName("loginCancel")));
        }
    }
}

