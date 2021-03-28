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

namespace HospitalService.Windowss
{
    /// <summary>
    /// Interaction logic for EditAppointmentForDoctor.xaml
    /// </summary>
    public partial class EditAppointmentForDoctor : Window
    {
        public Appointment a { get; set; }
        public AppointmentStorage baza { get; set; }
        public DataGrid Tabela { get; set; }

        public EditAppointmentForDoctor(Appointment ap, AppointmentStorage aps, DataGrid dg, RoomFileStorage sobe)
        {
            InitializeComponent();
            a = ap;
            baza = aps;
            Tabela = dg;
            editGrid.DataContext = this;
            IdTB.Text = a.Id;
            startTB.Text = a.StartTime.ToString();
            endTB.Text = a.EndTime.ToString();
            List<Room> r = sobe.GetAll();
            List<String> ids = new List<String>();
            Room soba;
            for (int i = 0; i < r.Count; i++)
            {
                soba = r[i];
                ids.Add(soba.Id);
            }
            comboBox.ItemsSource = ids;
            comboBox.SelectedItem = a.room.Id;

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

