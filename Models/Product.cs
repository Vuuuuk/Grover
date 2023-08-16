using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grover.Models
{
    public class Product
    {
        private int _id;
        private string _name;
        private double _price;
        private string _ingredients;

        public Product() { }

        public Product(string name, double price, string ingredients)
        {
            Name = name;
            Price = price;
            Ingredients = ingredients; 
        }

        public Product(int id, string name, double price, string ingredients)
        {
            Id = id;
            Name = name;
            Price = price;
            Ingredients = ingredients;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }
        public string Ingredients { get => _ingredients; set => _ingredients = value; }
    }
}