using MediatR;
using Hotel.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class UpdateHotelCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public DateTime CreateDate { get; set; }
        public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateHotelCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
            {
                var hotel = _context.Hotels.Where(a => a.Id == request.Id).FirstOrDefault();

                if (hotel == null)
                {
                    return default;
                }
                else
                {
                    hotel.Name = request.Name;
                    hotel.Email = request.Email;
                    hotel.Location = request.Location;
                    hotel.PhoneNumber = request.PhoneNumber;
                    _context.Hotels.Update(hotel);
                    await _context.SaveChangesAsync();
                    return hotel.Id;
                }
            }
        }
    }
}
