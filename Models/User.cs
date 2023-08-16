using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grover.Models
{
    public class User
    {
        private int _id;
        private string _username;
        private string _password;
        private string _email;
        private UserType _type;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private string _address;
        private string _image;
        private bool _enabled;

        public User() { }

        public User(string username, string password, string email, UserType type, string firstName, string lastName, DateTime birthDate, string address, string image)
        {
            if(type.Equals(UserType.KORISNIK))
            {
                Username = username;
                Password = password;
                Type = type;
                Email = email;
                BirthDate = birthDate;
                Address = address;
                Image = image;
                Enabled = true;
            }
            else if(type.Equals(UserType.DOSTAVLJAC))
            {
                Username = username;
                Password = password;
                Type = type;
                Email = email;
                BirthDate = birthDate;
                Address = address;
                Image = image;
                Enabled = false;
            }
        }
        
        //Database constructor
        public User(int id, string username, string password, string email, UserType type, string firstName, string lastName, DateTime birthDate, string address, string image, bool enabled)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Type = type;
            Email = email;
            BirthDate = birthDate;
            Address = address;
            Image = image;
            Enabled = enabled;
        }

        //Register constructor
        public User(string username, string password, string email, UserType type, string firstName, string lastName, DateTime birthDate, string address)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Type = type;
            Email = email;
            BirthDate = birthDate;
            Address = address;
        }

        //Facebook data constructor
        public User(string email, string firstName, string lastName, string image)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Image = image;
            Type = UserType.KORISNIK;
            Enabled = true;
        }

        public int Id { get => _id; set => _id = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }
        public UserType Type { get => _type; set => _type = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
        public string Address { get => _address; set => _address = value; }
        public string Image { get => _image; set => _image = value; }
        public bool Enabled { get => _enabled; set => _enabled = value; }
    }
}