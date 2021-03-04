using Assignment02.Commands;
using Assignment02.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace Assignment02.ViewModels
{
    class AdminViewModel : BaseViewModel
    {
        /*these four properties ID, Name, Price, Quantity are bind with textboxes in AdminDashboard usercontrol
         to get id, name, price and quantity of product for adding in record.*/
        public string ID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }

        /*idDel property bind with textbox for getting ID of product
         * used to validate the ID for deleting the product from records*/
        public string idDel { get; set; }

        /*v_id_text property used to show message to user whether deleting product ID is correct or not*/
        private string id_text;
        public string v_id_text { 
            get { return id_text; }
            set { id_text = value; NotifyPropertyChanged("v_id_text"); }
        }
        /*these four properties idText, nameText, priceText, quantityText used to show output
         to Admin whether the input is correct or not for Adding product in records.*/
        private string id;
        public string idText
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged("idText"); }
        }
        private string name;
        public string nameText
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("nameText"); }
        }

        private string price;
        public string priceText
        {
            get { return price; }
            set { price = value; NotifyPropertyChanged("priceText"); }
        }
        private string quantity;
        public string quantityText
        {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("quantityText"); }
        }
        public DelegateCommand AddProduct { get; set; }//Bind with AddProduct Button
        public DelegateCommand viewProductsCmd { get; set; }//Bind with Products Button
        public DelegateCommand deleteCommand { get; set; }//Bind with DELETE Button
        public DelegateCommand logoutCmd { get; set; }//Bind with LOGOUT Button
        public AdminViewModel()
        {
            AddProduct = new DelegateCommand(addProduct, canAddProduct);
            viewProductsCmd = new DelegateCommand(viewProducts, canViewProducts);
            deleteCommand = new DelegateCommand(deleteProduct, canDelete);
            logoutCmd = new DelegateCommand(Logout, canLogout);
        }
        /*method for Logout*/
        public void Logout(object o)
        {
            MainViewModel mainViewModel = MainViewModel.Instance;
            mainViewModel.SelectedViewModel = new AdLoginVM();
        }
        /*method decide logout button active or not*/
        public bool canLogout(object o)
        {
            return true;
        }
        /*method that delete the product from database it calls the AdminServices class method deleteProduct
         for deleting the product from DB.*/
        public void deleteProduct(object o)
        {
            AdminServices adminServices = new AdminServices();
            bool result = adminServices.deleteProduct(System.Convert.ToInt32(idDel));
            if(result == true)
            {
                MessageBox.Show("Product deleted successfully");
            }
            else
            {
                MessageBox.Show("Product is not in Products list");
            }
        }
        /*method check Delete button active or not return true mean button active and only return true when ID
         is in proper format*/
        public bool canDelete(object o)
        {
            if(!string.IsNullOrEmpty(idDel))
            {
                try
                {
                    v_id_text = "";//at start the message is blank
                    int i = System.Convert.ToInt32(idDel);
                    if(i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        v_id_text = "ID not valid";//this message is shown to admin in view
                        return false;
                    }
                }
                catch(Exception)
                {
                    v_id_text = "ID not valid";//this message is shown to admin in view
                    return false;
                }
            }
            else
            {
                v_id_text = "";
                return false;
            }
        }
        /*this method is open new usercontrol in which all products list is shown. Here I get MainViewModel Instance,
         * Since, MainViewModel is a singleton class so new object is not created
         and then from that we assign SelectedViewModel property ViewProductsViewModel*/
        public void viewProducts(object o)
        {
            MainViewModel mainViewModel = MainViewModel.Instance;
            mainViewModel.SelectedViewModel = new ViewProductsViewModel();
        }
        /*Method decide PRODUCTS button active or not.*/
        public bool canViewProducts(object o)
        {
            return true;
        }
        /* this method convert the ID, Name, Price and Quantity into appropriate data type and make new Product object, 
         * it call addProduct method of AdminServices class to add product into database and show output product reord
         is added or not.*/
        public void addProduct(object o)
        {
            AdminServices adminService = new AdminServices();
            Product product = new Product();
            product.ID = System.Convert.ToInt32(ID);
            product.Name = Name;
            product.Price = System.Convert.ToDecimal(Price);
            product.Quantiy = System.Convert.ToInt32(Quantity);
            (bool result,bool p_key_vol) = adminService.addProduct(product);
            if (result == true) 
            {
                MessageBox.Show("Product added successfully.");
            }
            else if(p_key_vol == true)
            {
                MessageBox.Show("Product with this ID already exists use different unique ID.");
            }
            else
            {
                 MessageBox.Show("Product is not added successfully.");
            }
        }
        /*method decide whether add product button is active or not it validate all the textboxs input used to
         * product properties e.g ID, Name, Price, Qty return true only when all the textboxes input format is correct.
         * it uses checkValidation helper method for this.*/
        public bool canAddProduct(object o)
        {
            bool i = false; bool n = false; bool p = false; bool q = false;
            if(!string.IsNullOrEmpty(ID))
            {
                i = checkVaildation(ID);
            }
            else if(string.IsNullOrEmpty(ID))
            {
                idText = "";
            }
            if(!string.IsNullOrEmpty(Name))
            {
                bool res = false;
                if(!char.IsLetter(Name[0]))
                {
                    nameText = "Name start with letter only.";
                }
                else
                {
                    Regex regex = new Regex("^[\\w ]+$");
                    res = regex.IsMatch(Name);
                    if (res == true)
                    {
                        nameText = "";
                        n = true;
                    }
                    else
                    {
                        nameText = "Name can't contian special char.";
                    }
                }
            }
            else if(string.IsNullOrEmpty(Name))
            {
                nameText = "";
            }
            if (!string.IsNullOrEmpty(Price))
            {
                p = checkVaildation(Price);
            }
            else if(string.IsNullOrEmpty(Price))
            {
                priceText = "";
            }
            if(!string.IsNullOrEmpty(Quantity))
            {
                q = checkVaildation(Quantity);
            }
            else if (string.IsNullOrEmpty(Quantity))
            {
                quantityText = "";
            }
            if(i == true && p == true && q == true && n == true)
            {
                return true;
            }
            else
            {
                i = false; n = false; p = false; q = false;
                return false;
            }
            
        }
        /*Helper method use to validate Properties Format e.g ID, Price and Quantity*/
        private bool checkVaildation(string prop)
        {
            bool i = false;
            try
            {
                int a = System.Convert.ToInt32(prop);
                if (a <= 0)
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
                if(prop == Quantity)
                {
                    quantityText = "Quantity is not valid.";
                }
                else if(prop == Price)
                {
                    priceText = "Price is not valid.";
                }
                else if(prop == ID)
                {
                    idText = "ID is not valid.";
                }
                i = false;
            }
            return i;
        }
    }
}
