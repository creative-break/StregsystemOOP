using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Stregsystem
{
    class StregsystemCommandParser
    {
        IStregsystem _stregsystem;
        IStregsystemUI _stregsystemCLI;

        readonly Dictionary<string, Action<List<string>>> _admincommands = new Dictionary<string, Action<List<string>>>();

        public StregsystemCommandParser(IStregsystemUI stregsystemCLI, IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _stregsystemCLI = stregsystemCLI;
            _stregsystemCLI.CommandEntered += ParseCommand;

            _admincommands.Add(":q", (List<string> args) => stregsystemCLI.Close());
            _admincommands.Add(":quit", (List<string> args) => stregsystemCLI.Close());
            _admincommands.Add(":activate", (List<string> args) => ActivateProduct(args));
            _admincommands.Add(":deactivate", (List<string> args) => DeactivateProduct(args));
            _admincommands.Add(":crediton", (List<string> args) => CreditOn(args));
            _admincommands.Add(":creditoff", (List<string> args) => CreditOff(args));
            _admincommands.Add(":addcredits", (List<string> args) => AddCredits(args));
        }

        public void ParseCommand(string input)
        {
            if (input != null)
            {
                string[] inputs = input.Split(' ');
                

                if (input.StartsWith(':'))
                {
                    try
                    {
                        _admincommands[inputs[0]](inputs.ToList());
                    }
                    catch (Exception)
                    {
                        _stregsystemCLI.DisplayAdminCommandNotFoundMessage("Command not found");
                    }                    
                }
                else
                {

                    switch (inputs.Length)
                    {
                        case 1:
                            {
                                try
                                {
                                    User user = _stregsystem.GetUserByUsername(input);
                                    _stregsystemCLI.DisplayUserInfo(user);

                                    IEnumerable<Transaction> transactions = _stregsystem.GetTransactions(user, 10);

                                    foreach (Transaction t in transactions)
                                    {
                                        _stregsystemCLI.DisplayTransaction(t);
                                    }
                                }
                                catch(Exception)
                                {
                                    _stregsystemCLI.DisplayUserNotFound("User not found");

                                }
                                break;
                            }

                        case 2:
                            {
                                Int32.TryParse(inputs[1], out int productId);
                                User user;
                                Product product;
                                user = _stregsystem.GetUserByUsername(inputs[0]);
                                product = _stregsystem.GetProductByID(productId);
                                BuyTransaction buyTransaction = _stregsystem.BuyProduct(user, product);

                                _stregsystemCLI.DisplayUserBuysProduct(buyTransaction);

                                break;
                            }

                        case 3:
                            {
                                break;
                                
                            }
                        default:
                            {
                                _stregsystemCLI.DisplayGeneralError("Unknown command");
                                break;
                            }

                    }
                }

            }
        }
        void ActivateProduct(List<string> args)
        {
            ActivateDeactivateProduct(int.Parse(args.ElementAt(1)), true);

        }

        void DeactivateProduct(List<string> args)
        {
            ActivateDeactivateProduct(int.Parse(args.ElementAt(1)), false);
        }

        void CreditOn(List<string> args)
        {
            CreditOnOff(int.Parse(args.ElementAt(1)), true);
        }

        void CreditOff(List<string> args)
        {
            CreditOnOff(int.Parse(args.ElementAt(1)), false);
        }

        void AddCredits(List<string> args)
        {
            AddCreditsHelper((args.ElementAt(1)),int.Parse(args.ElementAt(2)));
        }

        void CreditOnOff(int productId, bool boolValue)
        {
            try
            {
                Product product = _stregsystem.GetProductByID(productId);
                product.CanBeBoughtOnCredit = boolValue;
            }
            catch (FormatException)
            {
                _stregsystemCLI.DisplayGeneralError("Product ID not recognized");
            }
        }
        void ActivateDeactivateProduct(int productId, bool boolValue)
        {
            try
            {
                Product product = _stregsystem.GetProductByID(productId);
                product.Active = boolValue;
            }
            catch (FormatException)
            {
                _stregsystemCLI.DisplayGeneralError("Product ID not recognized");
            }
        }

        void AddCreditsHelper(string username, int amount)
        {
            try
            {
                User user = _stregsystem.GetUserByUsername(username);
                InsertCashTransaction insertCashTransaction = new InsertCashTransaction(user, amount);
            }
            catch (FormatException)
            {
                _stregsystemCLI.DisplayGeneralError("User ID not recognized");
            }

        }
    }
}
