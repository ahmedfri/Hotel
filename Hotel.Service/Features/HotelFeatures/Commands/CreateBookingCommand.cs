using AutoMapper;
using FluentValidation;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.ViewModel.Dto;
using Hotel.Persistence;
using Hotel.Service.Dto.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class CreateBookingCommand : IRequest<BookingModel>
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsConfirmed { get; set; }
        public double ActualPrice { get; set; }
        public double Discount { get; set; }
        public double PaidAmount { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }

        public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public CreateBookingCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BookingModel> Handle(CreateBookingCommand Request, CancellationToken cancellationToken)
            {
                var book = new Booking
                {
                   RoomId= Request.RoomId,
                   UserName= Request.UserName,
                   ActualPrice=Request.ActualPrice,
                   PaidAmount=Request.PaidAmount,
                   CheckOut=Request.CheckOut,
                   CheckIn=Request.CheckIn,
                   IsConfirmed=Request.IsConfirmed,

                };
                _context.Bookings.Add(book);
                await _context.SaveChangesAsync();
       
                return new BookingModel
                {
                    StatusCode = 200,
                    Messege = "Data Saved"
                };
            }
        }
    }
}