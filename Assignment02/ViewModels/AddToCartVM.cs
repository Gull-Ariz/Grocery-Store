using Assignment02.Commands;
using Assignment02.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Assignment02.ViewModels
{
    class AddToCartVM : BaseViewModel
    {
        /*ID property bind with view to get the item ID*/
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged("ID"); }
        }
        /*Quantity property bind with view to get the quantity of item.*/
        private string quantity;

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value;NotifyPropertyChanged("Quantity"); }
        }
        /*idText property bind with addToCart usercontrol to show user whether the entered ID is in correct format or not.*/
        private string idtext;
        public string idText
        {
            get { return idtext; }
            set { idtext = value; NotifyPropertyChanged("idText");}
        }
        /*quantityText property bind with addToCart usercontrol to show user whether the entered Quantity
         * is in correct format or not.*/
        private string quantitytext;
        public string quantityText
        { 
            get { return quantitytext; }
            set { quantitytext = value; NotifyPropertyChanged("quantityText"); }
        }
        /*availableItemList is bind with ListBox in addToCart usercontrol to show all the available items.*/
        public ObservableCollection<Product> availableItemList { get; set; }
        /*cartItemsList is bind with 2nd listbox in addToCart usercontrol to show all the items which user add into cart.*/
        public ObservableCollection<Product> cartItemsList { get; set; }
        public DelegateCommand addCommand { get; set; }//Command Bind with Add button in addToCart usercontrol
        public DelegateCommand logoutCmd { get; set; }//Command Bind with logout button in addToCart usercontrol
        public DelegateCommand finishCmd { get; set; }//Command bind with finish button in addToCart usercontrol
        public AddToCartVM()
        {
            AdminServices adminServices = new AdminServices();
            availableItemList = adminServices.getAllProducts();
            cartItemsList = new ObservableCollection<Product>();
            addCommand = new DelegateCommand(addToCart, canAdd);
            logoutCmd = new DelegateCommand(Logout, canLogout);
            finishCmd = new DelegateCommand(Finish, canFinish);
        }
        /*Method calls when user click Finish button in addToCart usercontrol, it loop throw all the items in 
         cartItemsList to calculate total and show the receipt to user.*/
        public void Finish(object o)
        {
            decimal total = 0;
            string res = "Receipt".PadLeft(30);res += "\n\n";
                res += "Product Name".PadRight(50); res += "Price".PadRight(10);     res += "Quantity".PadRight(10);res +="\n";
            foreach(Product p in cartItemsList)
            {
                int x = p.Name.Length;
                int padLen = 67 - x;
                res += p.Name.PadRight(padLen);
                res += p.Price.ToString().PadRight(10);
                res += "  "+p.Quantiy.ToString().PadRight(10);
                res += "\n";
                total += p.Price * p.Quantiy;
            }
            res += "\n\n";
            res += "Total".PadRight(62);
            res += total.ToString();
            
            MessageBox.Show(res);
        }
        /*Method decide whether the finish button is active or not is returns only true when user add at least
         one item into cart.*/
        public bool canFinish(object o)
        {
            if(cartItemsList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*Method logout the customer and assign CustomerViewModel to the SelectedViewModel property of MainViewModel
         class to show CustomerHomePage*/
        public void Logout(object o)
        {
            MainViewModel mainViewModel = MainViewModel.Instance;
            mainViewModel.SelectedViewModel = new CustomerViewModel();
        }
        /*Method decide whether logout button is active or not.*/
        public bool canLogout(object o)
        {
            return true;
        }
        /*method check that whether the product is available with entered ID in availableItemsList if available
         it is added into cartItemsList otherwise show the error msg to customer.*/
        public void addToCart(object o)
        {
            bool res = false;
            Product product = new Product();
            foreach(Product p in availableItemList)
            {
                if(p.ID == System.Convert.ToInt32(ID))
                {
                    if(System.Convert.ToInt32(Quantity) <= p.Quantiy)
                    {
                        product.ID = p.ID;
                        product.Name = p.Name;
                        product.Price = p.Price;
                        product.Quantiy = System.Convert.ToInt32(Quantity);
                        cartItemsList.Add(product);
                        res = true;
                        break;
                    }
                    else
                    {
                        res = true;
                        MessageBox.Show("select proper quantity");
                    }
                }
            }
            if(res == false)
            {
                MessageBox.Show("Please select the item from the available items list.");
            }
        }
        /* method decide whether Add button is active or not, it returns true only when both the ID and Quantity
         fields are in correct format.*/
        public bool canAdd(object o)
        {
            bool i = false; bool q = false;
            if (!string.IsNullOrEmpty(ID))
            {
                i = checkVaildation(ID);
            }
            else if (string.IsNullOrEmpty(ID))
            {
                idText = "";
            }
            if (!string.IsNullOrEmpty(Quantity))
            {
                q = checkVaildation(Quantity);
            }
            else if (string.IsNullOrEmpty(Quantity))
            {
                quantityText = "";
            }
            if (i == true &&  q == true)
            {
                return true;
            }
            else
            {
                i = false; q = false;
                return false;
            }
        }
        /*Helper method to validate the entered ID and Quantity, it shows appropriate error msg if property
         format is not correct.*/
        private bool checkVaildation(string prop)
        {
            bool i = false;
            try
            {
                int a = System.Convert.ToInt32(prop);
                if (a < 0)
                {
                    throw new Exception();
                }
                else
                {
                    i = true;
                }
            }
            catch (Exception)
            {
                if (prop == Quantity)
                {
                    quantityText = "Quantity is not valid.";
                }
                else if (prop == ID)
                {
                    idText = "ID is not valid.";
                }
                i = false;
            }
            return i;
        }
    }
}
