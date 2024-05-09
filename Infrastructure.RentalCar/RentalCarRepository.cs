using Application.RentalCar;
using Domain.RentalCar;

namespace Infrastructure.RentalCar
{
    public class RentalCarRepository : IQueryRentalCarUserCase
    {
        public IEnumerable<IVehicle> GetAllCars()
        {
            throw new NotImplementedException();
        }
    }
}