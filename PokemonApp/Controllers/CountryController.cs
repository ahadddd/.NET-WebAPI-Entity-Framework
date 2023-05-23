using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repositories;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(countries);
        }

        [HttpGet("{countryID}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExists(id))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(country);
        }

        [HttpGet("/owners/{ownerID}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfOwner(int ownerID)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerID));

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatCountry([FromBody] CountryDto countryCreate)
        {
            if(countryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var country = _countryRepository.GetCountries().
                Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper()).
                FirstOrDefault();
            if(country != null)
            {
                ModelState.AddModelError("", "Country already exists.");
                StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating country.");
                StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }
    }
}

