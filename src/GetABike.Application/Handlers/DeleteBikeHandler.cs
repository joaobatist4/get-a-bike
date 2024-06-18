using FluentResults;
using GetABike.Application.Commands;
using GetABike.Domain.Entities;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using MediatR;

namespace GetABike.Application.Handlers;

public class DeleteBikeHandler(
    IBikeRepository bikeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteBikeCommand, Result<Bike>>
{
    public async Task<Result<Bike>> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
    {
        var bike = await bikeRepository.GetByIdAsync(request.BikeId);

        if (bike is null)
            return Result.Fail("Bike not found");

        if (bike.Leases.Count != 0)
            return Result.Fail("It is not possible to delete this bike because there are rental contracts for it.");
        
        bike.Delete();

        await bikeRepository.UpdateAsync(bike);
        await unitOfWork.CommitAsync();

        return Result.Ok(bike);
    }
}