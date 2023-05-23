using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewerRepository
    {
        Reviewer GetReviewer(int reviewerId);

        ICollection<Reviewer> GetReviewers();

        ICollection<Review> GetReviewsByReviewer(int reviewerId);

        bool ReviewerExists(int reviewerId);
    }
}
