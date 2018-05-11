using System.Collections.Generic;

namespace Parking
{
    public static class Settings
    {
        public static int Timeout { get; }
        public readonly static Dictionary<CarType, int> Prices = new Dictionary<CarType, int>();
        public static int ParkingSpace { get; }
        public static float Fine { get; }

        static Settings()
        {
            Timeout = 3;

            Prices.Add(CarType.Truck, 5);
            Prices.Add(CarType.Passenger, 3);
            Prices.Add(CarType.Bus, 2);
            Prices.Add(CarType.Motorcycle, 1);

            ParkingSpace = 20;
            Fine = 2;
        }
    }
}
