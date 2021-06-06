using HospitalService.View.ManagerUI.Views.Help;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class HelpViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<string> views;
        private int selectedIndex;
        #endregion

        #region Properties
        public Frame Frame { get; set; }
        public ObservableCollection<string> Views
        {
            get { return views; }
            set
            {
                views = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public MyICommand SelectionChanged { get; set; }
        #endregion

        #region Actions
        private void OnChange()
        {
            if(SelectedIndex == 0)
            {
                this.Frame.NavigationService.Navigate(new RoomsHelp());
            }
            else if(SelectedIndex == 1)
            {
                this.Frame.NavigationService.Navigate(new InventoryHelp());
            }
            else if (SelectedIndex == 2)
            {
                this.Frame.NavigationService.Navigate(new MedsHelp());
            }
            else if (SelectedIndex == 3)
            {
                this.Frame.NavigationService.Navigate(new ReportsHelp());
            }

        }
        #endregion

        #region Functions
        private void LoadViews()
        {
            Views = new ObservableCollection<string>();
            Views.Add("Prostorije");
            Views.Add("Inventar");
            Views.Add("Lekovi");
            Views.Add("Izveštaji");
        }
        #endregion

        #region Constructors
        public HelpViewModel(Frame newFrame)
        {
            this.Frame = newFrame;
            this.SelectedIndex = -1;
            LoadViews();

            SelectionChanged = new MyICommand(OnChange);
        }
        #endregion
    }
}
