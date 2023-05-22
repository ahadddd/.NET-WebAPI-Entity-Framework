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

        [HttpGet("{countryID")]
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
    }
}
