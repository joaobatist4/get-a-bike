namespace GetABike.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}