using GetABike.Domain.Enums;
using GetABike.Domain.Exceptions;

namespace GetABike.Domain.Entities;

public class User : Entity
{ 
    public string Name { get; private set; } = null!;
    public string UserName { get; private set; }= null!;
    public string Password { get; private set; }= null!;
    public User? Author { get; private set; }
    public int? AuthorId { get; private set; }
    
    public UserType Type { get; private set; }
    public string? Cnpj { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string? CnhNumber { get; private set; }
    public CNHType? CnhType { get; private set; }
    public string? CnhUrl { get; private set; }

    public User CreateAdminUser(string name, string userName, string password, User author)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Author = author;
        CreationDate = DateTime.Now;
        Type = UserType.Admin;
        
        return this;
    }

    public User CreateDeliveryUser(string name, string userName, string password, string cnpj, string cnhNumber, string cnhUrl, CNHType cnhType,
        DateTime birthDate, User author)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Author = author;
        Type = UserType.Delivery;

        Cnpj = cnpj;
        CnhNumber = cnhNumber;
        CnhUrl = cnhUrl;
        CnhType = cnhType;
        BirthDate = birthDate;

        CreationDate = DateTime.Now;
        
        return this;
    }

    public void UpdateCnh(string cnhUrl)
        => CnhUrl = cnhUrl;
}