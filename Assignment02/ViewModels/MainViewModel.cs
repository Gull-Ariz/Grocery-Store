using Assignment02.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Assignment02.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        /*SelectedViewModel property in bind with Content property of Contonent control in MainUserControl 
         *it show the UserControl respective to ViewModel Selected.*/
        private BaseViewModel selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; NotifyPropertyChanged("SelectedViewModel"); }
        }

        /*Command bind with home, Customer and  Admin button and update the ContentControl Content according to
         selected button.*/
        public DelegateCommand updateViewCommand { get; set; }
        /*here I make the class singleton so I can access the object in everywhere in assembly which is created at
         starting of the program and from that insantance I can access the SelectedViewModel property to assign
        new UserControl to this property to view AdminDashBoard, CustomerHomePage etc views*/
        private static readonly MainViewModel instance = new MainViewModel();
        static MainViewModel()
        {
        }
        private MainViewModel()
        {
            updateViewCommand = new DelegateCommand(ViewSelector, canExecute);
            selectedViewModel = new HomeViewModel();
        }
        /*method return the created instance of the MainViewModel*/
        public static MainViewModel Instance
        {
            get
            {
                return instance;
            }
        }
        /*Method that update the Content of ContentControl according to selected button.*/
        public void ViewSelector(object o)
        {
            if ((o as string).Equals("Admin"))
            {
                SelectedViewModel = new AdLoginVM();
            }
            else if((o as string).Equals("Customer"))
            {
                SelectedViewModel = new CustomerViewModel();
            }
            else if((o as string).Equals("Home"))
            {
                SelectedViewModel = new HomeViewModel();
            }
        }
        /*Method that decide whether Home, Customer and Admin buttons are active or not.*/
        public bool canExecute(object o)
        {
            return true;
        }
    }
}
