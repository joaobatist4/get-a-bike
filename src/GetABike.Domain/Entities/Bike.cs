using GetABike.Domain.Exceptions;

namespace GetABike.Domain.Entities;

public class Bike : Entity
{
    public int Year { get; init; }
    public string Model { get; init; }
    public string LicensePlate { get; private set; }
    public DateTime? DeletionDate { get; private set; }
    public User? Author { get; set; }
    public int? AuthorId { get; set; }
    private List<Lease> _leases = [];
    public IReadOnlyCollection<Lease> Leases => _leases.AsReadOnly();
    
    public Bike(int year, string model, string licensePlate, int authorId)
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
    {
        if (_leases.Count != 0)
            throw new DomainException("Cannot delete bike because there are at least one lease");

        DeletionDate = DateTime.Now;
    }
}