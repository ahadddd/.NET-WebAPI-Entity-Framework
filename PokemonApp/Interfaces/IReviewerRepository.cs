using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewerRepository
    {
        Reviewer GetReviewer(int reviewerId);

        ICollection<Reviewer> GetReviewers();

        ICollection<Review> GetReviewsByReviewer(int reviewerId);

        bool UpdateReviewer(Reviewer reviewer);

        bool Save();

        bool ReviewerExists(int reviewerId);
    }
}
