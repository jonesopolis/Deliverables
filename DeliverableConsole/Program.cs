using System;
using System.Collections.Generic;
using System.Linq;
using Deliverables;
using Deliverables.Models;
using System.Threading.Tasks;

namespace DeliverableConsole
{
    public sealed class Program
    {
        private static IEnumerable<ITruck> _trucks;
        private static IEnumerable<Delivery> _deliveries;

        static async Task Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                _trucks = new List<ITruck>();
                _deliveries = new List<Delivery>();

                await Run();

                Console.WriteLine("Run Simulator Again? (Y/N)");
                run = Console.ReadLine().ToLower() == "y";
            }
        }

        private static async Task Run()
        {
            _trucks = GetTrucks();
            _deliveries = GetDeliveries();

            Console.WriteLine($"Press Any Key To Run Simulation with {_trucks.Count()} Trucks and {_deliveries.Count()} Deliveries");
            Console.ReadKey();

            DeliverySimulator simulator = new AutomatedDeliverySimulator(_trucks, _deliveries);

            if (!simulator.Validate(out IEnumerable<string> errors))
            {
                Console.WriteLine("Errors in simulator:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"\t{error}");
                }
            }
            else
            {
                await simulator.RunAsync();
                foreach (var truck in _trucks)
                {
                    Console.WriteLine($"Truck {truck.Name}:");
                    Console.WriteLine($"Total Travel Time: {truck.TotalTravelTime}");
                    foreach (var delivery in truck.Deliveries)
                    {
                        Console.WriteLine($"\tDelivery Tracking Number: {delivery.TrackingNumber}");
                        Console.WriteLine($"\tDelivery Started: {delivery.DepartureTime}");
                        Console.WriteLine($"\tDelivery Ended: {delivery.DepartureTime + delivery.HoursToComplete}");
                        Console.WriteLine();
                    }
                }
            }
        }

        private static IEnumerable<Delivery> GetDeliveries()
        {
            var deliveries = new List<Delivery>();

            WriteInfo();

            string input;
            while ((input = Console.ReadLine()) != string.Empty)
            {
                if (int.TryParse(input, out int i))
                {
                    deliveries.Add(new Delivery(i));
                    Console.WriteLine($"Added a {i} hour(s) delivery");
                }
                else
                {
                    Console.WriteLine("Invalid");
                }

                WriteInfo();
            }

            return deliveries;

            void WriteInfo()
            {
                Console.WriteLine($"Add Deliveries By Inputting Hours To Complete (Blank to Quit):");
                Console.WriteLine();
            }
        }

        private static IEnumerable<ITruck> GetTrucks()
        {
            var trucks = new List<ITruck>();

            WriteInfo();

            string input;
            while ((input = Console.ReadLine()) != string.Empty)
            {
                switch (input)
                {
                    case "1":
                        trucks.Add(new RedTruck());
                        Console.WriteLine("Added a Red Truck");
                        break;
                    case "2":
                        trucks.Add(new BlueTruck());
                        Console.WriteLine("Added a Blue Truck");
                        break;
                    case "3":
                        Console.WriteLine("Input Custom Truck Name (Cannot be Empty)");
                        string customName;
                        while ((customName = Console.ReadLine()) == string.Empty) { }
                        trucks.Add(new CustomTruck(customName));
                        Console.WriteLine($"Added a Custom Truck with Name: {customName}");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }

                WriteInfo();
            }

            return trucks;

            void WriteInfo()
            {
                Console.WriteLine($"Add Trucks (Blank to Quit):");
                Console.WriteLine($"\t 1. Red");
                Console.WriteLine($"\t 2. Blue");
                Console.WriteLine($"\t 3. Custom");
                Console.WriteLine();
            }
        }
    }
}