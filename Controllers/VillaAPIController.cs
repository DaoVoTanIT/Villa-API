using Microsoft.AspNetCore.Mvc;
using Villa_API.Data;
using Villa_API.Dto;
using Villa_API.Model;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<VillaDto> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        // [ProducesResponseType(200)]
        // [ProducesResponseType(404)]
        // [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetVillasById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(t => t.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            // if(!ModelState.IsValid){
            //     return BadRequest();
            // }
            if (VillaStore.villaList.FirstOrDefault(t => t.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {//if name is exists
                ModelState.AddModelError("Custom model", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.villaList.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }
        [HttpDelete]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else if (VillaStore.villaList.FirstOrDefault(t => t.Id == id) == null)//neu id ko co trong store
            {
                return NotFound();
            }
            else
            {
                VillaStore.villaList.RemoveAll(t => t.Id == id);
                return NoContent();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(t => t.Id == id);

            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;
            return NoContent();
        }
    }
}