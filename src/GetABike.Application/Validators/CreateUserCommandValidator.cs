using FluentValidation;
using GetABike.Application.Commands;
using GetABike.Common;
using GetABike.Common.Enums;

namespace GetABike.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserDeliveryCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Cnpj)
            .Must(p => p.IsValidCnpj())
            .WithMessage("Invalid cnpj");

        RuleFor(p => p.DriversLicenseType)
            .Must(p => p.GetType() == typeof(DriversLicenseType))
            .WithMessage("Invalid drivers license number");
    }
}