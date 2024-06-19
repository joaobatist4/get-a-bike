using FluentResults;
using GetABike.Application.Commands;
using GetABike.Common.Enums;
using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using MediatR;

namespace GetABike.Application.Handlers;

public class LeaseBikeHandler(
    IBikeRepository bikeRepository,
    IUserRepository userRepository,
    ILeaseRepository leaseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<LeaseBikeCommand, Result<Lease>>
{
    public async Task<Result<Lease>> Handle(LeaseBikeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Result.Fail("User not found");

        if (user.DriversLicenseType is not (DriversLicenseType.A or DriversLicenseType.Ab))
            return Result.Fail("User's not available for lease");
        
        var bike = await bikeRepository.GetByIdAsync(request.BikeId, BikeIncludes.Lease);

        if (bike is null)
            return Result.Fail("Bike not found");

        if (bike.Leases.Any(p => p.StartDate >= request.StartDate && p.EndDate <= request.EndDate))
            return Result.Fail("Bike is not available");

        var lease = new Lease(user, bike, request.Days, request.StartDate);

        await leaseRepository.AddAsync(lease);
        await unitOfWork.CommitAsync();

        return lease;
    }
}