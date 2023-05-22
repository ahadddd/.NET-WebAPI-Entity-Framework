using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories() {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(categories);
        }

        [HttpGet("{categoryID")]
        [ProducesResponseType(200, Type= typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryID)
        {
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);

        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCateogory(int cateogoryID)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>
                (_categoryRepository.GetPokemonByCategory(cateogoryID));
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(pokemons);
        }
    }

}
