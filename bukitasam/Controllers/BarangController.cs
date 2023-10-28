using bukitasam.DTOs;
using bukitasam.models;
using bukitasam.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bukitasam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarangController : ControllerBase
    {
        private readonly BarangRepository _barangRepository;

        public BarangController(BarangRepository barangRepository)
        {
            _barangRepository = barangRepository;
        }

        [HttpPost]
        public IActionResult CreateBarang([FromBody] BarangDTO data)
        {
            bool barang = _barangRepository.InsertBarang(data);
            if (barang)
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
        public IActionResult GetBarang([FromQuery] string? seacrh)
        {
            List<BarangModel> result = _barangRepository.GetBarang(seacrh);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateBarang([FromBody] BarangDTO data, int idBarang)
        {
            bool barang = _barangRepository.UpdateBarang(data, idBarang);
            if (barang)
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
    }
}