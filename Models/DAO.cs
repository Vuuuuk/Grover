using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Grover.Models
{
    public static class DAO
    {
        private static SqlConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ChowNowConnectionString"].ConnectionString);
        private static StringBuilder _hashed = new StringBuilder();

        //Encrypting the passwords
        public static string EncryptPassword(string password)
        {
            StringBuilder _hashed = new StringBuilder();
            byte[] encrypted = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte b in encrypted)
                _hashed.Append(b.ToString("x2"));

            return _hashed.ToString();
        }

        //Load all Contain data
        public static List<ChowNow_Contains> LoadAllContainData()
        {
            List<ChowNow_Contains> containData = new List<ChowNow_Contains>();

            _db.Open();

            string query = "select * from ChowNow_Contains";
            SqlCommand command = new SqlCommand(query, _db);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
                containData.Add(new ChowNow_Contains(dr.GetInt32(0),
                                                     dr.GetInt32(1),
                                                     dr.GetInt32(2)));

            _db.Close();
            return containData;
        }

        //Add Contains data
        public static void AddContainsData(int orderID, int productID)
        {
            _db.Open();

            string query = "insert into ChowNow_Contains VALUES " +
                            "('"
                            + orderID + "','" + productID +
                            "')";

            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }

        //Load all orders
        public static List<Order> LoadAllOrders()
        {
            List<Order> orders = new List<Order>();

            _db.Open();

            string query = "select * from ChowNow_Order";
            SqlCommand command = new SqlCommand(query, _db);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
                orders.Add(new Order(dr.GetInt32(0),
                                     dr.GetDouble(1),
                                     dr.GetInt32(2),
                                     dr.GetString(3),
                                     dr.GetString(4),
                                     dr.GetString(5),
                                     dr.GetInt32(6)));

            _db.Close();
            return orders;
        }

        //Add an order
        public static void AddOrder(double totalPrice, int totalAmmount, string commment, string deliveryAddress, string status, int userID)
        {
            _db.Open();

            string query = "insert into ChowNow_Order VALUES " +
                           "('"
                            + totalPrice + "','" + totalAmmount + "','" + commment + "','"
                            + deliveryAddress + "','" + status + "','" + userID +
                            "')";

            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }

        //Add a product
        public static void AddProduct(string name, double price, string ingredients)
        {
            _db.Open();

            string query = "insert into ChowNow_Product VALUES " +
                           "('"
                            + name + "','" + price + "','" + ingredients +
                            "')";

            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }

        //Load all products
        public static List<Product> LoadAllProducts()
        {
            List<Product> products = new List<Product>();

            _db.Open();

            string query = "select * from ChowNow_Product";
            SqlCommand command = new SqlCommand(query, _db);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
                products.Add(new Product (dr.GetInt32(0),
                                          dr.GetString(1),
                                          dr.GetDouble(2),
                                          dr.GetString(3)));

            _db.Close();
            return products;
        }

        //Activate a user
        public static void ActivateUser(int userID, bool activate)
        {
            _db.Open();
            string query = string.Empty;

            if(activate)
                query = "update ChowNow_User set enabled = 'True' where user_id = " + userID;
            else
                query = "update ChowNow_User set enabled = 'False' where user_id = " + userID;

            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }

        //Load all users
        public static Dictionary<string, User> LoadAllUsers()
        {
            Dictionary<string, User> users = new Dictionary<string, User>();

            _db.Open();
            string query = "select * from ChowNow_User";
            SqlCommand command = new SqlCommand(query, _db);
            SqlDataReader dr = command.ExecuteReader();

            while(dr.Read())
                users.Add(dr.GetString(1), new User(dr.GetInt32(0),
                                                        dr.GetString(1),
                                                        dr.GetString(2),
                                                        dr.GetString(3),
                                                        dr.GetString(4).Equals(UserType.DOSTAVLJAC.ToString()) ? UserType.DOSTAVLJAC : 
                                                        dr.GetString(4).Equals(UserType.KORISNIK.ToString()) ? UserType.KORISNIK : UserType.ADMIN,
                                                        dr.GetString(5),
                                                        dr.GetString(6),
                                                        dr.GetDateTime(7),
                                                        dr.GetString(8),
                                                        dr.GetString(9).Equals(string.Empty) ? string.Empty : dr.GetString(9),
                                                        dr.GetString(10).Equals("True") ? true : false));

            _db.Close();
            return users;
        }

        //Add a user
        public static void AddUser(User u)
        {
            _db.Open();

            string query = "insert into ChowNow_User VALUES " +
                "('"
                    + u.Username + "','" + u.Password + "','" + u.Email + "','"
                    + u.Type.ToString() + "','" + u.FirstName + "','" + u.LastName + "','" + u.BirthDate
                    + "','" + u.Address + "','" + u.Image + "','" + u.Enabled.ToString() + 
                "')";
            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }

        //Update user
        public static void UpdateUser(User u)
        {
            _db.Open();

            string query = "update ChowNow_User set username = '" + u.Username + "'," +
                           "password = '" + u.Password + "'," +
                           "email = '" + u.Email + "'," +
                           "user_type = '" + u.Type.ToString() + "'," +
                           "first_name = '" + u.FirstName + "'," + 
                           "last_name = '" + u.LastName + "'," +
                           "birth_date = '" + u.BirthDate + "'," +
                           "address = '" + u.Address + "'," +
                           "image = '" + u.Image + "'," +
                           "enabled = '" + u.Enabled.ToString() + "'" +
                           "where user_id = " + u.Id;

            SqlCommand command = new SqlCommand(query, _db);
            command.ExecuteNonQuery();

            _db.Close();
        }
    }
}