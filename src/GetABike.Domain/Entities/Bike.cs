using GetABike.Domain.Enums;
using GetABike.Domain.Exceptions;

namespace GetABike.Domain.Entities;

public class Bike : Entity
{
    public string Year { get; init; }
    public string Model { get; init; }
    public string LicensePlate { get; private set; }
    public DateTime? DeletionDate { get; private set; }
    public User? Author { get; set; }
    public int? AuthorId { get; set; }
    
    public Bike(string year, string model, string licensePlate, int authorId)
    {
        CreationDate = DateTime.Now;
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
        AuthorId = authorId;
    }

    public void EditBike(string licensePlate)
        => LicensePlate = licensePlate;

    public void Delete()
        => DeletionDate = DateTime.Now;
}