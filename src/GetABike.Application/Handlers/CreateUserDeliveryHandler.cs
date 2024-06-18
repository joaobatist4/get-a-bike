using FluentResults;
using GetABike.Application.Commands;
using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using GetABike.Infra;
using MediatR;

namespace GetABike.Application.Handlers;

public class CreateUserDeliveryHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    FileStorageService fileStorageService
) : IRequestHandler<CreateUserDeliveryCommand, Result<User>>
{
    public async Task<Result<User>> Handle(CreateUserDeliveryCommand request, CancellationToken cancellationToken)
    {
        var anyByCnpj = await userRepository.AnyByCnpjAsync(request.Cnpj);

        if (anyByCnpj)
            return Result.Fail($"Cannot create user because the cnpj {request.Cnpj} already exists");

        var anyByDriversLicenseNumber = await userRepository.AnyByDriversLicenseAsync(request.DriversLicenseNumber);

        if (anyByDriversLicenseNumber)
            return Result.Fail(
                $"It is not possible to register this user with the provided license number {request.DriversLicenseNumber}, because there is already a user registered with this license.");

        var fileExtension = Path.GetExtension(request.DriversLicenseImage.Name).ToLower();
        
        if (fileExtension != ".bpm" && fileExtension != ".jpg")
            return Result.Fail("Invalid file. Only .jpg or .bmp files are accepted.");
        
        var file = await fileStorageService.UploadFile(request.DriversLicenseImage);
        
        var user = new User().CreateDeliveryUser(request.Name,
            request.Cnpj,
            request.DriversLicenseNumber,
            file.Url,
            request.DriversLicenseType,
            request.BirthDate);

        await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();

        return Result.Ok(user);

    }
}