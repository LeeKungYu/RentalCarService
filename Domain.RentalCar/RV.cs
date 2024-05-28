using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RentalCar
{
    public class RV : IVehicle
    {
        public string Model { get; set; }
        public string CC { get; set; }

        public decimal CalculateRentalCost(int daysRented)
        {
            return daysRented * 120; //假設為美元
        }

        public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
        {
            return end - start;
        }

        public VehicleType GetVehicleType() => VehicleType.RV;


    }
}
