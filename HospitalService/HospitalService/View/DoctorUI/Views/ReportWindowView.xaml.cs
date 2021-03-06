using HospitalService.View.DoctorUI.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using Syncfusion.Pdf.Tables;
using System.Data;
using Syncfusion.Pdf.Grid;
using System.Collections.ObjectModel;
using HospitalService.Model;

namespace HospitalService.View.DoctorUI.Views
{
    /// <summary>
    /// Interaction logic for ReportWindowView.xaml
    /// </summary>
    public partial class ReportWindowView : Window
    {
        public ReportWindowView(MedicalRecord record, DateTime start, DateTime end)
        {
            InitializeComponent();
            this.DataContext = new ReportWindowViewModel(record, start, end, this);
        }

       
    }
}
