namespace Stregsystem
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, int amount) : base(user, amount)
        {

        }
        public override string ToString()
        {
            return $"Deposit:\n{Amount}";
        }

        public void Execute(int amount)
        {
            associatedUser.Balance = associatedUser.Balance + amount;
        }
    }
}

