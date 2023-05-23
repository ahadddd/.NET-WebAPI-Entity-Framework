using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(review => review.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
           return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfPokemon(int pokeId)
        {
            return _context.Reviews.Where(p => p.Pokemon.Id == pokeId).ToList();
        }
        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
