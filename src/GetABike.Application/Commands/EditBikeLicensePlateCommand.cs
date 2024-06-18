using FluentResults;
using GetABike.Domain.Entities;
using MediatR;

namespace GetABike.Application.Commands;

public record EditBikeLicensePlateCommand(int BikeId, string LicensePlate) : IRequest<Result<Bike>>;