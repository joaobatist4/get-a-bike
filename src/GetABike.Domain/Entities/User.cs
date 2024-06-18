using GetABike.Common.Enums;

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
    public string? DriversLicenseNumber { get; private set; }
    public DriversLicenseType? DriversLicenseType { get; private set; }
    public string? DriversLicenseUrl { get; private set; }

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

    public User CreateDeliveryUser(string name, string cnpj, string cnhNumber, string cnhUrl, DriversLicenseType driversLicenseType,
        DateTime birthDate)
    {
        Name = name;
        //Author = author;
        Type = UserType.Delivery;

        Cnpj = cnpj;
        DriversLicenseNumber = cnhNumber;
        DriversLicenseUrl = cnhUrl;
        DriversLicenseType = driversLicenseType;
        BirthDate = birthDate;

        CreationDate = DateTime.Now;
        
        return this;
    }

    public void UpdateDriverLicenseImagem(string cnhUrl)
        => DriversLicenseUrl = cnhUrl;
}