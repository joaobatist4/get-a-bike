using FluentResults;
using GetABike.Application.Commands;
using GetABike.Domain.Interfaces;
using GetABike.Domain.Interfaces.Repositories;
using GetABike.Infra;
using MediatR;

namespace GetABike.Application.Handlers;

public class UpdateDriversLicenseHandler(
    FileStorageService fileStorageService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateDriversLicenseCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDriversLicenseCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Result.Fail("User not found");

        var fileExtension = Path.GetExtension(request.File.Name).ToLower();

        if (fileExtension != ".bpm" && fileExtension != ".jpg")
            return Result.Fail("Invalid file. Only .jpg or .bmp files are accepted.");
        
        var file = await fileStorageService.UploadFile(request.File);
        
        user.UpdateDriverLicenseImagem(file.Url);

        await userRepository.UpdateAsync(user);
        await unitOfWork.CommitAsync();

        return Result.Ok(file.Url);
    }
}