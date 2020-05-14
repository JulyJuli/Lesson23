using System;
using FuelStat.Interfaces;
using FuelStat.Utils;

namespace FuelStat
{
    public class Car : IFuel, IMoving
    {
        private const int min = 10;
        private const int max = 20;

        private int _tankState;

        public event EventHandler<string> CarState;

        public Car(string carId, FuelType fuelType, int tankVolume)
        {
            CarId = carId;
            _tankState = tankVolume;
            FuelType = fuelType;
            TankVolume = tankVolume;
            IsEmpty = false;
        }

        public string CarId { get; }

        public bool IsEmpty { get; private set; }

        public int TankState
        {
            get => _tankState;
            private set
            {
                if (value >= 0)
                {
                    _tankState = value;
                }
            }
        }

        public FuelType FuelType { get; }
        public int TankVolume { get; }

        public void Move()
        {
            CarState?.Invoke(null, $"Car {CarId} started");

            while (_tankState > max)
            {
                var rand = new Random().Next(min, max);
                TankState -= rand;
                CarState?.Invoke(null, $"Car {CarId} moving with tank {TankState}");
            }

            IsEmpty = true;

            TankState = 0;
            CarState?.Invoke(null, $"*****Car {CarId} is going to fuel station!*****");
        }

        public void FillTank(int fuelLitters)
        {
            TankState += fuelLitters;
            CarState?.Invoke(null, $"Car {CarId} is full!");
            IsEmpty = false;
        }
    }
}
