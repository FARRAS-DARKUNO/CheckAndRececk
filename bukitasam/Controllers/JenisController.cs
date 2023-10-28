
using bukitasam.DTOs;
using bukitasam.models;
using bukitasam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bukitasam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JenisController : ControllerBase
    {
        private readonly JenisRepository _jenisRepository;

        public JenisController(JenisRepository jenisRepository)
        {
            _jenisRepository = jenisRepository;
        }

        [HttpPost]
        public IActionResult CreateJenis([FromBody] JenisDTO data)
        {
            bool jenis = _jenisRepository.InsertJenis(data);
            if (jenis)
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

        [HttpPut]
        public IActionResult UpdateJenis([FromBody] JenisDTO data, int idBarang)
        {
            bool jenis = _jenisRepository.UpdateJenis(data, idBarang);
            if (jenis)
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
        public IActionResult GetJenis([FromQuery] string? seacrh)
        {
            List<JenisModel> result = _jenisRepository.GetJenis(seacrh);

            return Ok(result);
        }
    }
}