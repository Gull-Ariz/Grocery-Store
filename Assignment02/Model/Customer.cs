using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment02.Model
{
    /*Customer class with customer properties e.g user name, password and phone no.*/
    class Customer
    {
        private string username;

        public string userName
        {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string phoneno;

        public string PhoneNo
        {
            get { return phoneno; }
            set { phoneno = value; }
        }
    }
}
