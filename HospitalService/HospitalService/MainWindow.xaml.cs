using HospitalService.Storage;
using HospitalService.View.DoctorUI;
using HospitalService.View.ManagerUI;
using HospitalService.View.PatientUI;
using HospitalService.View.SecretaryUI;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace HospitalService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private String FileLocation2 = @"..\..\..\Data\users.json";
        private PatientStorage patientBase = new PatientStorage();
        private DoctorStorage doctorBase = new DoctorStorage();
        private SecretaryStorage secretaryBase = new SecretaryStorage();
        private ManagerStorage managerBase = new ManagerStorage();
        InventoryFileStorage invStorage = new InventoryFileStorage();
        List<Account> users = new List<Account>();
       

        public MainWindow()
        {
            InitializeComponent();
            users = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(FileLocation2));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account ac = new Account(usernameText.Text, passwordBox.Password, UserType.Doctor);
                Boolean pom = false;
                foreach (Account user in users) {
                    if (user.Username.Equals(ac.Username) && user.Password.Equals(ac.Password))
                    {
                        ac.User = user.User;
                        pom = true;
                        if (ac.User == UserType.Patient)
                        {
                            Patient p = patientBase.GetOneByUsername(ac.Username);
                            new PatientWindow(p).Show();
                            this.Close();
                            break;
                        }
                        else if (ac.User == UserType.Doctor)
                        {
                            Doctor d = doctorBase.GetOne(ac.Username);
                            new DoctorWindow(d).Show();
                            this.Close();
                            break;
                        }
                        else if (ac.User == UserType.Manager)
                        {
                            Manager m = managerBase.GetOne(ac.Username);
                            new ManagerWindow(m).Show();
                            this.Close();
                            break;

                        }
                        else if (ac.User == UserType.Secretary)
                        {
                            Secretary s = secretaryBase.GetOne(ac.Username);
                            new SecretaryWindow(s).Show();
                            this.Close();
                            break;

                        }
                    }
                }
                if(pom ==  false)
                    MessageBox.Show("Neuspjesno logovanje");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Nije registrovan ");
            }

        }
    }
}
