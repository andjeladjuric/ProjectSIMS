using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalService.Model;
using HospitalService.View.PatientUI.ViewsModel;

namespace HospitalService.View.PatientUI.Pages
{
    /// <summary>
    /// Interaction logic for NotesNotification.xaml
    /// </summary>
    public partial class NotesNotification : Page
    {
        

        private NotesNotificationViewModel viewModel;
        public NotesNotification(Diagnosis diagnosis, Note note)
        {
            InitializeComponent();
            viewModel = new NotesNotificationViewModel(diagnosis,note,this);
            this.DataContext = viewModel;
            
            
            
        }

        
    }
}
