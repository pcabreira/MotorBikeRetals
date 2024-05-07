using Azure.Storage.Blobs;
using MediatR;
using MotorBikeRetals.Application.Commands.CreateUserImage;
using MotorBikeRetals.Core.Repositories;
using MotorBikeRetals.Infrastructure.Persistence.Repositories;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Commands.UpdateBike
{
    public class CreateUserImageCommandHandler : IRequestHandler<CreateUserImageCommand, Unit>
    {
        private const string ContainerName = "myfiles";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly IUserRepository _repositoryUser;

        public CreateUserImageCommandHandler(BlobServiceClient blobServiceClient, IUserRepository repositoryUser)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
            _repositoryUser = repositoryUser;
        }

        public async Task<Unit> Handle(CreateUserImageCommand request, CancellationToken cancellationToken)
        {
            FileInfo fileInfo = new FileInfo(request.File.FileName);
            var file = request.IdUser + fileInfo.Extension;
            var blobClient = _containerClient.GetBlobClient(file);
            await blobClient.UploadAsync(request.File.OpenReadStream(), true);

            var user = await _repositoryUser.GetByIdAsync(new Guid(request.IdUser));
            user.Details.ImageURL = file;

            await _repositoryUser.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
