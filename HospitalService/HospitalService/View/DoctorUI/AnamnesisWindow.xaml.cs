using HospitalService.Model;
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

namespace HospitalService.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for AnamnesisWindow.xaml
    /// </summary>
    public partial class AnamnesisWindow : Window
    {
        public HistoryWindow window { get; set; }
        public Diagnosis diagnosis { get; set; }

        public AnamnesisWindow(Diagnosis d, HistoryWindow hw)
        {
            InitializeComponent();
            window = hw;
            diagnosis = d;
            AnamnesisTB.Text = d.Anamnesis;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditAnamnesisWindow editAnamnesisWindow = new EditAnamnesisWindow(this);
            this.Close();
            editAnamnesisWindow.ShowDialog();
        }

        public void refresh()
        {
            AnamnesisTB.Text = diagnosis.Anamnesis;
        }
    }
}

