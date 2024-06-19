using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces.Repositories;
using GetABike.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GetABike.Infra.Repositories;

public class BikeRepository(
    ApplicationContext context) : IBikeRepository
{
    public async Task AddAsync(Bike bike)
        => await context.Bikes.AddAsync(bike);

    public async Task<bool> AnyByLicensePlateAsync(string licensePlate)
        => await context.Bikes.AnyAsync(p => p.LicensePlate.Equals(licensePlate));

    public async Task<Bike?> GetByIdAsync(int id, BikeIncludes includes = BikeIncludes.None)
        => await context.Bikes
            .Includes(includes)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdateAsync(Bike bike)
        => await ValueTask.CompletedTask;
}

public static class BikeQueryExtensions
{
    public static IQueryable<Bike> Includes(this IQueryable<Bike> query, BikeIncludes includes)
    {
        if (includes.HasFlag(BikeIncludes.Lease))
            query = query.Include(b => b.Leases);

        return query;
    }
}