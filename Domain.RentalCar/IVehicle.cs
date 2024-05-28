using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RentalCar
{
    public interface IVehicle
    {
        decimal CalculateRentalCost(int daysRented);
        TimeSpan ChoiseRentalTime(DateTime start, DateTime end);
        VehicleType GetVehicleType();

        string Model { get; set; }
        string CC { get; set; } 
    }
}
