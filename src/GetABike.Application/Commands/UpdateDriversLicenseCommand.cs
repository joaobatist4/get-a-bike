using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GetABike.Application.Commands;

public record UpdateDriversLicenseCommand(IFormFile File, int UserId) : IRequest<Result<string>>;