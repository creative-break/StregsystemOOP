using System;
using System.Text.RegularExpressions;

namespace Stregsystem
{

    class User : IComparable<User>
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;
        private int _balance;

        public User(int id, string firstname, string lastname, string username, int balance,
                    string email)
        {
            _id = id;
            _firstname = firstname;
            _lastname = lastname;
            _username = username;
            _email = email;
            _balance = balance;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                if (value != null)
                {
                    _firstname = value;
                }
                else throw new ArgumentNullException();
            }
        }

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (value != null)
                {
                    _lastname = value;
                }
                else throw new ArgumentNullException();
            }
        }

        public string Username
        {

            get { return _username; }

            set
            {
                var userMatch = Regex.Match(value, "^[a-zA-Z0-9_]");

                {
                    if (userMatch.Success)
                    {
                        _username = value;
                    }
                    else
                        throw new ArgumentOutOfRangeException();
                }

            }

        }

        public string Email
        {
            get { return _email; }
            set
            {
                var emailMatch = Regex.Match(value, "^[a-zA-Z0-9._-]{1,100}@[a-zA-Z0-9]{1,100}[a-zA-Z0-9._-].[a-zA-Z]{0,100}$");
                {

                    if (emailMatch.Success)
                    {
                        _email = value;
                    }
                    else
                        throw new ArgumentOutOfRangeException();

                }
            }
        }

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public override string ToString()
        {
            return $"{_firstname} {_lastname} {_email}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(User other)
        {
            return _id.CompareTo(other._id);
        }

    }

}
