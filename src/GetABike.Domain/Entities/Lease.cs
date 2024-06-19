using GetABike.Domain.Exceptions;

namespace GetABike.Domain.Entities;

public class Lease : Entity
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; private set; }
    public DateTime EstimatedCompletion { get; private set; }
    public User? Lessee { get; init; }
    public int LesseeId { get; init; }
    public Bike? Bike { get; init; }
    public int BikeId { get; init; }
    public decimal NetPrice { get; private set; }
    public decimal GrossPrice { get; private set; }
    
    public Lease(User lessee, Bike bike, int days, DateTime startDate)
    {
        CreationDate = DateTime.Now;
        StartDate = startDate.AddDays(1);
        EndDate = CalculateEndDate(startDate, days);
        NetPrice = CalculatePrice(days);
        GrossPrice = CalculatePrice(days);
        EstimatedCompletion = EndDate;
        Lessee = lessee;
        Bike = bike;
    }
    
    private static decimal CalculatePrice(int days)
        => days switch
        {
            7 => days * 30,
            15 => days * 28,
            30 => days * 22,
            45 => days * 20,
            50 => days * 18,
            _ => throw new InvalidOperationException()
        };
    
    private static DateTime CalculateEndDate(DateTime startDate, int days)
        => days switch
        {
            7 => startDate.AddDays(7),
            15 => startDate.AddDays(15),
            30 => startDate.AddDays(30),
            45 => startDate.AddDays(45),
            50 => startDate.AddDays(50),
            _ => throw new DomainException("Invalid period to rent")
        };
}