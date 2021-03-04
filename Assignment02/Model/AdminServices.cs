using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Microsoft.Data.SqlClient;
namespace Assignment02.Model
{
    /*class use to create different admin methods that Admin perform*/
    class AdminServices
    {
        const string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStroeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        /*Mehtod verify the user name and password of the Admin from the DB and return true or false.*/
        public bool loginAdmin(Admin admin)
        {
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                bool result = false;
                string query = $"select * from Admin where userName = @u and password = @p";
                SqlParameter p1 = new SqlParameter("u", admin.userName);
                SqlParameter p2 = new SqlParameter("p", admin.Password);
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.Add(p1);
                sqlCommand.Parameters.Add(p2);
                connection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    result = true;
                }
                return result;
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        /*Method get Product class object and insert this product record in DB and return true or false,
         based on whether product is record is entered or not.*/
        public (bool, bool) addProduct(Product product)
        {
            SqlConnection connection = new SqlConnection(conString);
            bool p_key_vol = false;
            try
            {
                string query = $"insert into Products (Id, name,price,quantity)" +
                $"  values('{product.ID}','{product.Name}','{product.Price}','{product.Quantiy}')";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                int rowsInserter = cmd.ExecuteNonQuery();
                if (rowsInserter > 0)
                {
                    return (true,p_key_vol);
                }
                else
                {
                    return (false,p_key_vol);
                }
            }
            catch(SqlException e) when (e.Number == 2627)
            {
                p_key_vol = true;
                return (false,p_key_vol);
            }
            catch (Exception )
            {
                return (false,p_key_vol);
            }
            finally
            {
                connection.Close();
            }
        }
        /*method get Product ID and delete this product from DB and return true or false,
         based on whether product is delted or not.*/
        public bool deleteProduct(int Id)
        {
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                string query = $"delete from Products where ID = @id";
                SqlParameter p1 = new SqlParameter("id", Id);
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(p1);
                connection.Open();
                int rowsInserter = cmd.ExecuteNonQuery();
                if (rowsInserter > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        /*mehtod return all the products list that are saved in DB*/
        public ObservableCollection<Product> getAllProducts()
        {
            ObservableCollection<Product> list = new ObservableCollection<Product>();
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = "Select * from Products";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product p = new Product();
                    p.ID = (int)dr.GetValue(0);
                    p.Name = (string)dr.GetValue(1);
                    p.Price = (decimal)dr.GetValue(2);
                    p.Quantiy = (int)dr.GetValue(3);
                    list.Add(p);
                }
            }
            catch (Exception )
            {
                
            }
            finally
            {
                con.Close();
            }
            return list;
        }
    }
}
