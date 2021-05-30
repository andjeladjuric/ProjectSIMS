using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalService.View.ManagerUI.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //protected virtual void SetProperty<T>(ref T member, T val,
        //   [CallerMemberName] string propertyName = null)
        //{
        //    if (object.Equals(member, val)) return;

        //    member = val;
        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}