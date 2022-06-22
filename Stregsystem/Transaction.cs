using System;

namespace Stregsystem
{
    class Transaction
    {
        
        int _id;
        User _user;
        DateTime _transactionDate;
        int _amount;

        public Transaction(User user, int amount)
        {
            _user = user;
            _amount = amount;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        protected User associatedUser { get; set; }

        public User User
        {
            get { return associatedUser; }
        }

        public DateTime TransactionDate
        {
            get { return DateTime.Now; }
            set { _transactionDate = value; }
        }

        public override string ToString()
        {
            return $"Id: {_id} Bruger: {_user.Username} Beløb: {_amount} Dato: {_transactionDate}";
        }

        public virtual void Execute()
        {
            
        }

        public class InsufficientCreditsException : Exception
        {
            public InsufficientCreditsException(string message)
                : base(message)
            {
            }
        }
    }
}
