using Parking.Exceptions;
using System;
using System.Collections.Generic;

namespace Parking
{
    public class Menu
    {
        Parking parking = Parking.Instance;

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Please, chose the option.");
                Console.WriteLine(" A - Add car to a parking;\n R - Remove car from parking;\n E - Refill car balance;\n B - Show parking balance;\n F - Show free parking place;\n T - Show transaction log;\n S - Show transactions by last minute;\n X - Exit.");
                string actionKey = Console.ReadLine();

                if (actionKey.Length==0)
                {
                    continue;
                }

                var key = Convert.ToChar(actionKey.ToUpper());

                switch (key)
                {
                    case 'A':
                        AddNewCar();
                        break;
                    case 'R':
                        RemoveCar();
                        break;
                    case 'E':
                        RefillCarBalance();
                        break;
                    case 'B':
                        ShowParkingBalance();
                        break;
                    case 'F':
                        ShowFreeParkingPlace();
                        break;
                    case 'T':
                        ShowTransactionLog();
                        break;
                    case 'S':
                        ShowTransactionByLastMinute();
                        break;
                    case 'X':
                        return;
                    default:
                        Console.WriteLine("Sorry, you input invalid data.");
                        break;
                }
            }
        }

        void ShowTransactionByLastMinute()
        {
            List<Transaction> transactions = parking.GetTransactionByLastMinute();

            foreach (var item in transactions)
            {
                Console.WriteLine($"{item.TimeOfTransaction}\tCar Id:{item.CarId}\t Fee:{item.Fee}");
            }
        }

        void ShowTransactionLog()
        {
            TransactionManager.GetTransactoinLog();
        }

        void RemoveCar()
        {
            ShowAllCarsOnParking();
            Console.WriteLine("Chose car by id");
            var id = Console.ReadLine();
            try
            {
                var car = parking.FindCarById(id);
                if (car == null)
                {
                    throw new CarIsNotFoundException($"Car with id {id} is not found.");
                }
                else
                {
                    parking.RemoveCar(car);
                }
            }
            catch (CarIsNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FinePresentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void ShowParkingBalance()
        {
            Console.WriteLine($"Parking balance:{parking.Balance}");
        }

        void ShowFreeParkingPlace()
        {
            Console.WriteLine($"Free parking place:{parking.FreePlaces}");
        }

        void RefillCarBalance()
        {
            ShowAllCarsOnParking();
            Console.WriteLine("Chose car by id");
            var id = Console.ReadLine();
            try
            {
                var car = parking.FindCarById(id);
                if (car == null)
                {
                    throw new CarIsNotFoundException($"Car with id {id} is not found.");
                }
                else
                {
                    Console.WriteLine("Enter the balance");
                    double additionalBalance;
                    if (double.TryParse(Console.ReadLine(), out additionalBalance))
                    {
                        car.Refill(additionalBalance);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, you input invalid data.");
                        RefillCarBalance();
                    }
                }
            }
            catch (CarIsNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void ShowAllCarsOnParking()
        {
            foreach (var car in parking.cars)
            {
                Console.WriteLine($"{car.Key.Id}\t {car.Key.Balance}\t {car.Key.Type}");
            }
        }

        void AddNewCar()
        {
            Console.WriteLine("Enter the car balance");
            double balance;
            if (double.TryParse(Console.ReadLine(), out balance))
            {
                Console.WriteLine("Enter the car id");
                var id = Console.ReadLine();
                if (parking.FindCarById(id) == null)
                {
                    Console.WriteLine("Chose car type");
                    var carType = ChoseCarType();

                    Car car = new Car(id, balance, carType);
                    try
                    {
                        parking.AddCar(car);
                    }
                    catch (ParkingIsFullException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                else
                {
                    Console.WriteLine("This id has already exist.");
                    AddNewCar();
                }
            }
            else
            {
                Console.WriteLine("Sorry, you input invalid data.");
                AddNewCar();
            }
        }

        CarType ChoseCarType()
        {
            Console.WriteLine("1 - Truck\t2 - Passenger\t3 - Bus\t4 - Motorcycle");
            CarType carType = 0;
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    carType = CarType.Truck;
                    break;
                case 2:
                    carType = CarType.Passenger;
                    break;
                case 3:
                    carType = CarType.Bus;
                    break;
                case 4:
                    carType = CarType.Motorcycle;
                    break;
                default:
                    Console.WriteLine("Sorry, you input invalid data.");
                    ChoseCarType();
                    break;
            }

            return carType;
        }
    }
}
