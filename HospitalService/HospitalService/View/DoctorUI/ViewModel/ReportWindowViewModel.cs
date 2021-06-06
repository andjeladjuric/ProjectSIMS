using HospitalService.Model;
using HospitalService.Service;
using HospitalService.View.DoctorUI.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using Syncfusion.Pdf.Grid;
using System.Data;
using HospitalService.View.DoctorUI.Views;

namespace HospitalService.View.DoctorUI.ViewModel
{
    public class ReportWindowViewModel : ViewModelClass
    {
        public ReportWindowView Window { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public string PatientsName { get; set; }
        public string TimePeriod { get; set; }
        private ObservableCollection<Diagnosis> diagnoses;
        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ObservableCollection<Diagnosis> Diagnoses
        {
            get { return diagnoses; }
            set
            {
                diagnoses = value;
                OnPropertyChanged();
            }
        }

        public ReportWindowViewModel(MedicalRecord record, DateTime start, DateTime end, ReportWindowView window)
        {
            this.Window = window;
            this.MedicalRecord = record;
            this.PatientsName = record.Patient.Name + " " + record.Patient.Surname;
            this.TimePeriod = start.ToString("dd.MM.yyyy") + " - " + end.ToString("dd.MM.yyyy");
            List<Diagnosis> foundDiagnoses = new MedicalRecordService().GetForTimePeriod(MedicalRecord.Id, start, end);
            Diagnoses = new ObservableCollection<Diagnosis>();
            foundDiagnoses.ForEach(Diagnoses.Add);
            ApplyCommand = new RelayCommand(Executed_ApplyCommand,
             CanExecute_ApplyCommand);
            CancelCommand = new RelayCommand(Executed_CancelCommand,
             CanExecute_ApplyCommand);
        }

        public bool CanExecute_ApplyCommand(object obj)
        {
                return true;
        }

        public void Executed_CancelCommand(object obj) {
            this.Window.Close();
         }

        public void Executed_ApplyCommand(object obj)
        {
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 22);
            PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            PdfFont font3 = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //Draw the text
            PdfGraphics graphics = page.Graphics;
            String text = "Pregled stanja";
            graphics.DrawString(text, font, PdfBrushes.DarkBlue, new PointF(180, 10)); ;
            graphics.DrawString("Pacijent: " + PatientsName, font2, PdfBrushes.DarkGray, new PointF(20, 50));
            graphics.DrawString("Vremenski period: " + TimePeriod, font2, PdfBrushes.DarkGray, new PointF(20, 75));
            graphics.DrawString("U tabeli su prikazane sve uspostavljene dijagnoze za zadati vremenski period.", font3, PdfBrushes.DarkGray, new PointF(20, 110));

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("DIJAGNOZA");
            dataTable.Columns.Add("SIMPTOMI");
            dataTable.Columns.Add("DATUM");
            //Add rows to the DataTable.
            foreach(Diagnosis diagnosis in diagnoses)
                dataTable.Rows.Add(new object[] { diagnosis.Illness, diagnosis.Symptoms, diagnosis.Datum.ToShortDateString() });
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Set row break.
            pdfGrid.AllowRowBreakAcrossPages = true;
            //Get the rows collection from the PdfGrid
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 140));
            //Save the document.
            String filename = MedicalRecord.Patient.Name + "_" + MedicalRecord.Patient.Surname + "(" + TimePeriod + ")" + ".pdf";
        
            doc.Save(filename);
            //close the document
            doc.Close(true);
            this.Window.Close();
        }
    }
}
