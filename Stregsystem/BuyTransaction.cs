namespace Stregsystem
{
    class BuyTransaction : Transaction
    {
        Product associatedProduct;

        public BuyTransaction(User user, Product product) : base(user, product.Price)
        {
            associatedProduct = product;
            associatedUser = user;            
        }

        public override string ToString()
        {
            return $"Order confirmation: \nProduct: {associatedProduct}\nUser: {associatedUser}\nTransaction id: {Id}\nTime {TransactionDate} ";
        }

        public override void Execute()
        {
            if (associatedProduct.Price > associatedUser.Balance)
            {
                throw new InsufficientCreditsException("Not enough credit");
            }
            else
            {
                associatedUser.Balance = associatedUser.Balance - associatedProduct.Price;                
            }
            base.Execute();
        }

    }
}