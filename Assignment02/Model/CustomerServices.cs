using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Assignment02.Model
{
    /*Class use to create different Customer methods that customer performs*/
    class CustomerServices
    {
        const string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStroeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        /*Mehtod verify the user name and password of the Customer from the DB and return true or false.*/
        public bool loginCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                bool result = false;
                string query = $"select * from Customer where userName = @u and password = @p";
                SqlParameter p1 = new SqlParameter("u", customer.userName);
                SqlParameter p2 = new SqlParameter("p", customer.Password);
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
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        /*Method get Customer class object and add this customer record into DB*/
        public bool addCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(conString);
            try
            {
                string query = $"insert into Customer (phoneNo, username,password)" +
                $"  values('{customer.PhoneNo}','{customer.userName}','{customer.Password}')";
                SqlCommand cmd = new SqlCommand(query, connection);
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
    }
}
