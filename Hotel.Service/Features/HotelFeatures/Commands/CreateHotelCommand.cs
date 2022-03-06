using MediatR;
using Hotel.Domain.Entities;
using Hotel.Persistence;
using System.Threading;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Hotel.Service.Dto.Common;
using FluentValidation;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class CreateHotelCommand : IRequest<BaseModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, BaseModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
  
        

            public CreateHotelCommandHandler(IApplicationDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
               
            }

            public async Task<BaseModel> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
            {
                var hotel = new Domain.Entities.Hotel
                {
                    Name = request.Name,
                    Email = request.Email,
                    Location = request.Location,
                    PhoneNumber = request.PhoneNumber,
                    Description = request.Description,
                    Address = request.Address,

                };
              
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return new BaseModel
                {
                    StatusCode = 200,
                    Messege = "Data Saved!"
                };
            }
        }
    }
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(0, 50)
                .WithMessage("Name Is Required");
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(0, 50)
               .WithMessage("Email Is Required")
                .EmailAddress().WithMessage("Email Is not Valid");
        }
    }
}
