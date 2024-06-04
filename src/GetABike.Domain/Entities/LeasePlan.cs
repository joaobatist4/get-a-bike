namespace GetABike.Domain.Entities;

public class LeasePlan : Entity
{
    public int NumberOfDays { get; set; }
    public decimal DailyRate { get; set; }
}