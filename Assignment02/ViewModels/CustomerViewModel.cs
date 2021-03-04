using Assignment02.Commands;
using Assignment02.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Assignment02.ViewModels
{
    class CustomerViewModel : BaseViewModel
    {
        /*ph_v_text property is bind with textblock in view to show the message to user whether the phone no
         entered is in correct format or not.*/
        private string ph_text;

        public string ph_v_text
        {
            get { return ph_text; }
            set { ph_text = value;NotifyPropertyChanged("ph_v_text"); }
        }
        /*signUpCmd is bind with SIGNUP button */
        public DelegateCommand signUpCmd { get; set; }
        /*loginCommand is bind with LOGIN button */
        public DelegateCommand loginCommand { get; set; }
        public CustomerViewModel()
        {
            signUpCmd = new DelegateCommand(SignUp, canSignUp);
            loginCommand = new DelegateCommand(Login, canLogin);
        }
        /*Method extract name and password from the parameter o then create new Customer object assigning this username
         and password and call AdminServices class method loginCustomer to check the validation of customer,
        if username and password are validate Customer redirect to SALE window other show error message.*/
        public void Login(object o)
        {
            Customer customer = new Customer();
            var list = o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            customer.userName = textBox.Text.ToString();
            customer.Password = passwordBox.Password.ToString();
            CustomerServices customerServices = new CustomerServices();
            bool result = customerServices.loginCustomer(customer);
            if(result == true)
            {
                MainViewModel mainViewModel = MainViewModel.Instance;
                mainViewModel.SelectedViewModel = new AddToCartVM();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
        /*Method decide whether the login button active or not, it extract name and password from the parameter o
         and return true if both fields are not empty.*/
        public bool canLogin(object o)
        {
            var list = o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            string userName = textBox.Text;
            string password = passwordBox.Password;
            if(!string.IsNullOrEmpty(userName) && ! string.IsNullOrEmpty(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*Method extract user name password and phone no from the parameter o then create new customer object
         * with these properties and call Customer service class method addCustomer to add the record of Customer 
         into DB and show output to customer whether account is crated or not.*/
        public void SignUp(object o)
        {
            Customer customer = new Customer();
            var list = o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            TextBox textBox2 = list[2] as TextBox;
            customer.userName = textBox.Text.ToString();
            customer.Password = passwordBox.Password.ToString();
            customer.PhoneNo = textBox2.Text.ToString();
            string err_msg = "";
            if(customer.userName.Length <= 20 && customer.Password.Length <= 15)
            {
                CustomerServices customerService = new CustomerServices();
                bool result = customerService.addCustomer(customer);
                if (result == true)
                {
                    MessageBox.Show("Account Created Successfully.");
                }
                else
                {
                    MessageBox.Show("Error in creating Account.");
                }
            }
            if(customer.userName.Length > 20)
            {
                err_msg += "User Name Length not greater then 20.";
            }
            if(customer.Password.Length > 15)
            {
                err_msg += "\nMax Password length is 15 characters.";
            }
            if(err_msg.Length > 1)
            {
                MessageBox.Show("Error in creating account.\n" + err_msg);
            }
        }
        /*Mehod decide whether SIGNUP button is active or not,
         * it extract user name password and phone no from the parameter o and 
         then validate the phone no format set error message with ph_v_text which is bind in view to show error to user
         and return true only if all the fields are not empty and phone no is
         in correct format.*/
        public bool canSignUp(object o)
        {
            var list =  o as List<object>;
            TextBox textBox = list[0] as TextBox;
            PasswordBox passwordBox = list[1] as PasswordBox;
            TextBox textBox2 = list[2] as TextBox;
            string userName = textBox.Text;
            string password = passwordBox.Password;
            string phoneNo = textBox2.Text;
            bool res = false;
            if (!string.IsNullOrEmpty(phoneNo))
            {
                Regex regex = new Regex(@"^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$");
                res = regex.IsMatch(phoneNo);
                if(res == false)
                {
                    ph_v_text = "PhoneNo not valid.";
                }
                else
                {
                    ph_v_text = "";
                    res = true;
                }
            }
            else
            {
                ph_v_text = "";
            }
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(phoneNo) && res == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
