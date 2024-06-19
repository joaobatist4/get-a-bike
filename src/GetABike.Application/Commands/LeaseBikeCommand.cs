using FluentResults;
using GetABike.Domain.Entities;
using MediatR;

namespace GetABike.Application.Commands;

public record LeaseBikeCommand(int Days, DateTime StartDate, DateTime EndDate, int BikeId, int UserId)
    : IRequest<Result<Lease>>;