using GetABike.Domain.Exceptions;

namespace GetABike.Domain.Entities;

public class Lease : Entity
{
    public LeasePlan Plan { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime EstimatedCompletion { get; set; }
    public User? Lessee { get; set; }
    public int LesseeId { get; set; }
    public Bike? Bike { get; set; }
    public int BikeId { get; set; }

    public Lease Leasing(LeasePlan leasePlan, User lessee, Bike bike)
    {
        Plan = leasePlan;
        CreationDate = DateTime.Now;
        StartDate = CreationDate.AddDays(1);
        EndDate = leasePlan.DailyRate switch
        {
            7 => StartDate.AddDays(7),
            15 => StartDate.AddDays(15),
            30 => StartDate.AddDays(30),
            _ => throw new DomainException("Invalid period to rent")
        };

        EstimatedCompletion = EndDate;
        Lessee = lessee;
        Bike = bike;

        return this;
    }
}