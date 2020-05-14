using System;
using FuelStat.Interfaces;
using FuelStat.Utils;

namespace FuelStat
{
    public class FuelStation
    {
        private readonly object locer = new object();
        public event EventHandler<string> FuelStationState;

        public FuelStation(string fuelId, int volume, FuelType fuelType)
        {
            FuelId = fuelId;
            Volume = volume;
            FuelType = fuelType;
        }

        public string FuelId { get; }

        public int Volume { get; private set; }

        public FuelType FuelType { get; }

        public void Refuel(IFuel car, EventHandler<string> handler)
        {
            lock (locer)
            {
                FuelStationState += handler;
                if (FuelType == car.FuelType && Volume > car.TankVolume)
                {
                    FuelStationState?.Invoke(null, $"Station {FuelId} is refueling...");
                    Volume -= car.TankVolume;
                    FuelStationState -= handler;
                    return;
                }

                if (Volume > car.TankVolume)
                {
                    FuelStationState?.Invoke(null, $"Station {FuelId} has {FuelType} when car has {car.FuelType}.");
                }
                else
                {
                    FuelStationState?.Invoke(null, $"Station {FuelId} doesn't have enough fuel.");
                }
                FuelStationState -= handler;
            }
        }
    }
}
