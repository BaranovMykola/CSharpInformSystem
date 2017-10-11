using TaxiCore.Entities.Transport;

namespace TaxiCore.Entities.Taxi
{
    public interface IDriver
    {
        LicenseCategory DriverCategory { get; set; }
        string Name { get; set; }
    }
}