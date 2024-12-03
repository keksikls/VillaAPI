using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Models;

namespace VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //Получение всех
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.vilaList);
        }

        //Для получения по Id
        [HttpGet("{id:int}", Name ="GetVilla")]
        //документации статус кодов
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var vila = VillaStore.vilaList.FirstOrDefault(u => u.Id == id);

            if (vila == null)
            {
                return NotFound();
            }

            return Ok(vila);
        }
        //Для создания новой записи в типа бд в нашей модели 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO vilaDTO)
        {
            if (VillaStore.vilaList.FirstOrDefault(u => u.Name.ToLower() == vilaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (vilaDTO == null)
            {
                return BadRequest(vilaDTO);
            }

            if (vilaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //для получения следующего id 
            vilaDTO.Id = VillaStore.vilaList.OrderByDescending(u=>u.Id).FirstOrDefault().Id+1;
            VillaStore.vilaList.Add(vilaDTO);

            return CreatedAtRoute("GetVilla", new { id = vilaDTO.Id},vilaDTO);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id) 
        {
            if (id == 0 )
            {
                return BadRequest();
            }

            var vila = VillaStore.vilaList.FirstOrDefault(u => u.Id == id);

            if (vila == null)
            {
                return NotFound();
            }
            VillaStore.vilaList.Remove(vila);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest(); 
            }

            var vila = VillaStore.vilaList.FirstOrDefault(u => u.Id == id);

            vila.Name = villaDTO.Name;
            vila.Sqft = villaDTO.Sqft;
            vila.Occupancy = villaDTO.Occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> pathDTO)
        {
            if (pathDTO == null || id == 0)
            {
                return BadRequest();
            }

            var vila = VillaStore.vilaList.FirstOrDefault(u => u.Id == id);

            if (vila == null)
            {
                return BadRequest();
            }

            pathDTO.ApplyTo(vila, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
