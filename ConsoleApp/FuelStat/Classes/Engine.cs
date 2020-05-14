using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuelStat.Utils;

namespace FuelStat
{
    public class Engine
    {
        private readonly IList<Car> cars;
        private readonly IList<FuelStation> stations;

        public Engine()
        {
            cars = new List<Car>()
            {
                new Car("Car1", FuelType.Petrol, 50),
                new Car("Car2", FuelType.Gas, 70),
                new Car("Car3", FuelType.Gas, 40)
            };

            stations = new List<FuelStation>()
            {
                new FuelStation("Station1", 10, FuelType.Gas),
                new FuelStation("Station2", 10, FuelType.Petrol),
                new FuelStation("Station3", 10, FuelType.Gas)
            };
        }

        public void Run()
        {
            var carTasks = new List<Task>();

            foreach (var car in cars)
            {
                carTasks.Add(new Task(() => Processing(car)));
            }

            foreach (var carTask in carTasks)
            {
                carTask.Start();
            }

            carTasks.ForEach(task => task.Wait());
        }

        private void Processing(Car car)
        {
            car.CarState += Notification;
            while (!car.IsEmpty)
            {
                car.Move();
            }

            var station = FindStation(car.FuelType);

            station.Refuel(car, Notification);

            car.CarState -= Notification;
            station.FuelStationState -= Notification;
        }

        private void Notification(object sender, string message)
        {
            Console.WriteLine(message);
        }

        private FuelStation FindStation(FuelType type)
        {
            var randStation = stations[new Random().Next(0, stations.Count)];

            while (randStation.FuelType != type)
            {
                randStation = stations[new Random().Next(0, stations.Count)];
            }

            return randStation;
        }
    }
}
