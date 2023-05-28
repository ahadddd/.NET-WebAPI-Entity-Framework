using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int id);

        ICollection<Review> GetReviewsOfPokemon(int pokeId);

        bool UpdateReview(Review review);

        bool ReviewExists(int reviewId);

        bool Save();
    }
}
