﻿#pragma checksum "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "24193CC1B89277CBA598BE443250342ABD553EE5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HospitalService.Windowss;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
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


namespace HospitalService.Windowss {
    
    
    /// <summary>
    /// AddAppointmentToDoctor
    /// </summary>
    public partial class AddAppointmentToDoctor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IdBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StartBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EndBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PatientBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoomBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AppointmentTypeBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HospitalService;component/windowss/addappointmenttodoctor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.IdBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.StartBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.EndBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PatientBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.RoomBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.AppointmentTypeBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.DateBox = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            
            #line 27 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 32 "..\..\..\..\Windowss\AddAppointmentToDoctor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

