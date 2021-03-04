using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment02.Model
{
    /*Admin class with admin username and password properties.*/
    class Admin 
    {
        private string username;

        public string userName
        {
            get { return username; }
            set { 
                username = value;
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
