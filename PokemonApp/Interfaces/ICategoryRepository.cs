using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(int id);

        ICollection<Pokemon> GetPokemonByCategory(int categoryID);

        bool CategoryExists(int id);

        bool CreateCategory(Category category);

        bool Save();
        
    }
}
