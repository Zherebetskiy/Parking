namespace Parking
{
    public class Car
    {
        string _id;
        double _balance;
        CarType _type;

        public string Id {
            get { return _id; }
            private set { _id = value; }
        }
        public double Balance {
            get { return _balance; }
            set { _balance = value; }
        }
        public CarType Type {
            get { return _type; }
            private set { _type = value; }
        }

        public Car(string id, double balance,CarType type)
        {
            _id = id;
            _balance = balance;
            _type = type;
        }

        public void Refill(double money)
        {
            _balance += money;
        }
    }
}
