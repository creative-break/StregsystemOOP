using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    class Stregsystem : IStregsystem
    {
        List<User> users = new List<User>();
        List<Product> products = new List<Product>();
        List<Transaction> transactions = new List<Transaction>();

        public void ImportUsers()
        {
            User user;
            string pathUsers = AppDomain.CurrentDomain.BaseDirectory + "users.csv";
            StreamReader reader = new StreamReader(pathUsers);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                Int32.TryParse(values[1], out int idValue);
                Int32.TryParse(values[4], out int balanceValue);

                user = new User(idValue, values[1], values[2], values[3], balanceValue, values[5]);

                users.Add(user);
            }
            users.RemoveAt(0);
        }

        public void ImportProducts()
        {
            Product product;
            string pathProducts = AppDomain.CurrentDomain.BaseDirectory + "products.csv";
            StreamReader reader = new StreamReader(pathProducts);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] value = line.Split(';');
                bool activeValue;
                bool onCreditValue;

                Int32.TryParse(value[0], out int idValue);
                value[1] = Regex.Replace(value[1], "<.*?>", string.Empty);
                Int32.TryParse(value[2], out int priceValue);

                if (value[3] == "1")
                {
                    activeValue = true;
                }
                else activeValue = false;

                if (value[4] == "1")
                {
                    onCreditValue = true;
                }
                else onCreditValue = false;

                {
                    priceValue = (int)(priceValue * 0.01f);
                    product = new Product(idValue, value[1], priceValue, activeValue, onCreditValue);
                    products.Add(product);
                }
            }
        }

        public void ImportCsv()
        {
            ImportProducts();
            ImportUsers();
        }

        public void WriteToFile()
        {
            throw new NotImplementedException();
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction buyTransaction = new BuyTransaction(user, product);
            buyTransaction.Execute();
            // returnere til samme liste
            return buyTransaction;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(user, amount);
            insertCashTransaction.Execute();
            transactions.Add(insertCashTransaction);
            return insertCashTransaction;
        }

        public Product GetProductByID(int id)
        {
            Product product = products.Where(p => p.Id == id).FirstOrDefault(); // Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            User user = users.Where(u => u.Username == username).FirstOrDefault(); // Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
            if (users == null)
            {
                throw new Exception("No user found");
            }

            else
            {
                return user;
            }
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {

            return transactions.Where(t => t.User.Equals(user));

        }

        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return products.Where(product => product.Active);
            }
        }

    }
}
