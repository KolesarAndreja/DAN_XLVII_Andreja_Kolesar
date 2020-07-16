using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVII_AndrejaKolesar
{
    class Program
    {
        #region static fields and methods
        internal static Random rnd = new Random();


        public static void PrintCreatedVehicles(Vehicle[] vehicles)
        {
            Console.WriteLine("Total number of vehicles: {0} \nSCHEDULE:",vehicles.Length);
            for(int i = 0; i<vehicles.Length; i++)
            {
                Console.WriteLine("Vehicle " + vehicles[i].order + ": " + vehicles[i].direction );
            }
        }

        public static void CrossBridge(Vehicle v)
        {
            Console.WriteLine("Vehicle " + v.order + "is moving " + v.direction);
            Thread.Sleep(500);
        }
        #endregion

        #region main
        static void Main(string[] args)
        {
            int x = rnd.Next(1, 15);
            int y = 0;
            Publisher publisher = new Publisher();
            publisher.onNotification += PrintCreatedVehicles;

            //create x vehicles and thread for every vehicle
            Thread[] threads = new Thread[x];
            Vehicle[] vehicles = new Vehicle[x];

            for(int i = 0; i < x; i++)
            {
                y = rnd.Next(0, 2);
                string s = y == 0 ? "South" : "North";
                vehicles[i] = new Vehicle
                {
                    order = i + 1,
                    direction = s
                };

                //thread
                int localI = i;
                threads[i] = new Thread(()=>CrossBridge(vehicles[localI]));
            }
            PrintCreatedVehicles(vehicles);
            Console.WriteLine("BRIDGE:");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //start vehicles
            threads[0].Start();
            for (int i = 1; i < x; i++)
            {
                if (vehicles[i].direction == vehicles[i - 1].direction)
                {
                    threads[i].Start();
                }
                else
                {
                    Console.WriteLine("Vehicle " + vehicles[i].order + "is waiting");
                    threads[i - 1].Join();
                    //while (threads[i - 1].IsAlive)
                    //{
                    //    Thread.Sleep(0);
                    //}
                    threads[i].Start();
                }
            }
            threads[x - 1].Join();
            stopwatch.Stop();
            Console.WriteLine("Total time: {0}ms" ,stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
        #endregion
    }
}
