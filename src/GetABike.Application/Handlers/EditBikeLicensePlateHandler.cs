using FluentResults;
using GetABike.Application.Commands;
using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using MediatR;

namespace GetABike.Application.Handlers;

public class EditBikeLicensePlateHandler(
    IBikeRepository bikeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<EditBikeLicensePlateCommand, Result<Bike>>
{
    public async Task<Result<Bike>> Handle(EditBikeLicensePlateCommand request, CancellationToken cancellationToken)
    {
        var anyByLicensePlate = await bikeRepository.AnyByLicensePlateAsync(request.LicensePlate);

        if (anyByLicensePlate)
            return Result.Fail(
                $"Can't create bike because already exists one with this license plate {request.LicensePlate}");

        var bike = await bikeRepository.GetByIdAsync(request.BikeId);

        if (bike is null)
            return Result.Fail("Bike not found");
        
        bike.EditBike(request.LicensePlate);

        await bikeRepository.UpdateAsync(bike);

        await unitOfWork.CommitAsync();

        return Result.Ok(bike);
    }
}