using FuelStat.Utils;

namespace FuelStat.Interfaces
{
    public interface IFuel
    {
        void FillTank(int fuelLitters);

        FuelType FuelType { get; }

        int TankVolume { get; }
    }
}
