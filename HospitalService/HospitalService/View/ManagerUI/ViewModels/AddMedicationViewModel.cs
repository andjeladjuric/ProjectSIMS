using HospitalService.Repositories;
using HospitalService.Service;
using HospitalService.View.ManagerUI.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class AddMedicationViewModel : ViewModel
    {
        #region Fields
        private string medicationId;
        private string medicationName;
        private string medicationFormat;
        private MedicationType type;
        private MedicineStatus status;
        private string doctor;
        private bool demoOn;
        private bool isOpen;
        private CancellationTokenSource cts = new CancellationTokenSource();
        #endregion

        #region Properties
        private bool warning;
        public bool Warning
        {
            get { return warning; }
            set
            {
                warning = value;
                OnPropertyChanged();
            }
        }
        public bool IsPopupOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }
        public bool DemoOn
        {
            get { return demoOn; }
            set
            {
                demoOn = value;
                OnPropertyChanged();
            }
        }
        public CollectionView CurrentIngredients { get; set; }
        private ObservableCollection<string> ingView;
        public ObservableCollection<string> IngredientsView { get { return ingView; } set { ingView = value; OnPropertyChanged(); } }
        public string MedicationId
        {
            get { return medicationId; }
            set
            {
                medicationId = value;
                OnPropertyChanged();
            }
        }
        public string MedicationName
        {
            get { return medicationName; }
            set
            {
                medicationName = value;
                OnPropertyChanged();
            }
        }
        public string MedicationFormat
        {
            get { return medicationFormat; }
            set
            {
                medicationFormat = value;
                OnPropertyChanged();
            }
        }

        public MedicationType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public MedicineStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        
        public string Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged(); }
        }

        public List<String> Doctors { get; set; }
        public Frame Frame { get; set; }

        public Dictionary<string, int> Ingredients { get; set; }
        #endregion

        #region Commands
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand AddIngredientCommand {get; set;}
        public MyICommand CancelCommand { get; set; }
        public MyICommand StopDemo { get; set; }
        #endregion

        #region Actions
        private void OnStop()
        {
            cts.Cancel();
            Warning = true;
            DemoOn = false;
            this.Frame.NavigationService.Navigate(new RoomsView());
        }
        private void OnConfirm()
        {
            MedicationService medicationService = new MedicationService();
            string[] generateId = Guid.NewGuid().ToString().Split("-");
            MedicationId = generateId[0];
            medicationService.AddMedication(new Medication(MedicationId, MedicationName, MedicineStatus.WaitingForApproval, Type, Ingredients, MedicationFormat));
            CreateValidationRequest();
            this.Frame.NavigationService.Navigate(new MedicationsView());
        }

        private void OnCancel()
        {
            this.Frame.NavigationService.Navigate(new MedicationsView());
        }

        private void OnAddIngredients()
        {
            this.Frame.NavigationService.Navigate(new IngredientsView(Ingredients, Frame, IngredientsView, false));
        }

        private bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Other Functions
        private void LoadDoctors()
        {
            Doctors = new List<string>();
            DoctorsRepository repository = new DoctorsRepository();
            foreach (Doctor d in repository.GetAll())
            {
                Doctors.Add(d.Name + " " + d.Surname);
            }
        }

        private void CreateValidationRequest()
        {
            string[] selectedDoctor = Doctor.ToString().Split(" ");
            string docName = selectedDoctor[0];
            string docLastName = selectedDoctor[1];
            string docJmbg = "";

            DoctorsRepository repository = new DoctorsRepository();
            foreach (Doctor d in repository.GetAll())
            {
                if (d.Name.Equals(docName) && d.Surname.Equals(docLastName))
                {
                    docJmbg = d.Jmbg;
                }
            }

            MedicineValidationRequest validationRequest = new MedicineValidationRequest(MedicationId, docJmbg);
            MedicineValidationsRepository validations = new MedicineValidationsRepository();
            validations.GetAll().Add(validationRequest);
            validations.SerializeValidationRequests();
        }

        private async Task DemoIsOn(CancellationToken ct)
        {
            if (DemoOn)
            {
                MedicationService service = new MedicationService();
                DoctorService doctors = new DoctorService();
                MessageViewModel.Message = "Završena četvrta funkcionalnost \n Sledi - dodavanje lekova";
                ct.ThrowIfCancellationRequested();

                await Task.Delay(1500, ct);
                MedicationName = "Roakutan";
                await Task.Delay(2000, ct);
                MedicationFormat = "Tableta";
                await Task.Delay(2000, ct);
                Type = MedicationType.Painkiller;
                await Task.Delay(2000, ct);
                Doctor = "Petra Jovic";
                await Task.Delay(2000, ct);
                this.Frame.NavigationService.Navigate(new IngredientsView(Ingredients, Frame, IngredientsView, DemoOn));
            }
        }
        #endregion

        #region Constructors
        public AddMedicationViewModel(Frame currentFrame, bool demo)
        {
            this.Frame = currentFrame;

            LoadDoctors();
            Ingredients = new Dictionary<string, int>();
            IngredientsView = new ObservableCollection<string>();
            ConfirmCommand = new MyICommand(OnConfirm, CanExecute);
            CancelCommand = new MyICommand(OnCancel, CanExecute);
            AddIngredientCommand = new MyICommand(OnAddIngredients, CanExecute);
            StopDemo = new MyICommand(OnStop, CanExecute);

            this.DemoOn = demo;
            try
            {
                DemoIsOn(cts.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Greška!");
            }
        }
        #endregion
    }
}
