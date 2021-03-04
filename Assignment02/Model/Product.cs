using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment02.Model
{
    /*Product class with product ID, Name, Price, Quantity properties.*/
    class Product
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        private int quantity;

        public int Quantiy
        {
            get { return quantity; }
            set { quantity = value; }
        }


    }
}
