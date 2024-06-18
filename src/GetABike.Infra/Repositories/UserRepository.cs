using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces.Repositories;
using GetABike.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GetABike.Infra.Repositories;

public class UserRepository(ApplicationContext context) : IUserRepository
{
    public async Task<bool> AnyByCnpjAsync(string cnpj)
        => await context.Users.AnyAsync(c => c.Cnpj!.Equals(cnpj));

    public async Task<bool> AnyByDriversLicenseAsync(string driversLicenseNumber)
        => await context.Users.AnyAsync(p => p.DriversLicenseNumber!.Equals(driversLicenseNumber));

    public async Task AddAsync(User user)
        => await context.Users.AddAsync(user);

    public async Task<User?> GetByIdAsync(int id)
        => await context.Users.FindAsync(id);

    public async Task UpdateAsync(User user)
        => await ValueTask.CompletedTask;
}