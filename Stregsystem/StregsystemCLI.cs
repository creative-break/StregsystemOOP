using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stregsystem
{
    class StregsystemCLI : IStregsystemUI
    {

        bool running;
        IStregsystem _stregsystem;
        string line = "--------------------------------------------------------------------------------------";
        string welcomeMessage = "Welcome to the stregsystem!\n\n" +
                                  "Type in your username followed by the id of the product you wish to buy\n" +
                                  "OR enter your username to see full name, credit and previous purchases\n";

        public event StregsystemEvent CommandEntered;

        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
        }        

        public void Close()
        {
            running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Command not regonized {adminCommand}");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"An error occured {errorString}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"{user} your balance is to low to buy: {product}");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Produkt: {product} not found");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguments {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"Transaction complete {transaction}");
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"Products bought:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public void DisplayTransaction(Transaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"User: {user.Username} | Name: {user.Firstname} {user.Lastname} | Balance: {user.Balance} ");
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"{username} not found");
        }

        public void Start()
        {
            running = true;
            DrawMenu();
            Console.WriteLine(welcomeMessage);

            while (running)
            {
                try
                {
                    CommandEntered?.Invoke(Console.ReadLine());
                }
                catch (Exception)
                {
                    DrawMenu();
                    Console.WriteLine(welcomeMessage);

                }
            }
        }
        public void DrawMenu()
        {            
            Console.Clear();
            Console.WriteLine("AVAILABLE PRODUCTS");
            Console.WriteLine(line);
            foreach (Product i in _stregsystem.ActiveProducts)
            {
                Console.WriteLine(i);
                Console.WriteLine(line);
            }
        }
        
    }

}