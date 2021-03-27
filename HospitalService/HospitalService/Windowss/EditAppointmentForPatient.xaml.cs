﻿using System;
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
using Model;

namespace HospitalService.Windowss
{
    /// <summary>
    /// Interaction logic for EditAppointmentForPatient.xaml
    /// </summary>
    public partial class EditAppointmentForPatient : Window
    {

        public Appointment a { get; set; }
        public AppointmentStorage baza { get; set; }
        public DataGrid Tabela { get; set; }
        public EditAppointmentForPatient(Appointment ap, AppointmentStorage aps, DataGrid dg)
        {
            InitializeComponent();
            a = ap;
            baza = aps;
            Tabela = dg;
            editGrid.DataContext = this;
            idTB.Text = a.Id;
            startTB.Text = a.StartTime.ToString();
            endTB.Text = a.EndTime.ToString();

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                a.StartTime = Convert.ToDateTime(startTB.Text);
                a.EndTime = Convert.ToDateTime(endTB.Text);
            }
            catch (Exception) { }
            a.room.Id = comboBox.Text;
            baza.Edit(a.Id, a.StartTime, a.EndTime, a.room);
            Tabela.Items.Refresh();

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
