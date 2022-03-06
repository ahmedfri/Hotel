using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using Hotel.Service.Features.HotelFeatures.Commands;
using Hotel.Service.Features.HotelFeatures.Queries;
using Hotel.Service.Dto.Common;
using Microsoft.AspNetCore.Http;
using System.IO;
using Hotel.Infrastructure.ViewModel.Dto;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/Hotel")]
    public class HotelController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateHotelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

     

        [HttpPost("search")]
        public async Task<IActionResult> GetAll(SearchHotelsQuery query)
        {
            HotelsModel Hotels = await Mediator.Send(query);
            return StatusCode(Hotels.StatusCode, Hotels.Data == null ? Hotels.Messege : Hotels.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            HotelModel Hotel = await Mediator.Send(new GetHotelByIdQuery { Id = id });
            return StatusCode(Hotel.StatusCode, Hotel.Data == null ? Hotel.Messege : Hotel.Data);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteHotelByIdCommand { Id = id }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateHotelCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


        [HttpPost("add/review")]
        public async Task<IActionResult> AddHotelReview(CreateHotelReviewCommand command)
        {
            BaseModel HotelReview = await Mediator.Send(command);
            return StatusCode(HotelReview.StatusCode, HotelReview.Messege);

        }


        [HttpPost("add/image")]
        public async Task<IActionResult> AddHotelImage(IFormFile image, int hotelId)
        {
            if (!IsWrongFileExtension(image))
            {
                return BadRequest("wrong file extension");
            }
            string ImageUrl = await Mediator.Send(new UploadFileCommand
            {
                FormFile = image,
                Path = Directory.GetCurrentDirectory() + @"\Uploads\HotelImages",
            });
            BaseModel Hotelimage = await Mediator.Send(new CreateHotelImageCommand { HotelId = hotelId, Url = ImageUrl });
            return StatusCode(Hotelimage.StatusCode, Hotelimage.Messege);
        }
        private bool IsWrongFileExtension(IFormFile file)
        {
            string extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
            return (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif");
        }
    }
}
