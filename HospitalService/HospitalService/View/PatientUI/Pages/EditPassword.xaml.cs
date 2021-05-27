using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.View.PatientUI.ViewsModel;
using Model;
using Newtonsoft.Json;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for EditPassword.xaml
    /// </summary>
    public partial class EditPassword : Page
    {
      

        private EditPasswordViewModel viewModel;
        public EditPassword(Patient patient, EditProfile editPassword)
        {
            InitializeComponent();
            viewModel = new EditPasswordViewModel(patient, oldPasswordPb, newPasswordPB, confirmPb,editPassword);
            this.DataContext = viewModel;
           
        }

      
    }
}
