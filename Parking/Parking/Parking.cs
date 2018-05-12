using Parking.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public enum CarType { Truck, Passenger, Bus, Motorcycle };

    class Parking
    {
        public Dictionary<Car, TransactionManager> cars = new Dictionary<Car, TransactionManager>();
        public List<Transaction> transactions = new List<Transaction>();
        public float Balance { get; set; }
        public int Timeout { get; set; }
        public Dictionary<CarType, int> Prices = new Dictionary<CarType, int>();
        public int ParkingSpace { get; set; }
        public float Fine { get; set; }
        public int FreePlaces { get; set; }
        static object locker = new object();

        private static Lazy<Parking> parking = new Lazy<Parking>(() => new Parking
        {
            Timeout = Settings.Timeout,
            Prices = Settings.Prices,
            ParkingSpace = Settings.ParkingSpace,
            Fine = Settings.Fine,
            FreePlaces= Settings.ParkingSpace
        });

        public static Parking Instance { get { return parking.Value; } }

        public List<Transaction> GetTransactionByLastMinute()
        {
            DateTime dateNow = DateTime.Now;
            List<Transaction> tempTransactions = new List<Transaction>();
            lock (locker)
            {
                for (int i = transactions.Count-1; i >= 0 ; i--)
                {
                    if (dateNow.Subtract(transactions[i].TimeOfTransaction).TotalMinutes<1)
                    {
                        tempTransactions.Add(transactions[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return tempTransactions;
        }

        public void AddCar(Car car)
        {
            if (FreePlaces == 0)
            {
                throw new ParkingIsFullException($"Sorry, parking is full.");
            }
            else
            {
                TransactionManager transactionManager = new TransactionManager(car.Id);
                cars.Add(car, transactionManager);
                Task task = new Task(transactionManager.TransactionTimer);
                task.Start();
                FreePlaces--;
            }
        }

        public void RemoveCar(Car car)
        {
                var resCar = FindCarById(car.Id);
                if (resCar.Balance < 0)
                {
                    throw new FinePresentException($"Sorry, you must pay a fine {Math.Abs(car.Balance)}");
                }
                else
                {
                    cars.Remove(resCar);
                    FreePlaces++;
                }
        }

        public Car FindCarById(string id)
        {
            var res = cars.FirstOrDefault(car => car.Key.Id == id);
            return res.Key;
        }
    }


}
