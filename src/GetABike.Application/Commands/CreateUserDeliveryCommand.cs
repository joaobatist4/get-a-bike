using FluentResults;
using GetABike.Common.Enums;
using GetABike.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GetABike.Application.Commands;

//TODO: adicionar validação usando Fluent Validation para os campos CNPJ e Tipo da CNH
public record CreateUserDeliveryCommand(
    string Name,
    string Cnpj,
    DateTime BirthDate,
    string DriversLicenseNumber,
    DriversLicenseType DriversLicenseType,
    IFormFile DriversLicenseImage) : IRequest<Result<User>>;