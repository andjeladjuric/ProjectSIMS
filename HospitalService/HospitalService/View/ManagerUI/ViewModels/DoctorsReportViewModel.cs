using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class DoctorsReportViewModel : ViewModel
    {
        #region Fields
        public Doctor SelectedDoctor { get; set; }
        public Frame Frame { get; set; }
        private string doctor;
        public string DoctorsName
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }
        private string patientsName;
        public string PatientsName
        {
            get { return patientsName; }
            set
            {
                patientsName = value;
                OnPropertyChanged();
            }
        }

        private DateTime start;
        public DateTime StartDate
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        private DateTime end;
        public DateTime EndDate
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Appointment> appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand SelectionChanged { get; set; }
        public MyICommand GenerateReport { get; set; }
        public MyICommand CancelCommand { get; set; }
        #endregion

        #region Actions
        private void OnDateChanged()
        {
            AppointmentsService service = new AppointmentsService();
            Appointments = new ObservableCollection<Appointment>();

            foreach (Appointment a in service.GetAll())
            {
                if (a.doctor.Jmbg.Equals(SelectedDoctor.Jmbg) && a.StartTime >= StartDate && a.EndTime <= EndDate)
                {
                    a.patient.FullName = a.patient.Name + " " + a.patient.Surname;
                    Appointments.Add(a);
                }
            }
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new DoctorsView());
        }

        private void OnConfirm()
        {
            PdfPTable table = new PdfPTable(6);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 30, 30, 50, 35);
            //doc.SetMargins(30,30,50,50);

            var outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                @"D:\FAKS\SIMS\ProjectSIMS\HospitalService\HospitalService\View\ManagerUI", "Report.pdf");

            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(outputFile,
                System.IO.FileMode.Create));
            doc.Open();

            string text = "Izveštaj o planu rada lekara: " + SelectedDoctor.Name + " " + SelectedDoctor.Surname;
            Paragraph paragraph = new Paragraph();
            paragraph.SpacingBefore = 10;
            paragraph.SpacingAfter = 20;
            paragraph.Alignment = Element.ALIGN_CENTER;
            paragraph.Font = FontFactory.GetFont(FontFactory.HELVETICA, 22f, BaseColor.BLACK);
            paragraph.Add(text);
            doc.Add(paragraph);

            string period = "Izabrani period: " + StartDate.ToShortDateString() + " - " + EndDate.ToShortDateString();
            Paragraph secondLine = new Paragraph();
            secondLine.SpacingBefore = 20;
            secondLine.SpacingAfter = 5;
            secondLine.Alignment = Element.ALIGN_JUSTIFIED;
            secondLine.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14f, Font.ITALIC, BaseColor.DARK_GRAY);
            secondLine.Add(period);
            doc.Add(secondLine);

            string exp = "U tabeli su prikazani svi termini za zadati vremenski period.";
            Paragraph thirdLine = new Paragraph();
            thirdLine.SpacingBefore = 5;
            thirdLine.SpacingAfter = 10;
            thirdLine.Alignment = Element.ALIGN_JUSTIFIED;
            thirdLine.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14f, Font.ITALIC, BaseColor.DARK_GRAY);
            thirdLine.Add(exp);
            doc.Add(thirdLine);

            table.AddCell("DATUM");
            table.AddCell("POCETAK TERMINA");
            table.AddCell("KRAJ TERMINA");
            table.AddCell("PACIJENT");
            table.AddCell("PROSTORIJA");
            table.AddCell("VRSTA TERMINA");

            if (Appointments != null)
            {
                foreach (var a in Appointments)
                {
                    a.patient.FullName = a.patient.Name + " " + a.patient.Surname;
                    table.AddCell(a.StartTime.ToShortDateString());
                    table.AddCell(a.StartTime.ToShortTimeString());
                    table.AddCell(a.EndTime.ToShortTimeString());
                    table.AddCell(a.patient.FullName);
                    table.AddCell(a.room.Id);
                    table.AddCell(a.Type.ToString());
                }

                doc.Add(table);
            }

            string potpis = "  Izveštaj izdaje: " ;
            Paragraph p = new Paragraph();
            p.SpacingBefore = 40;
            p.SpacingAfter = 2;
            p.Alignment = Element.ALIGN_RIGHT;
            p.Font = FontFactory.GetFont(FontFactory.HELVETICA, 14f, BaseColor.DARK_GRAY);
            p.Add(potpis);
            doc.Add(p);

            string ime = "Andjela Djuric";
            Paragraph imep = new Paragraph();
            imep.SpacingBefore = 2;
            imep.SpacingAfter = 10;
            imep.Alignment = Element.ALIGN_RIGHT;
            imep.Font = FontFactory.GetFont(FontFactory.TIMES_BOLDITALIC, 11f, Font.ITALIC, BaseColor.DARK_GRAY);
            imep.Add(ime);
            doc.Add(imep);
            doc.Close();
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Constructors
        public DoctorsReportViewModel(Doctor selected, Frame currentFrame)
        {
            this.Frame = currentFrame;
            this.SelectedDoctor = selected;
            this.DoctorsName = "Plan rada, Dr " + SelectedDoctor.Name + " " + SelectedDoctor.Surname;
            this.StartDate = DateTime.Today;
            this.EndDate = DateTime.Today;

            SelectionChanged = new MyICommand(OnDateChanged, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            GenerateReport = new MyICommand(OnConfirm, CanExecute);
        }
        #endregion
    }
}
