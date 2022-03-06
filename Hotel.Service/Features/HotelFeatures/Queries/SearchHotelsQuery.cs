using AutoMapper;
using Hotel.Infrastructure.ViewModel.Dto;
using Hotel.Persistence;
using LinqKit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Service.Features.HotelFeatures.Queries
{
    public class SearchHotelsQuery : Pagination, MediatR.IRequest<HotelsModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public class SearchHotelsQueryHandler : IRequestHandler<SearchHotelsQuery, HotelsModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public SearchHotelsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
               
            }
            public async Task<HotelsModel> Handle(SearchHotelsQuery query, CancellationToken cancellationToken)
            {
                Expression<Func<Domain.Entities.Hotel, bool>> predicate = PredicateBuilder.True<Domain.Entities.Hotel>();

                if (!string.IsNullOrEmpty(query.Name))
                {
                    predicate = predicate.And(i => i.Name.ToLower().Contains(query.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(query.Email))
                {
                    predicate = predicate.And(i => i.Email.Contains(query.Email));
                }
                if (!string.IsNullOrEmpty(query.PhoneNumber))
                {
                    predicate = predicate.And(i => i.PhoneNumber.Contains(query.PhoneNumber));
                }
                List<Domain.Entities.Hotel> hotelList = null;


                if (hotelList == null || hotelList.Count == 0)
                    hotelList = await _context.Hotels
                        .Include(x => x.Reviews)
                        .Include(x => x.Images)

                        .Include(x => x.Rooms)
                        .Where(predicate)
                        .OrderBy(o => o.Name)

                        .ToListAsync();
                if (hotelList == null)
                {
                    return new HotelsModel
                    {
                        Data = null,
                        StatusCode = 404,
                        Messege = "No data found"
                    };
                }
                _mapper.Map<List<HotelDto>>(hotelList);
                return new HotelsModel
                {
                    Data = _mapper.Map<List<HotelDto>>(hotelList),
                    StatusCode = 200,
                    Messege = "Data found"
                };
            }

        }
    }
}