using MediatR;
using Microsoft.EntityFrameworkCore;
using Hotel.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class DeleteHotelByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteHotelByIdCommandHandler : IRequestHandler<DeleteHotelByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteHotelByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteHotelByIdCommand request, CancellationToken cancellationToken)
            {
                var hotel = await _context.Hotels.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (hotel == null) return default;
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return hotel.Id;
            }
        }
    }
}
