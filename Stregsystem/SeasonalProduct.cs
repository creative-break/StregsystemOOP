using System;


namespace Stregsystem
{
    class SeasonalProduct : Product
    {
        public DateTime _seasonStartDate;
        public DateTime _seasonEndDate;

        public SeasonalProduct(int id, string name, int price, bool active, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime SeasonEndDate): base(id, name, price, active, canBeBoughtOnCredit)
        {
            _seasonStartDate = seasonStartDate;
            _seasonEndDate = SeasonEndDate;
        }
        public bool active
        {
            get
            {
                if (DateTime.Now >= _seasonStartDate && DateTime.Now <= _seasonEndDate)
                    return true;
                else
                    return false;
            }
        }
    }
}
