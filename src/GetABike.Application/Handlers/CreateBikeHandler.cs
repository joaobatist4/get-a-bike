using FluentResults;
using GetABike.Application.Commands;
using GetABike.Domain;
using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using MediatR;

namespace GetABike.Application.Handlers;

public class CreateBikeHandler(
    IBikeRepository bikeRepository,
    AuthenticatedUser authenticatedUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBikeCommand, Result<Bike>>
{
    public async Task<Result<Bike>> Handle(CreateBikeCommand request, CancellationToken cancellationToken)
    {
        var bikeAlreadyExists = await bikeRepository.AnyByLicensePlateAsync(request.LicensePlate);

        if (bikeAlreadyExists)
            return Result.Fail($"Can't create bike because already exists one with this license plate {request.LicensePlate}");

        var bike = new Bike(request.Year, request.Model, request.LicensePlate, authenticatedUser.Id);

        await bikeRepository.AddAsync(bike);

        await unitOfWork.CommitAsync();

        return bike;
    }
}