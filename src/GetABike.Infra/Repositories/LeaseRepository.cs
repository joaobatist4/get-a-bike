using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces.Repositories;
using GetABike.Infra.Context;

namespace GetABike.Infra.Repositories;

public class LeaseRepository(ApplicationContext context) : ILeaseRepository
{
    public async Task AddAsync(Lease lease)
        => await context.Leases.AddAsync(lease);
}