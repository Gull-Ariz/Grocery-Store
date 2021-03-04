using System;
using System.Collections.Generic;
using System.Text;
using Assignment02.Commands;
using Assignment02.Model;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Assignment02.ViewModels
{
    class ViewProductsViewModel : BaseViewModel
    {
        public DelegateCommand goBackCommand { get; set; }//bind with go back button
        public ObservableCollection<Product> products { get; set; }//bind with listbox in viewProductsUserControl
        public ViewProductsViewModel()
        {
            goBackCommand = new DelegateCommand(goBack, canGoBack);
            AdminServices adminServices = new AdminServices();
            products = adminServices.getAllProducts();
        }
        /*Method get the created instance of mainviewmodel and from that instance, SelectedViewModel is assigned with
         AdminViewModel to show the adminDashBoard in ContentControl.*/
        public void goBack(object o)
        {
            MainViewModel mainViewModel = MainViewModel.Instance;
            mainViewModel.SelectedViewModel = new AdminViewModel();
        }
        /*Method decide whether GoBack Butthon is active or not.*/
        public bool canGoBack(object o)
        {
            return true;
        }
    }
}
