using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDBContext _db;
        public VillaController(ILogger<VillaController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al obtener la villa con Id: " + id);
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
                return NotFound();

            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CrearVilla([FromBody] VillaDTO villaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
                return BadRequest(villaDTO);

            if (villaDTO.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            Villa modelo = new()
            {
                Nombre = villaDTO.Nombre,
                Ocupantes = villaDTO.Ocupantes,
                Amenidad = villaDTO.Amenidad,
                Detalle = villaDTO.Detalle,
                Tarifa = villaDTO.Tarifa,
                ImagenURL = villaDTO.ImagenURL,
                MetrosCuadrados = villaDTO.MetrosCuadrados
            };

            _db.Villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("id:int")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

            if (villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPut("id:int")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
                return BadRequest();

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Ocupantes = villaDto.Ocupantes,
                Amenidad = villaDto.Amenidad,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa,
                ImagenURL = villaDto.ImagenURL,
                MetrosCuadrados = villaDto.MetrosCuadrados
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("id:int")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> villaDto)
        {
            if (villaDto == null || id == 0)
                return BadRequest();

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Amenidad = villa.Amenidad,
                Detalle= villa.Detalle,
                ImagenURL= villa.ImagenURL,
                MetrosCuadrados = villa.MetrosCuadrados,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa
            };

            if (villa == null)
                return BadRequest();

            villaDto.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Amenidad= villaDTO.Amenidad,
                Detalle = villaDTO.Detalle,
                ImagenURL = villaDTO.ImagenURL,
                MetrosCuadrados = villaDTO.MetrosCuadrados,
                Nombre = villaDTO.Nombre,
                Ocupantes = villaDTO.Ocupantes,
                Tarifa = villaDTO.Tarifa
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
