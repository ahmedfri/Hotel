using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Persistence;
using Hotel.Service.Dto.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Commands
{
    public class CreateHotelRoomsCommand : IRequest<BaseModel>
    {
        public int RoomNo { get; set; }
        public int NoOfPersons { get; set; }
        public double Price { get; set; }
        public int HotelId { get; set; }

        public class CreateHotelRoomsCommandHandler : IRequestHandler<CreateHotelRoomsCommand, BaseModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public CreateHotelRoomsCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<BaseModel> Handle(CreateHotelRoomsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Room room = new()
                    {
                        RoomNo = request.RoomNo,
                        NoOfPersons = request.NoOfPersons,
                        Price = request.Price,
                        HotelId = request.HotelId,
                    };
                    _context.Rooms.Add(room);
                    await _context.SaveChangesAsync();
                    return new BaseModel
                    {
                        StatusCode = 200,
                        Messege = "Data Saved!"
                    };
                }
                catch (System.Exception ex)
                {
                    return new BaseModel
                    {
                        StatusCode = 500,
                        Messege = ex.InnerException.Message.ToString()
                };

                   
                }
               
            }
        }
        }

    }

