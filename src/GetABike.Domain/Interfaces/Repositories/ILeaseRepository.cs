using GetABike.Domain.Entities;

namespace GetABike.Domain.Interfaces.Repositories;

public interface ILeaseRepository
{
    Task AddAsync(Lease lease);
}