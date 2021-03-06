using FluentValidation;
using Hotel.Domain.Entities;
using Hotel.Persistence;
using Hotel.Service.Dto.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class CreateHotelImageCommand : IRequest<BaseModel>
    {
        public string Url { get; set; }
        public int HotelId { get; set; }

        public class CreateHotelImageCommandHandler : IRequestHandler<CreateHotelImageCommand, BaseModel>
        {
            private readonly IApplicationDbContext _context;
            public CreateHotelImageCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BaseModel> Handle(CreateHotelImageCommand command, CancellationToken cancellationToken)
            {
                HotelImage Image = new()
                {
                    Url = command.Url,
                    HotelId = command.HotelId,
                };
                _context.HotelImages.Add(Image);
                await _context.SaveChangesAsync();
                return new BaseModel
                {
                    StatusCode = 200,
                    Messege = "Data Saved!"
                };
            }
        }
        public class CreateHotelImageCommandValidator : AbstractValidator<CreateHotelImageCommand>
        {
            public CreateHotelImageCommandValidator()
            {
                RuleFor(x => x.Url)
                    .NotEmpty()
                    .WithMessage("Url should be not empty!");
            }
        }


    }
}
