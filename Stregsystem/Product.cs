namespace Stregsystem
{
    class Product
    {
        public int _id;
        public string _name;
        public int _price;
        public bool _active;
        public bool _canBeBoughtOnCredit;

        public Product(int id, string name, int price,
                        bool active, bool canBeBoughtOnCredit)
        {
            Id = id;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit; 
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        public bool CanBeBoughtOnCredit
        {
            get { return _canBeBoughtOnCredit; }
            set { _canBeBoughtOnCredit = value; }
        }

        public override string ToString()
        {
            return $"Id:{_id}. Produkt: {_name}, Pris: {_price} Kroner";
        }        
    }
}