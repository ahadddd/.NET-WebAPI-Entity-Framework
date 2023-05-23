using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
           var reviews =  _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(reviews);

        }

        [HttpGet("{reviewID}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewID)
        {
            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewID));
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(review);

        }

        [HttpGet("pokemon/{pokeID}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfPokemon(int pokeID)
        {
            var reviews = _mapper.Map<List<Review>>(_reviewRepository.GetReviewsOfPokemon(pokeID));
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(reviews);

        }
    }
}
