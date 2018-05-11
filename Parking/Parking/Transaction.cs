using System;

namespace Parking
{
    public class Transaction
    {
        DateTime _timeOfTransaction;
        string _carId;
        float _fee;

        public DateTime TimeOfTransaction
        {
            get { return _timeOfTransaction; }
            set { _timeOfTransaction = value; }
        }
        public string CarId
        {
            get { return _carId; }
        }
        public float Fee
        {
            get { return _fee; }
        }

        public Transaction(DateTime timeOfTransaction, string carId)
        {
            _timeOfTransaction = timeOfTransaction;
            _carId = carId;
        }

        public void DoTransaction()
        {
            var parking = Parking.Instance;
            var car = parking.FindCarById(_carId);

            if (car.Balance - parking.Prices[car.Type] < 0)
            {
                _fee = parking.Prices[car.Type] * parking.Fine;
            }
            else
            {
                _fee = parking.Prices[car.Type];
            }

            car.Balance -= _fee;          
            parking.Balance += _fee;
            TransactionManager.feePerMinute += _fee;
            parking.transactions.Add(this);
        }
    }
}

