using Domain.RentalCar;

namespace Application.RentalCar
{
    public class RentalCarServices
    {
        //private IVehicle _iVehicle;
        private readonly IQueryRentalCarUserCase _queryRentalCarUserCase;

        public RentalCarServices(IQueryRentalCarUserCase queryRentalCarUserCase)
        {
            //_iVehicle = vehicle;
            _queryRentalCarUserCase = queryRentalCarUserCase;
        }

        public IEnumerable<IVehicle> GetAllCars()
        {
            return _queryRentalCarUserCase.GetAllCars();
        }

        public TimeSpan ChoiseRentalTime(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateRentalCost(int daysRented, VehicleType vehicleType)
        {
            var result = _queryRentalCarUserCase.GetAllCars()
                .Where(c => c.GetVehicleType() == vehicleType)
                .FirstOrDefault();

            return result!.CalculateRentalCost(daysRented);
        }
    }

}