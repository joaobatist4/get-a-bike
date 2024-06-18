using GetABike.Domain.Entities;

namespace GetABike.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> AnyByCnpjAsync(string cnpj);
    Task<bool> AnyByDriversLicenseAsync(string driversLicenseNumber);
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task UpdateAsync(User user);
}