using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(ICollection<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonRepository.GetPokemons();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(pokemons);
            }
        }

        [HttpGet("{pokeID}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeID)
        {
            if(_pokemonRepository.PokemonExists(pokeID) == false)
                return NotFound();
                
            var pokemon = _pokemonRepository.GetPokemon(pokeID);
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);
        }


        [HttpGet("{pokeID}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeID)
        {
            if (_pokemonRepository.PokemonExists(pokeID) == false)
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeID);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(rating);
        }




    }
}
