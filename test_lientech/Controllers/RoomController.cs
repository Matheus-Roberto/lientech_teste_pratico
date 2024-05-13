using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using test_lientech.Data;
using test_lientech.Model;
using test_lientech.Service;

namespace test_lientech.Controllers
{
    
    [ApiController]
    [Route("api/v1/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddRoom(RoomRequestViewModel room)
        {
            try
            {
                var response = await _roomRepository.Create(room);
                if (response is null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateRoom(Room room)
        {
            var response = await _roomRepository.Update(room);
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var response = await _roomRepository.DeleteById(roomId);
            if (response is null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber == 0 || pageQuantity == 0)
            {
                return BadRequest("numero de paginação ou a quantidade de pagina é 0");
            }
            var room = _roomRepository.GetAll(pageNumber, pageQuantity);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int roomId)
        {
            if (roomId == 0)
            {
                return BadRequest("id é 0");
            }
            var room = _roomRepository.GetById(roomId);
            if (room is null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPatch]
        [Route("Attach")]

        public async Task<IActionResult> Attach(int roomId, int movieId)
        {
            var response = await _roomRepository.Attach(roomId, movieId);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPatch]
        [Route("Detach")]

        public async Task<IActionResult> Detach(int roomId, int movieId)
        {
            var response = await _roomRepository.Detach(roomId, movieId);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);


        }


    }
}
