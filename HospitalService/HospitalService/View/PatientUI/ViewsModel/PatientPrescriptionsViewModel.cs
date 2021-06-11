using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows;
using HospitalService.Model;
using HospitalService.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;

namespace HospitalService.View.PatientUI.ViewsModel
{
   public class PatientPrescriptionsViewModel: ValidationBase
    {

        private ObservableCollection<Prescription> prescriptions;
        public ObservableCollection<Prescription> Prescriptions
        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged();
            }
        }
        private Prescription selectedPrescription;
        public Prescription SelectedPrescription
        {
            get { return selectedPrescription; }
            set
            {
                selectedPrescription = value;
                OnPropertyChanged();
            }
        }
        private MedicalRecordService medicalRecordService;

        public RelayCommand setReminder { get; set; }
        public RelayCommand saveReport { get; set; }
        private Patient patient;
        private Timer medicationReminder;
        List<string> data = new List<string>();
        PdfPTable table = new PdfPTable(25);

        private String notification = "";

        private void Execute_SaveReport(object obj) {

            Validate();
            if (IsValid)
            {


                FileStream fs = new FileStream("izvjestaj.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4.Rotate());
                Document doc = new Document(rec);
                PdfWriter pdfwriter = PdfWriter.GetInstance(doc, fs);
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font titleFont = new Font(bfTimes, 16);
                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Izvjestaj o uzimanju terapije", titleFont);
                title.Alignment = Element.ALIGN_CENTER;

                iTextSharp.text.Paragraph blanc = new iTextSharp.text.Paragraph("    ", titleFont);

                doc.Open();
                doc.Add(title);
                doc.Add(blanc);
                getUsageData();
                fillTable();
                doc.Add(table);
                doc.Close();
                MessageBox.Show("PDF je kreiran.");

            }

        }
        public void fillTable()
        {

            PdfPHeaderCell h0 = new PdfPHeaderCell();
            PdfPHeaderCell h1 = new PdfPHeaderCell();
            PdfPHeaderCell h2 = new PdfPHeaderCell();
            PdfPHeaderCell h3 = new PdfPHeaderCell();
            PdfPHeaderCell h4 = new PdfPHeaderCell();
            PdfPHeaderCell h5 = new PdfPHeaderCell();
            PdfPHeaderCell h6 = new PdfPHeaderCell();
            PdfPHeaderCell h7 = new PdfPHeaderCell();
            PdfPHeaderCell h8 = new PdfPHeaderCell();
            PdfPHeaderCell h9 = new PdfPHeaderCell();
            PdfPHeaderCell h10 = new PdfPHeaderCell();
            PdfPHeaderCell h11 = new PdfPHeaderCell();
            PdfPHeaderCell h12 = new PdfPHeaderCell();
            PdfPHeaderCell h13 = new PdfPHeaderCell();
            PdfPHeaderCell h14 = new PdfPHeaderCell();
            PdfPHeaderCell h15 = new PdfPHeaderCell();
            PdfPHeaderCell h16 = new PdfPHeaderCell();
            PdfPHeaderCell h17 = new PdfPHeaderCell();
            PdfPHeaderCell h18 = new PdfPHeaderCell();
            PdfPHeaderCell h19 = new PdfPHeaderCell();
            PdfPHeaderCell h20 = new PdfPHeaderCell();
            PdfPHeaderCell h21 = new PdfPHeaderCell();
            PdfPHeaderCell h22 = new PdfPHeaderCell();
            PdfPHeaderCell h23 = new PdfPHeaderCell();
            PdfPHeaderCell h24 = new PdfPHeaderCell();
            h0.Phrase = new Phrase(" ");
            h1.Phrase = new Phrase("1h");
            h2.Phrase = new Phrase("2h");
            h3.Phrase = new Phrase("3h");
            h4.Phrase = new Phrase("4h");
            h5.Phrase = new Phrase("5h");
            h6.Phrase = new Phrase("6h");
            h7.Phrase = new Phrase("7h");
            h8.Phrase = new Phrase("8h");
            h9.Phrase = new Phrase("9h");
            h10.Phrase = new Phrase("10h");
            h11.Phrase = new Phrase("11h");
            h12.Phrase = new Phrase("12h");
            h13.Phrase = new Phrase("13h");
            h14.Phrase = new Phrase("14h");
            h15.Phrase = new Phrase("15h");
            h16.Phrase = new Phrase("16h");
            h17.Phrase = new Phrase("17h");
            h18.Phrase = new Phrase("18h");
            h19.Phrase = new Phrase("19h");
            h20.Phrase = new Phrase("20h");
            h21.Phrase = new Phrase("21h");
            h22.Phrase = new Phrase("22h");
            h23.Phrase = new Phrase("23h");
            h24.Phrase = new Phrase("24h");


            table.AddCell(h0);
            table.AddCell(h1);
            table.AddCell(h2);
            table.AddCell(h3);
            table.AddCell(h4);
            table.AddCell(h5);
            table.AddCell(h6);
            table.AddCell(h7);
            table.AddCell(h8);
            table.AddCell(h9);
            table.AddCell(h10);
            table.AddCell(h11);
            table.AddCell(h12);
            table.AddCell(h13);
            table.AddCell(h14);
            table.AddCell(h15);
            table.AddCell(h16);
            table.AddCell(h17);
            table.AddCell(h18);
            table.AddCell(h19);
            table.AddCell(h20);
            table.AddCell(h21);
            table.AddCell(h22);
            table.AddCell(h23);
            table.AddCell(h24);

            List<String> days = new List<String>();
            days.Add("Ponedeljak");
            days.Add("Utorak");
            days.Add("Srijeda");
            days.Add("Cetvrtak");
            days.Add("Petak");
            days.Add("Subota");
            days.Add("Nedelja");

            int n = 24 / SelectedPrescription.HowOften;
            
            for (int i = 0; i < 7; i++) {
                table.AddCell(new PdfPCell(new Phrase(days[i])));
                /*table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase("")));
                table.AddCell(new PdfPCell(new Phrase(SelectedPrescription.Medication)));*/
                for (int k = 0; k < n; k++) {
                    for (int j = 0; j < SelectedPrescription.HowOften-1; j++) {
                        table.AddCell(new PdfPCell(new Phrase("")));
                    }
                    table.AddCell(new PdfPCell(new Phrase(SelectedPrescription.Medication)));

                }
                
                
            }


            
           

        }

        public void getUsageData()
        {
            int num = 24 / SelectedPrescription.HowOften;
            for (int i = 0; i < num; i++) {
                data.Add(SelectedPrescription.Medication);
            }
            
        }
        private void Execute_SetReminder(object obj) {

            Validate();

            if (IsValid)
            {


                DateTime prescriptionStartDate = SelectedPrescription.Start;
                medicationReminder = new System.Timers.Timer(15000); // in milliseconds - p.HowOften*3600000
                notification += "Uzmite lijek" + " " + SelectedPrescription.Medication.ToUpper() + "\n" + "Dodatne informacije: " + SelectedPrescription.AdditionalInfos;
                DateTime prescriptionExpiryDate = prescriptionStartDate.AddDays(SelectedPrescription.HowLong);
                medicationReminder.Elapsed += (sender, e) => showNotification(sender, e, notification, prescriptionExpiryDate);
                medicationReminder.Start();



            }
        }
        private bool CanExecute_Command(object obj) {
            return true;
        }

        static void showNotification(object sender, ElapsedEventArgs e, string theString, DateTime et)
        {
            if (DateTime.Compare(DateTime.Now, et) < 0)
            {
                MessageBox.Show(theString);

            }
        }

        protected override void ValidateSelf()
        {
            if (SelectedPrescription == null)
            {
                this.ValidationErrors["Start"] = "Odaberite jedan recept.";
            }
            else if (SelectedPrescription.Start.AddDays(SelectedPrescription.HowLong) <= DateTime.Now)
            {
                this.ValidationErrors["Start"] = "Recept je istekao.";
            }
        }

        public PatientPrescriptionsViewModel(Patient patient) {
            this.patient = patient;
            medicalRecordService = new MedicalRecordService();
            List<Prescription> patientPrescriptions = medicalRecordService.GetPrescriptions(patient);
            this.Prescriptions = new ObservableCollection<Prescription>();
            patientPrescriptions.ForEach(this.Prescriptions.Add);
            setReminder = new RelayCommand(Execute_SetReminder, CanExecute_Command);
            saveReport = new RelayCommand(Execute_SaveReport,CanExecute_Command);

        }
    }
}
