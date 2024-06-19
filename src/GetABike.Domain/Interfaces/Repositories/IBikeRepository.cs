using GetABike.Domain.Entities;

namespace GetABike.Domain.Interfaces.Repositories;

public interface IBikeRepository
{
    Task AddAsync(Bike bike);
    Task<bool> AnyByLicensePlateAsync(string licensePlate);
    Task<Bike?> GetByIdAsync(int id, BikeIncludes includes = BikeIncludes.None);
    Task UpdateAsync(Bike bike);
}

[Flags]
public enum BikeIncludes
{
    None,
    Lease
}