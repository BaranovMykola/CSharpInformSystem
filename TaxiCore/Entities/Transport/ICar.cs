using TaxiCore.Entities.Taxi;

namespace TaxiCore.Entities.Transport
{
    public interface ICar
    {
        LicenseCategory Category { get; set; }
        string Model { get; set; }
        int SeatsCouunt { get; set; }

        bool CanDrive(Driver driver);
    }
}