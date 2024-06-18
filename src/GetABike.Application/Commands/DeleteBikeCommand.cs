using FluentResults;
using GetABike.Domain.Entities;
using MediatR;

namespace GetABike.Application.Commands;

public record DeleteBikeCommand(int BikeId) : IRequest<Result<Bike>>;