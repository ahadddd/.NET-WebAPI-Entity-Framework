using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Collections.Generic;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
            if (!_ownerRepository.OwnerExists(id))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDto createOwner)
        {
            if (createOwner == null)
            {
                return BadRequest(ModelState);
            }
            var owner = _ownerRepository.GetOwners().
                Where(o => o.LastName.Trim().ToUpper() == createOwner.LastName.TrimEnd().ToUpper()).
                FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exists!");
                StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(createOwner);
            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Error encountered while adding owner.");
                StatusCode(400, ModelState);
            }
            return Ok("Created successfully.");
        }

        [HttpPut("{ownerID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerID, Owner updateOwner)
        {
            if (updateOwner == null)
                return BadRequest();
            if (ownerID != updateOwner.Id)
                return BadRequest();
            if (!_ownerRepository.OwnerExists(ownerID))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ownerMap = _mapper.Map<Owner>(updateOwner);

            if (!_ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Owner could not be updated.");
                StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
