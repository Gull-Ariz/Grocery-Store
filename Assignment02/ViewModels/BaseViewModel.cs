using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment02.ViewModels
{
    /*Since every view model class implement the INotifyPropertyChanged Interface. So, implementing in every class
     we implement it here and inherit all the classes from this BaseViewModel class.*/
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        
    }
}
