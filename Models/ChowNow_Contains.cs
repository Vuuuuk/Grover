using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grover.Models
{
    public class ChowNow_Contains
    {
        private int _orderID;
        private int _productID;
        private int _containsID;

        public ChowNow_Contains() { }
        public ChowNow_Contains(int containsID, int orderID, int productID) 
        {
            ContainsID = containsID;
            OrderID = orderID;
            ProductID = productID;
        }

        public int OrderID { get => _orderID; set => _orderID = value; }
        public int ProductID { get => _productID; set => _productID = value; }
        public int ContainsID { get => _containsID; set => _containsID = value; }
    }
}