using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class UploadFileCommand : IRequest<string>
    {

        public IFormFile FormFile { get; set; }
        public string Path { get; set; }

        public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
        {

            public async Task<string> Handle(UploadFileCommand command, CancellationToken cancellationToken)
            {
                string extension = "." + command.FormFile.FileName.Split('.')[command.FormFile.FileName.Split('.').Length - 1];

                string fileName = DateTime.Now.Ticks + extension;
                command.Path = command.Path;
                if (!Directory.Exists(command.Path))
                {
                    Directory.CreateDirectory(command.Path);
                }
                using (FileStream stream = new(command.Path + @"\" + fileName, FileMode.Create))
                {
                    await command.FormFile.CopyToAsync(stream);
                }
                return fileName;
            }
        }
        public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
        {
            public UploadFileCommandValidator()
            {
                RuleFor(x => x.Path)
                    .NotEmpty()
                    .WithMessage("Path should be not empty!");
            }
        }

    }
}