﻿using AutoMapper;
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

        [HttpGet("{categoryID}")]
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(category != null)
            {
                ModelState.AddModelError("", "Category alreadye exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong.");
                return StatusCode(500, ModelState); 
            }
            return Ok("Successfully added.");

        }


        [HttpPut("{categoryID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryID, [FromBody] CategoryDto categoryUpdate)
        {
            if (categoryUpdate == null)
            {
                ModelState.AddModelError("", "Something wrong happened.");
                BadRequest(ModelState);
            }

            if (categoryID != categoryUpdate.Id)
                return BadRequest();
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var categoryMap = _mapper.Map<Category>(categoryUpdate);
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Could not be updated.");
                return StatusCode(500, ModelState);
            }
            return NoContent(); 
        }

        [HttpDelete("{categoryID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryID)
        {
            //if (categoryID == null)
            //    return BadRequest(ModelState);
            if (!_categoryRepository.CategoryExists(categoryID))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = _mapper.Map<Category>(_categoryRepository.GetCategory(categoryID));
            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Category could not be deleted.");
                StatusCode(500, ModelState);
            }
            return Ok("Category deleted.");
        }

    }

}
