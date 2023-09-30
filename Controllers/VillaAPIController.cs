using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa_API.Data;
using Villa_API.Dto;
using Villa_API.Model;

namespace Villa_API.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<VillaDto>> GetVillas()
        {
            IEnumerable<Villa> villasList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villasList));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        // [ProducesResponseType(200)]
        // [ProducesResponseType(404)]
        // [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetVillasById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(t => t.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VillaDto>> CreateVillaAsync([FromBody] VillaCreateDto createDto)
        {
            if (await _db.Villas.FirstOrDefaultAsync(t => t.Name.ToLower() == createDto.Name.ToLower()) != null)
            {//if name is exists
                ModelState.AddModelError("Custom model", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest();
            }
            // if (villaDto.Id > 0)
            // {
            //     return StatusCode(StatusCodes.Status500InternalServerError);
            // }
            // Villa model = new()
            // {
            //     Amenity = villaDto.Amenity,
            //     Details = villaDto.Details,
            //     ImageUrl = villaDto.ImageUrl,
            //     Occupancy = villaDto.Occupancy,
            //     Name = villaDto.Name,
            //     Rate = villaDto.Rate,
            //     Sqft = villaDto.Sqft
            // };
            var model = _mapper.Map<Villa>(createDto);
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = model.Id }, createDto);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVillaAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(t => t.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVillaAsync(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }
        //    var villa = await _db.Villas.FirstOrDefaultAsync(t => t.Id == id);
            Villa model = _mapper.Map<Villa>(updateDto);
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch]
        public async Task<IActionResult> UpdatePartialVillaAsync(int id, JsonPatchDocument<VillaUpdateDto> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            // VillaUpdateDto villaDto = new()
            // {
            //     Amenity = villa.Amenity,
            //     Details = villa.Details,
            //     Id = villa.Id,
            //     ImageUrl = villa.Name,
            //     Occupancy = villa.Occupancy,
            //     Rate = villa.Rate,
            //     Sqft = villa.Sqft
            // };
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDto, ModelState);
            // Villa model = new()
            // {
            //     Amenity = villaDto.Amenity,
            //     Details = villaDto.Details,
            //     Id = villaDto.Id,
            //     ImageUrl = villaDto.Name,
            //     Occupancy = villaDto.Occupancy,
            //     Rate = villaDto.Rate,
            //     Sqft = villaDto.Sqft
            // };
            Villa model = _mapper.Map<Villa>(villaDto);
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            return NoContent();
        }
    }
}