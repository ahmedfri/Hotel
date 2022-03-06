using Hotel.Service.Dto.Common;
using Hotel.Service.Features.HotelFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/Room")]
    public class RoomController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        [HttpPost]
    
        public async Task<IActionResult> AddHotelRoom(CreateHotelRoomsCommand command)
        {
            BaseModel HotelRoom = await Mediator.Send(command);
            return StatusCode(HotelRoom.StatusCode, HotelRoom.Messege);

        }
    }
}
