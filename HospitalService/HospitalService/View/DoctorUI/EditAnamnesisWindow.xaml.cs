using HospitalService.Model;
using Model;
using Storage;
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
    /// Interaction logic for EditAnamnesisWindow.xaml
    /// </summary>
    public partial class EditAnamnesisWindow : Window
    {
        public AnamnesisWindow anamnesisWindow { get; set; }

        public EditAnamnesisWindow(AnamnesisWindow aw)
        {
            InitializeComponent();
            anamnesisWindow = aw;
            EditAnamnesisTB.Text = anamnesisWindow.diagnosis.Anamnesis;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage baza = new MedicalRecordStorage();
            string newAnamnesis = EditAnamnesisTB.Text;
            Diagnosis diagnosis = anamnesisWindow.diagnosis;
            diagnosis.Anamnesis = newAnamnesis;
            MedicalRecord mr = anamnesisWindow.window.Karton.MedicalRecord;
            mr.editDignosis(diagnosis);
            baza.Edit(mr);
            anamnesisWindow.refresh();
            this.Close();
        }
    }
}
