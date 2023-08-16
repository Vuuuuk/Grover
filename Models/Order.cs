using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grover.Models
{
    public class Order
    {
        private int _orderID;
        private double _totalPrice;
        private int _ammount;
        private string _comment;
        private string _deliveryAddress;
        private string _status;
        private int _userID;

        public Order() { }

        public Order(double totalPrice, int ammount, string comment, string deliveryAddress, string status, int userID)
        {
            TotalPrice = totalPrice;
            Ammount = ammount;
            Comment = comment;  
            DeliveryAddress = deliveryAddress;
            Status = status;
            UserID = userID;
        }

        public Order(int orderID, double totalPrice, int ammount, string comment, string deliveryAddress, string status, int userID)
        {
            OrderID = orderID;
            TotalPrice = totalPrice;
            Ammount = ammount;
            Comment = comment;
            DeliveryAddress = deliveryAddress;
            Status = status;
            UserID = userID;
        }

        public int OrderID { get => _orderID; set => _orderID = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public int Ammount { get => _ammount; set => _ammount = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public string DeliveryAddress { get => _deliveryAddress; set => _deliveryAddress = value; }
        public string Status { get => _status; set => _status = value; }
        public int UserID { get => _userID; set => _userID = value; }
    }
}