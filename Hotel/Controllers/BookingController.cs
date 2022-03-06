using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Hotel.Service.Features.HotelFeatures.Commands;
using Microsoft.AspNetCore.Http;
using Hotel.Infrastructure.ViewModel.Dto;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/Booking")]
    public class BookingController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            BookingModel booking = await Mediator.Send(command);
            return StatusCode(booking.StatusCode, booking.Data == null ? booking.Messege : booking.Data);
        }
    }
}
