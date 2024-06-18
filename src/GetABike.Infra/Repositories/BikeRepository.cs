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

    public async Task<Bike?> GetByIdAsync(int id)
        => await context.Bikes.FindAsync(id);

    public async Task UpdateAsync(Bike bike)
        => await ValueTask.CompletedTask;
}