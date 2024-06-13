using GetABike.Domain.Entities;

namespace GetABike.Domain.Interfaces.Repositories;

public interface IBikeRepository
{
    Task AddAsync(Bike bike);
    Task<bool> AnyByLicensePlateAsync(string licensePlate);
}