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

        [HttpGet("{id:int}")]
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
    }
}