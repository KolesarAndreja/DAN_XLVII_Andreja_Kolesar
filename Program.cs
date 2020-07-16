using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVII_AndrejaKolesar
{
    class Program
    {
        internal static Random rnd = new Random();

        public static void PrintCreatedVehicles(Vehicle[] vehicles)
        {
            Console.WriteLine("Total number of vehicles: {0}",vehicles.Length);
            for(int i = 0; i<vehicles.Length; i++)
            {
                Console.WriteLine("Vehicle " + vehicles[i].order + ": " + vehicles[i].direction );
            }
        }
        static void Main(string[] args)
        {
            int x = rnd.Next(1, 16);
            Publisher publisher = new Publisher();
            publisher.onNotification += PrintCreatedVehicles;

            //create x vehicles
            Vehicle[] vehicles = new Vehicle[x];
            for(int i = 0; i < x; i++)
            {
                Vehicle v = new Vehicle(i+1);
                vehicles[i] = v;
            }
            PrintCreatedVehicles(vehicles);


            Thread[] t = new Thread[x];
            Console.ReadKey();

        }
    }
}
