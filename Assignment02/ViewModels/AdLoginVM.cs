using Assignment02.Commands;
using Assignment02.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Assignment02.ViewModels
{
    /*this viewmodel is for AdminLoginControl view*/
    class AdLoginVM : BaseViewModel
    {
        /*  Admin credientails for login into system  */
        /*  User Name = Gull    */
        /*  Password  = ariz123 */
        public DelegateCommand LoginCmd { get; set; }//Command bind with Login button
        public AdLoginVM()
        {
            LoginCmd = new DelegateCommand(Execute, canExecute);
        }
        /*method extract the user name and password from parameter 'o', then create new admin object with this
         user name and password and call the AdminServices class method loginAdmin if it returns true
        AdminDashboard is shows else error msg is shown to user.*/
        public void Execute(object o)
        {
            var list = o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            string name = textBox.Text;
            string password = passwordBox.Password;
            Admin admin = new Admin { userName = name,Password=password };
            AdminServices adminServices = new AdminServices();
            bool result = adminServices.loginAdmin(admin);
            if (result == true)
            {
                MainViewModel mainViewModel = MainViewModel.Instance;
                mainViewModel.SelectedViewModel = new AdminViewModel();
            }
            else
            {
                MessageBox.Show("User Name or Password is Incorrect");
            }
        }
        /* method decide whether login button active or not,
         * it extract the user name and password from parameter 'o'
         and return only true if both fields are not empty.*/
        public bool canExecute(object o)
        {
            var list = o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            string name = textBox.Text;
            string password = passwordBox.Password;
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
