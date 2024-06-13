using FluentResults;
using GetABike.Domain.Entities;
using MediatR;

namespace GetABike.Application.Commands;

public record CreateBikeCommand(int Year, string LicensePlate, string Model) : IRequest<Result<Bike>>;