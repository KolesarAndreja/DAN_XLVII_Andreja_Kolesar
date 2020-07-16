using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DAN_XLVII_AndrejaKolesar
{
    class Program
    {
        #region static fields and methods
        internal static Random rnd = new Random();

        /// <summary>
        /// Print schedule of vehicles
        /// </summary>
        /// <param name="vehicles"></param>
        public static void PrintCreatedVehicles(Vehicle[] vehicles)
        {
            Console.WriteLine("Total number of vehicles: {0} \nSCHEDULE:",vehicles.Length);
            for(int i = 0; i<vehicles.Length; i++)
            {
                Console.WriteLine("Vehicle " + vehicles[i].order + ": " + vehicles[i].direction );
            }
        }
        /// <summary>
        /// Vehicle v is crossing the bridge
        /// </summary>
        /// <param name="v"></param>
        public static void CrossBridge(Vehicle v)
        {
            Console.WriteLine("Vehicle " + v.order + " is moving " + v.direction);
            Thread.Sleep(500);
        }
        #endregion

        #region main
        static void Main(string[] args)
        {
            //get random nubmer of vehicles [1,15]
            int x = rnd.Next(1, 4);
            Publisher publisher = new Publisher();
            publisher.onNotification += PrintCreatedVehicles;

            //create x vehicles and thread for every vehicle
            Thread[] threads = new Thread[x];
            Vehicle[] vehicles = new Vehicle[x];
            int y;
            for (int i = 0; i < x; i++)
            {
                y = rnd.Next(0, 2);
                string s = y == 0 ? "South" : "North";
                //make new vehicle
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
            //start measure time
            stopwatch.Start();
            //start vehicles
            threads[0].Start();
            for (int i = 1; i < x; i++)
            {
                //if direction of current vehicle is same as direction of last vehicle, start crossing a bridge
                if (vehicles[i].direction == vehicles[i - 1].direction)
                {
                    threads[i].Start();
                }
                //if direction is different, wait all previous vehicles to cross the bridge
                else
                {
                    Console.WriteLine("Vehicle " + vehicles[i].order + " is waiting");
                    for (int j = 0; j<i; j++)
                    {
                        threads[j].Join();
                    }
                    threads[i - 1].Join();
                    threads[i].Start();
                }
            }
            for (int j = 0; j < x; j++)
            {
                threads[j].Join();
            }
            stopwatch.Stop();
            Console.WriteLine("Total time: {0}ms" ,stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
        #endregion
    }
}
