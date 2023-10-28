
using bukitasam.DTOs;
using bukitasam.models;
using bukitasam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bukitasam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryRepository _historyRepository;
        public HistoryController(HistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpPost]
        public IActionResult CreateHistory([FromBody] HistoryDTO data)
        {
            bool history = _historyRepository.InsertHistory(data);
            if (history)
            {
                return Ok(new
                {
                    status = "Succsess",
                });
            }

            return BadRequest(new
            {
                status = "Failid",
            });
        }

        [HttpDelete]
        public IActionResult DeleteHistory([FromQuery] int id)
        {
            bool history = _historyRepository.DeleteHistory(id);
            if (history)
            {
                return Ok(new
                {
                    status = "Succsess",
                });
            }

            return BadRequest(new
            {
                status = "Failid",
            });
        }

        [HttpGet]
        public IActionResult GetHistory([FromQuery] string filter, string? search)
        {
            List<HistoryModel> result = _historyRepository.GetHistory(filter, search);

            return Ok(result);
        }
    }
}